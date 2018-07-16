using UnityEngine;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Scripts/Player/Camera Script")]
// Controla a camera principal do player.
public class CameraScript : MonoBehaviour {

    // Objeto que a camera seguirá (a.k.a. Player).
    private Transform target;
    // Limite da fase (A camera não passará dessa coordenas).
    // OBS: É necessário que exista um objeto na cena contendo um SceneScript que possua essa informação.
    private SceneScript.CameraBoundary boundary;

	private void Start () {

        // Encontra o player a ser seguido.
        target = Player.player.transform;

        // Dá um erro e retorna se não ouver um SceneScript na cena atual.
        if (SceneScript.currentSceneScript == null) {
            Debug.LogError("(CameraScript) A valid SceneScript containing a CameraBoundary was not found on current scene!");
            return;
        }

        // Pega os limites da fase a partir de um SceneScript.
        boundary = SceneScript.currentSceneScript.cameraBoundary;
        
    }

    private void Update () {

        // Faz com que a camera siga o player dentro dos limites da fase.
        transform.position = new Vector3(Mathf.Clamp(target.position.x, boundary.minX, boundary.maxX), target.position.y, transform.position.z);

	}
}
