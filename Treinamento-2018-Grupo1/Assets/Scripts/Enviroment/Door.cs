using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//realiza mesma movimentacao de rocha1, no entanto quando chega na posicao final para
public class Door : RockMovement{
	protected override void chegouPosFinal(){
		ativo = 0;
	}

}
