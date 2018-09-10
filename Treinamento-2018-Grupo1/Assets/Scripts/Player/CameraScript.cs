using UnityEngine;

[RequireComponent(typeof(Camera))]
[AddComponentMenu("Scripts/Player/Camera Script")]
// Controla a camera principal do player.
public class CameraScript : MonoBehaviour {

    // Objeto que a camera seguirá (a.k.a. Player).
    private Transform seekTarget;
    // Limite da fase (A camera não passará dessa coordenas).
    // OBS: É necessário que exista um objeto na cena contendo um SceneScript que possua essa informação.
    private SceneScript.CameraBoundary boundary;

	public void UpdateCameraScript (Transform target) {

        // Encontra o player a ser seguido.
        seekTarget = target;

        // Dá um erro e retorna se não ouver um SceneScript na cena atual.
        if (SceneScript.currentSceneScript == null) {
            Debug.LogError("(CameraScript) A valid SceneScript containing a CameraBoundary was not found on current scene!");
            return;
        }

        // Pega os limites da fase a partir de um SceneScript.
        boundary = SceneScript.currentSceneScript.cameraBoundary;
        
    }

    private void Update () {

        if (seekTarget == null)
            return;

        // Faz com que a camera siga o player dentro dos limites da fase.
        float camX = seekTarget.position.x;
        if (boundary.limitX)
            camX = Mathf.Clamp(camX, boundary.minX, boundary.maxX);

        float camY = seekTarget.position.y;
        if (boundary.limitY)
            camY = Mathf.Clamp(camY, boundary.minY, boundary.maxY);

        transform.position = new Vector3(camX, camY, transform.position.z);

	}

    public void ChangeTarget(GameObject novoTarget){
        seekTarget = novoTarget.transform;
    }
}
