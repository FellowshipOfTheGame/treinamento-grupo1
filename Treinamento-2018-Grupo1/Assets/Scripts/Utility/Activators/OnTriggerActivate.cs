using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[AddComponentMenu("Scripts/Utility/Activators/On Trigger Activate")]
// Trigger usado para ativar ações.
public class OnTriggerActivate : ActivatorBase {

    // Se diferente de "none", esse trigger só é ativado por objetos com essa tag.
    public string targetTag = "none";

    void OnTriggerEnter2D(Collider2D collision) {

        if (targetTag == "none" || collision.gameObject.tag == targetTag)
            ActivateTargets();

    }
}
