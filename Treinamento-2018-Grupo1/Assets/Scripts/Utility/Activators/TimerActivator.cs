using System.Collections;
using UnityEngine;

//Ativador por timer
[AddComponentMenu("Scripts/Utility/Activators/Button")]
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
