using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Modifica valor de simulated do rigidbody2d
[AddComponentMenu("Scripts/Utility/Actions/Destroy")]
public class ChangeGravity : ActionBase {


	public int valor;
    // Lista de objetos a serem destruídos.
    public List<Rigidbody2D> targets;

    public override void Activate() {
        base.Activate();

        for (int i = 0; i < targets.Count; i++)
            targets[i].gravityScale= valor;
	}

}
