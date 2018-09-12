using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[AddComponentMenu("Scripts/Utility/Activators/Use Activate")]
// Ativador usado para objetos que o player pode interagir sempre.
public class UseActivate : ActivatorBase {

    // Guarda se o player está na área em que pode ativar esse script.
    private bool isOnRange = false;

    // Verifica quando o player entra na área alvo.
    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            isOnRange = true;

    }

    // Verifica quando o player sai da área alvo.
    void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            isOnRange = false;

    }

    void Update() {
        
        // Verifica se o player está dentro da área de ativação.
        if(isOnRange) {
            if (Player.instance.input.useButton) // Detecta quando o player tenta usar este objeto.
                ActivateTargets();
        }

    }
}
