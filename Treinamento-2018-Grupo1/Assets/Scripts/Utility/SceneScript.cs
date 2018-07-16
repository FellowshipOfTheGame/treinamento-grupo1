using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scripts/Utility/Scene Script")]
// Guarda informações importantes para a fase atual.
public class SceneScript : MonoBehaviour {

    // Guarda uma referência estática a esse script.
    public static SceneScript currentSceneScript;

    [System.Serializable]
    public struct CameraBoundary {

        public int minX;
        public int maxX;

    } public CameraBoundary cameraBoundary; // Limites nos quais a camera dessa cena tem que ficar no eixo-x.

    // Nome da cena seguinte á atual.
    public string nextSceneName;

    private void Awake () {

        // Pega uma referência estática a esse script.
        currentSceneScript = this;

    }

    // Carrega a próxima cena.
    public void NextScene () {

        SceneManager.LoadScene(nextSceneName);

    }
}
