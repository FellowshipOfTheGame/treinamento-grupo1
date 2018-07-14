using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impulsionarRock2: ActionBase {

	public List<GameObject> targets;
	
	public override void Activate() {

        base.Activate();

        //impulsiona todas as rochas
        for (int i = 0; i < targets.Count; i++)
            targets[i].GetComponent<RockMovement2>().impulsionar();


    }
}