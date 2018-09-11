using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraTarget : ActionBase {

    // Lista de objetos a serem destruídos.
    public GameObject novoTarget;

    public override void Activate() {

        base.Activate();
		Player aux = FindObjectsOfType<Player>()[0];
		//FindObjectsOfType<PlayerMovement>()[0].desacelerar();
		//FindObjectsOfType<PauseMenu>()[0].creditosIniciados = true;
		novoTarget.transform.position = aux.transform.position;
        FindObjectsOfType<CameraScript>()[0].ChangeTarget(novoTarget);

    }
}
