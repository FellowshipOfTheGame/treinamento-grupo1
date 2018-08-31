using UnityEngine;

[AddComponentMenu("Scripts/Utility/Actions/Play Sound")]
public class PlaySoundAction : ActionBase {

    // Sons a serem tocados.
    public acharSound[] sons;


    public override void Activate() {

        base.Activate();

		// Toca os sons.
        for (int i = 0; i < sons.Length; i++)
            FindObjectsOfType<AudioManager>()[0].GetComponent<AudioManager>().play(sons[i].nome,sons[i].sfx);

    }
}
