using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scripts/Utility/Game Controller")]
public class GameController : MonoBehaviour {

    public static GameController instance = null;

    public enum GameState { Play, Paused, Dead, Cutscene };
    public GameState currentState;

    public GameObject player;

    public GameObject pauseScreen;
    public GameObject deathScreen;
    public GameObject loadingScreen;
    public Image transitionScreen;
    [HideInInspector]
    public bool transition = false;

	void Awake () {

        // Garante que só exista um GameControler por vez.
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        // Garante que esse GameCOntroller passe para as próximas cenas.
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Cria um delegate a ser chamado quando uma nova cena é carregada.
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        Debug.Log("(GameController) Done loading " + scene.name + "!");

        // Inicializa o GameController se a cena for um nível jogável.
        if (scene.name.Contains("Level_")) {

            //AudioManager.instance.RebuildSFX();

            pauseScreen.SetActive(false);
            deathScreen.SetActive(false);
            loadingScreen.SetActive(false);

            transition = false;

            Time.timeScale = 1;

            transitionScreen.color = Color.black;

            instance.currentState = GameState.Play;

            if (Player.instance == null) {

                Instantiate(player, SceneScript.currentSceneScript.playerSpawn.position, SceneScript.currentSceneScript.playerSpawn.rotation);

            } else {
                Player.instance.transform.position = SceneScript.currentSceneScript.playerSpawn.position;
                Player.instance._rigidbody.velocity = Vector3.zero;
                Player.instance.UpdateAbility();
            }

            CameraScript cam_script = Camera.main.GetComponent<CameraScript>();
            cam_script.UpdateCameraScript(Player.instance.transform);

        } else {

            // Se a cena não for um nível jogável destrói esse objeto e o player se ele existir.

            Time.timeScale = 1;

            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (Player.instance != null)
                Destroy(Player.instance.gameObject);

            instance = null;
            Destroy(gameObject);

        }

    }

    // Update is called once per frame
    void Update () {

        // Gera o efeito de fade in ao entrar em uma cena.
        Color transition_color = transitionScreen.color;
        if ((transition || loadingScreen.activeSelf) && transition_color.a < 1) {

            if (transition_color.a + Time.deltaTime > 1)
                transition_color.a = 1;
            else
                transition_color.a += Time.deltaTime;

            transitionScreen.color = transition_color;

        } else if (transition_color.a > 0) { // Gera o efeito de fade out ao sair de uma cena.

            if (transition_color.a - Time.deltaTime < 0)
                transition_color.a = 0;
            else
                transition_color.a -= Time.deltaTime;

            transitionScreen.color = transition_color;

        }

        if (currentState == GameState.Cutscene && Input.GetButtonDown("Cancel"))
            QuitToMainMenu();

	}

    // Puasa o jogo.
    public void PauseGame () {

        instance.currentState = GameState.Paused;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;

    }

    // Despausa o jogo.
    public void UnpauseGame() {

        instance.currentState = GameState.Play;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;

    }

    // Sai para o menu principal.
    public void QuitToMainMenu () {

        LoadScene("MainMenu", true);

    }

    public void RestartLevel () {

        // Recarrega a cena atual.
        LoadScene(SceneManager.GetActiveScene().name, true);

    }

    public void LoadScene (string sceneName, bool loadScreen) {

        if (loadScreen) {

            pauseScreen.SetActive(false);
            deathScreen.SetActive(false);

            loadingScreen.SetActive(true);
        }

        // Carrega a cena.
        SceneManager.LoadSceneAsync(sceneName);

    }
}
