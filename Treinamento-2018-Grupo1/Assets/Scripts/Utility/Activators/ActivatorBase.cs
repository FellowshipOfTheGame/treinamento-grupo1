using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Scripts/Utility/Activators/Base")]
// Base para os scripts que ativam ações no jogo.
public class ActivatorBase : MonoBehaviour {

    // Destrói esse script depois de um certo número de usos (0 = Destruído imediatamente / Números negativos = infinito).
    public int maxUses = 1;

    // Lista de scripts que devêm ser ativados por este.
    public List<ActionBase> targets;
   
    // Ativa os scripts, nas classes filhas basta chamar essa função de algum lugar baseada em uma condição desejada.
    public void ActivateTargets() {

        // Ativa os scripts da lista de alvos.
        for (int i = 0; i < targets.Count; i++)
            targets[i].Activate();

        // Destói o script depois de um certo número de usos.
        maxUses--;
        if (maxUses == 0)
            Destroy(this);

    }

}
