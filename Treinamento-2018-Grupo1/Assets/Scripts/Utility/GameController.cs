using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scripts/Utility/Game Controller")]
public class GameController : MonoBehaviour {

    public static GameController gameController = null;

    public enum GameState { Play, Paused, Dead, Cutscene, Transition };
    public GameState currentState;

    public GameObject player;

    public GameObject pauseScreen;
    public GameObject deathScreen;
    public Image transitionScreen;

    public bool loadingScene = true;

	void Awake () {

        // Garante que só exista um GameControler por vez.
        if (gameController != null) {
            Destroy(gameObject);
            return;
        }
        // Garante que esse GameCOntroller passe para as próximas cenas.
        gameController = this;
        DontDestroyOnLoad(gameObject);

        // Cria um delegate a ser chamado quando uma nova cena é carregada.
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        print(SceneManager.GetActiveScene().name);

        // Inicializa o GameController se a cena for um nível jogável.
        if (scene.name.Contains("Level_") && loadingScene) {

            transitionScreen.color = Color.black;

            gameController.currentState = GameState.Play;

            if (Player.player == null) {

                GameObject new_player = Instantiate(player, SceneScript.currentSceneScript.playerSpawn.position, SceneScript.currentSceneScript.playerSpawn.rotation);
                CameraScript cam_script = Camera.main.GetComponent<CameraScript>();
                cam_script.UpdateCameraScript(new_player.transform);

            } else {
                Player.player.transform.position = SceneScript.currentSceneScript.playerSpawn.position;
                Player.player._rigidbody.velocity = Vector3.zero;
            }

            loadingScene = false;

        } else {

            // Se a cena não for um nível jogável destrói esse objeto e o player se ele existir.

            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (Player.player != null)
                Destroy(Player.player.gameObject);

            gameController = null;
            Destroy(gameObject);

        }

    }

    // Update is called once per frame
    void Update () {

        // Gera o efeito de fade in ao entrar em uma cena.
        Color transition_color = transitionScreen.color;
        if (currentState == GameState.Transition && transition_color.a < 1) {

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

	}

    // Puasa o jogo.
    public void PauseGame () {

        gameController.currentState = GameState.Paused;
        pauseScreen.SetActive(true);
        Time.timeScale = 0;

    }

    // Despausa o jogo.
    public void UnpauseGame() {

        gameController.currentState = GameState.Play;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;

    }

    // Sai para o menu principal.
    public void QuitToMainMenu () {

        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");

    }

    public void RestartLevel () {

        // Recarrega a cena atual.
        LoadScene(SceneManager.GetActiveScene().name);

    }

    public void LoadScene (string sceneName) {

        // Esconde os menus.
        pauseScreen.SetActive(false);
        deathScreen.SetActive(false);

        // Carrega a cena.
        loadingScene = true;
        SceneManager.LoadSceneAsync(sceneName);

    }
}
