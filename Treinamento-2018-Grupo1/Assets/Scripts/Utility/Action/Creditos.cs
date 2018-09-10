using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditos : ActionBase {

    // Lista de objetos a serem destruídos.
    public GameObject menu;
	public GameObject creditoPosition;

    public override void Activate() {

        base.Activate();
		menu.SetActive(true);
		menu.GetComponent<RectTransform>().SetParent(creditoPosition.transform);

    }
}
