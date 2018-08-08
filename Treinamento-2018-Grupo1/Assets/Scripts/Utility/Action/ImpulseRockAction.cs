using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Utility/Actions/Impulse Rock")]
public class ImpulseRockAction: ActionBase {

	public List<GameObject> targets;
	
	public override void Activate() {

        base.Activate();

        //impulsiona todas as rochas
        for (int i = 0; i < targets.Count; i++)
            targets[i].GetComponent<RockMovement2>().impulsionar();


    }
}