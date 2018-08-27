using UnityEngine;

// Script que faz o efeito de paralax no fundo.
[AddComponentMenu("Scripts/Enviroment/Paralax")]
public class Paralax : MonoBehaviour {

    [System.Serializable]
    public class ParalaxLayer {

        public GameObject layerObject;
        public float x_multiplier = 1;
        public float y_multiplier = 1;

    }

    public ParalaxLayer[] layers;

    void Update () {
        // "Move" cada camada em uma velocidade baseada na camera principal.
        for (int i = 0; i < layers.Length; i++) {

            Vector2 layer_pos = layers[i].layerObject.transform.position;
            layer_pos.x = Camera.main.transform.position.x * layers[i].x_multiplier;
            layer_pos.y = Camera.main.transform.position.y * layers[i].y_multiplier;

            layers[i].layerObject.transform.position = layer_pos;

        }

	}
}
