using UnityEngine;
using System.Collections.Generic;

// Destrói os objetos alvo.
[AddComponentMenu("Scripts/Utility/Actions/Destroy")]
public class DestroyAction : ActionBase {

    // Lista de objetos a serem destruídos.
    public List<Object> targets;

    public override void Activate() {

        base.Activate();

        // Passa pela lista e destrói os objetos.
        for (int i = 0; i < targets.Count; i++)
            Destroy(targets[i]);

    }
}
