using UnityEngine;

// Temporário: Uma solução melhor da unity sairá em breve.
// Ajusta o tamanho da camera para minimizar problemas com o tamanho dos pixels.
[AddComponentMenu("Scripts/Utility/Camera Adjustments")]
public class CameraAdjustments : MonoBehaviour {

    // Cria uma referêmcia estática a esse script.
    public static CameraAdjustments cameraAdjustments = null;
    // Camera alvo.
    public Camera target;
    // Relação pixels por unidade dos sprites.
    public int pixelsPerUnityRef = 64;

	private void Start () {

        if (cameraAdjustments = null) { // Cria uma referência a esse script e ajusta a camera.

            cameraAdjustments = this;

            Adjust(target, pixelsPerUnityRef);

        } else { // Se uma referência já existir, a destrói e usa a referência antiga para ajustar essa camera.

            Adjust(target, pixelsPerUnityRef);

            Destroy(this);
        }

    }

    // Faz o ajuste no tamanho ortográfico.
    public static void Adjust (Camera cam, int pRef) {

        cam.orthographicSize = Screen.height / (2 * pRef);

    }
}
