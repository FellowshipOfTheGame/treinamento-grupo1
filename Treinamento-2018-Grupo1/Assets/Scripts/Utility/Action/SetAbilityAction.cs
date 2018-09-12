using UnityEngine;

// Seta a abilidade do player.
[AddComponentMenu("Scripts/Utility/Actions/Set Ability")]
public class SetAbilityAction : ActionBase {

    public string ability = "none";

    public override void Activate() {

        base.Activate();

        Player.instance.SetAbility(ability);

    }
}
