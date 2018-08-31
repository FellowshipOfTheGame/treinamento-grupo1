using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ativador por timer
public class TimerActivator : ActivatorBase {

	public float cooldown;
	public bool ativarOnstart;
	void Start () {
		if(ativarOnstart)
			ativar();
	}
	
	public void ativar(){
		StartCoroutine(subrotina());
	}
	IEnumerator subrotina(){

		//espera cooldown
 		yield return new WaitForSeconds(cooldown);

		 //ativa as açoes
		ActivateTargets();
	}
}
