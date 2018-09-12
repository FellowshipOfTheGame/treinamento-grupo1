using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[AddComponentMenu("Scripts/Utility/Activators/Use Hability Activate")]
// Ativador usado para objetos que o player pode interagir quando está com certa habilidade.
public class UseHabilityActivate : ActivatorBase {

    // Nome da habilidade capaz de ativar esse script.
    public string targetHabilty = "none";
    public float cooldown = 0;
    private bool pdAtivar = true;

    // Guarda se o player está na área em que pode ativar esse script.
    private bool isOnRange = false;

    // Verifica quando o player entra na área alvo.
    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            isOnRange = true;

    }

    // Verifica quando o player sai da área alvo.
    void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            isOnRange = false;

    }

    void Update() {
        
        // Verifica se o player está dentro da área de ativação e com a habilidade certa.
        if(isOnRange ) {
            if(targetHabilty ==  "none" || (Player.instance.currentAbility != null && Player.instance.currentAbility._name == targetHabilty)){
                if (Player.instance.input.useButton && pdAtivar){ // Detecta quando o player tenta usar este objeto.
                    ActivateTargets();
                   StartCoroutine( comecarCooldown());
                }
            }
        }

    }


    IEnumerator comecarCooldown(){
        pdAtivar = false;//desativar ate termianr cooldown
 		yield return new WaitForSeconds(cooldown);
        pdAtivar = true;//ativa denovo

	}

}
