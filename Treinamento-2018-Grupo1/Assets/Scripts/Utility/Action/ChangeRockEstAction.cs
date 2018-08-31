using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Utility/Actions/Change Rock Est")]
public class ChangeRockEstAction : ActionBase {

	public List<RockMovement> targets;
	
	public override void Activate() {

        base.Activate();

        //muda o estado das rochas
        for (int i = 0; i < targets.Count; i++)
            targets[i].mudarEst();
    }
}
