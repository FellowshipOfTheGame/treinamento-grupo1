using System.Collections;
using UnityEngine;


[AddComponentMenu("Scripts/Utility/Actions/Destroy")]
public class tocarSom: ActionBase {

    //sons a serem tocados
    public string[] nome;
	public bool[] sfx;//booleana o qual afirma se nome[indexAtual] eh ou não um sfx
    public override void Activate() {

        base.Activate();

		//toca os sons
        for (int i = 0; i < nome.Length; i++)
            FindObjectsOfType<AudioManager>()[0].GetComponent<AudioManager>().play(nome[i],sfx[i]);

    }
}
