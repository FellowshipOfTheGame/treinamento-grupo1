using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Utility/Action/Start Cooldown")]
public class IniciarCooldown : ActionBase {

    // Lista de objetos a serem destruídos.
    public List<TimerActivator> targets;

    public override void Activate() {

        base.Activate();

        // Passa pela lista e destrói os objetos.
        for (int i = 0; i < targets.Count; i++)
            targets[i].ativar();

    }
}