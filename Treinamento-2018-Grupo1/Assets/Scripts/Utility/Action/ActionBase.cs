using UnityEngine;
using System.Collections.Generic;

// Base para os scripts que realizam ações no jogo.
[AddComponentMenu("Scripts/Utility/Actions/Base")]
public class ActionBase : MonoBehaviour {

    // Destrói esse script depois de um certo número de usos (0 = Destruído imediatamente / Números negativos = infinito).
    public int maxUses = 1;

    // Lista de scripts que devêm ser ativados junto com este.
    public List<ActionBase> secondaryTargets;

    // Ativa esse script para fazer alguma ação. Essa ação deve ser colocada em um override de uma classe filha.
    // Lembrar de sempre usar base.Activate() para executar o for.
    public virtual void Activate() {

        // Ativa os scripts da lista de alvos secundários.
        for (int i = 0; i < secondaryTargets.Count; i++)
            secondaryTargets[i].Activate();

        // Destói o script depois de um certo número de usos.
        maxUses--;
        if (maxUses == 0)
            Destroy(this);

    }

}
