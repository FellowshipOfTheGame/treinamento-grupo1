using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[AddComponentMenu("Scripts/Utility/Activators/On Collision Activate")]
// Trigger usado para ativar ações.
public class ProjectileHitActivator : ActivatorBase {

    //Trigger ativa quando acertado por um projectile certo projetil
	public GameObject projetil;
    private void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag == "Projectile"){
        	if (projetil == null || collision.gameObject == projetil){
				Destroy(collision.gameObject);
            	ActivateTargets();
			}
		}
    }
}
