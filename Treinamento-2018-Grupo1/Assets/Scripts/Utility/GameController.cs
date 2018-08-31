using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scripts/Utility/Game Controller")]
public class GameController : MonoBehaviour {

    public static GameController gameController = null;

    public enum GameState { Play, Paused, Dead };
    public GameState currentState;

    public GameObject player;

	void Awake () {

        if (gameController != null) {
            Destroy(gameObject);
            return;
        }

        gameController = this;
        DontDestroyOnLoad(gameObject);

        // Cria um delegate a ser chamado quando uma nova cena é carregada.
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        if (scene.name.Contains("Level_")) {

            if (Player.player == null) {

                GameObject new_player = Instantiate(player, SceneScript.currentSceneScript.playerSpawn.position, SceneScript.currentSceneScript.playerSpawn.rotation);
                CameraScript cam_script = Camera.main.GetComponent<CameraScript>();
                cam_script.UpdateCameraScript(new_player.transform);

            } else {
                Player.player.transform.position = SceneScript.currentSceneScript.playerSpawn.position;
                Player.player._rigidbody.velocity = Vector3.zero;
            }

            
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void QuitToMainMenu () {

        Time.timeScale = 1;

        gameController = null;
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);

    }

    public void RestartLevel () {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
