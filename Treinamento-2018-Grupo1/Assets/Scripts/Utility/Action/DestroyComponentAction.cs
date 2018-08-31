using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destrói um componente dos objetos alvo.
[AddComponentMenu("Scripts/Utility/Actions/Destroy")]
public class DestroyComponentAction : ActionBase {

    public List<GameObject> targets;
	public string Cnome;
    public override void Activate() {

        base.Activate();

        // Passa pela lista e destrói os objetos.
        for (int i = 0; i < targets.Count; i++)
            Destroy(targets[i].GetComponent(Cnome));

    }
}
