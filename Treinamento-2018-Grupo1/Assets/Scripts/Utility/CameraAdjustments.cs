using UnityEngine;

// Temporário: Uma solução melhor da unity sairá em breve.
// Ajusta o tamanho da camera para minimizar problemas com o tamanho dos pixels.
[AddComponentMenu("Scripts/Utility/Camera Adjustments")]
public static class CameraAdjustments {

    // Faz o ajuste no tamanho ortográfico.
    public static void Adjust (Camera cam, int pRef) {

        cam.orthographicSize = Screen.height / (2 * pRef);

    }
}
