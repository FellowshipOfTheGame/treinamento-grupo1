using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[AddComponentMenu("Scripts/Utility/Activators/On Collision Activate")]
// Trigger usado para ativar ações.
public class OnCollisionActivate : ActivatorBase {

    // Se diferente de "none", esse trigger só é ativado por objetos com essa tag.
    public string targetTag = "none";

    private void OnCollisionEnter2D(Collision2D collision) {

        if (targetTag == "none" || collision.gameObject.tag == targetTag)
            ActivateTargets();

    }
}
