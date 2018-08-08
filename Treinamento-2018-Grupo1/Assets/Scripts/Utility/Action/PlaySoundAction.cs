using UnityEngine;

[AddComponentMenu("Scripts/Utility/Actions/Play Sound")]
public class PlaySoundAction : ActionBase {

    // Sons a serem tocados.
    public string[] nome;
	public bool[] sfx;// Booleana o qual afirma se nome[indexAtual] eh ou não um sfx.

    public override void Activate() {

        base.Activate();

		// Toca os sons.
        for (int i = 0; i < nome.Length; i++)
            FindObjectsOfType<AudioManager>()[0].GetComponent<AudioManager>().play(nome[i],sfx[i]);

    }
}
