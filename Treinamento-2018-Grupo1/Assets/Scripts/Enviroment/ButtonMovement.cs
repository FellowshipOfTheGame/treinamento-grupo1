using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//anda botao até ponto final e ativa as funcoes
public class ButtonMovement : RockMovement {


	protected override void chegouPosFinal(){
		ativo = 0;

		base.chegouPosFinal();
	}

	protected override void chegouPosInicial(){
		ativo = 0;
		ButtonActivator butAct = GetComponent<ButtonActivator>();
		if(butAct != null)
			butAct.ativo = true;
		base.chegouPosInicial();
	}

}
