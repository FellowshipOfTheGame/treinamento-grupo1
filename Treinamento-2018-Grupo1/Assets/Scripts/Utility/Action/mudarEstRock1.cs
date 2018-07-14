using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mudarEstRock1 : ActionBase {

	public List<GameObject> targets;
	
	public override void Activate() {

        base.Activate();

        //muda o estado das rochas
        for (int i = 0; i < targets.Count; i++)
            targets[i].GetComponent<RockMovement>().mudarEst();
    }
}
