using UnityEngine;

// Esta ação muda o sprite de um objeto.
[AddComponentMenu("Scripts/Utility/Actions/Base")]
public class ChangeSpriteAction : MonoBehaviour {

    // Novo sprite,
    public Sprite newSprite;
    // Alvo.
    public SpriteRenderer target;

    public virtual void Activate() {

        target.sprite = newSprite;

    }
}
