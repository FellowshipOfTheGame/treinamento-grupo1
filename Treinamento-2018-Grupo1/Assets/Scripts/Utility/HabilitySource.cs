using UnityEngine;

// Esse script serve como fonte de habilidades para o player.
[AddComponentMenu("Scripts/Utility/Hability Source")]
public class HabilitySource : MonoBehaviour {

    // Nome da habilidade a ser dada ao player. Deve estar contida entre as habilidades adicionadas na lista de habilidades
    // no script do player.
    public string habilityName = "newHability";

    // Guarda se o player está na área em que pode ativar esse script.
    private bool isOnRange = false;

    // Verifica quando o player entra na área.
    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            isOnRange = true;

    }

    // Verifica quando o player sai da área.
    void OnTriggerExit2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            isOnRange = false;

    }

    void Update() {

        // Verifica se o player está dentro da área de ativação.
        if(isOnRange)
            if (Player.player.input.useButton) // Detecta quando o player tenta usar este objeto.
                Player.player.SetHability(habilityName);

    }
}
