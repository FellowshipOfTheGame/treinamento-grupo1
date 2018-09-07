using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scripts/Utility/Scene Script")]
// Guarda informações importantes para a fase atual.
public class SceneScript : MonoBehaviour {

    // Guarda uma referência estática a esse script.
    public static SceneScript currentSceneScript;

    public Transform playerSpawn;

    [System.Serializable]
    public struct CameraBoundary {

        public bool limitX;
        public float minX;
        public float maxX;

        public bool limitY;
        public float minY;
        public float maxY;

    } public CameraBoundary cameraBoundary; // Limites nos quais a camera dessa cena tem que ficar no eixo-x.

    // Faz com qu eo player comece sem uma habilidade.
    public bool noAbility = false;

    // Nome da cena seguinte á atual.
    public string nextSceneName;

    private void Awake () {

        // Pega uma referência estática a esse script.
        currentSceneScript = this;

    }

    // Carrega a próxima cena.
    public void NextScene () {

        GameController.gameController.LoadScene(nextSceneName);

    }
}
