using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//muda posicao final de rock Movement 2
public class ChangepointRockMov2: ActionBase {

[System.Serializable]
public class alvos{
    public RockMovement2 obj;//rock movement com posicao final
    public Transform Novapos;//nova posicao
}
	public List<alvos> targets;
	
	public override void Activate() {

        base.Activate();

        //muda o estado das rochas
        for (int i = 0; i < targets.Count; i++)
            targets[i].obj.novaPosFinal = targets[i].Novapos.position;
    }
}

