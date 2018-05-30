using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Scripts/Player/Player")]
// Controla o player.
public class Player : MonoBehaviour {

    // Singleton para o player.
    public static Player player = null;

    // Guarda o input passado atrav�s de um PlayerInput.
    [System.Serializable]
    public class Input {

        public float horizontalAxis = 0.0f;
        public bool jumpButton = false;
        public bool useButton = false;

    } public Input input;

    // Deve conter todas as habilidades que o player pode usar.
    public List<Hability> habilities;
    public string currentHability = "none";

    void Awake () {

        // Pega uma refer�ncia ao player no in�co e da um erro caso exista mais de um player.
        if (player == null)
            player = this;
        else
            Debug.LogError("(Player) More than one player found! There should only be a single instance of the player script at any given time!");

    }

    void Update () {

    }

    // Muda a habilidade do player.
    public void SetHability(string hability) {
        
        for(int i = 0; i < habilities.Count; i++) { 
            if(habilities[i]._name == hability) { // Encontra na lista a habilidade desejada.

                currentHability = hability;

                // (N�O IMPLEMENTADO) <<<<------------ Resto das coisas que dev�m acontecer

                return;
            }
        }

        // Mostra um erro caso a habilidade seja inv�lida,
        Debug.LogError("(Player) Hability <+" + hability + "+> is not valid!");

    }
}
