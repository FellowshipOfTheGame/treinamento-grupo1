using UnityEngine;

//borda do mundo, caso player toque nela, morra
[AddComponentMenu("Scripts/Utility/Actions/Kill Player")]
public class KillPlayerAction : ActionBase {
    public override void Activate() {

        base.Activate();

        // Faz o player morrer.
        Player.player.Death();

    }

}