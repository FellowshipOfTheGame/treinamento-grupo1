using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ativador por timer
public class TimerActivator : ActivatorBase {

	public float cooldown;
	void Start () {
		 StartCoroutine(ativar());
	}
	
	IEnumerator ativar(){

		//espera cooldown
 		yield return new WaitForSeconds(cooldown);

		 //ativa as açoes
		ActivateTargets();
	}
}
