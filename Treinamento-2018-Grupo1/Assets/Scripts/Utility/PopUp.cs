using UnityEngine;

// Esse script faz com que um sprite aparece na tela quando o player está no trigger.
[AddComponentMenu("Scripts/Utility/Pop Up")]
public class PopUp : MonoBehaviour {

    public SpriteRenderer target;

    // Verifica quando o player entra na área.
    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            target.enabled = true;

    }

    // Verifica quando o player sai da área.
    void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            target.enabled = false;

    }
}
