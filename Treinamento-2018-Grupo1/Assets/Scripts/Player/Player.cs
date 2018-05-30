using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Scripts/Player/Player")]
// Controla o player.
public class Player : MonoBehaviour {

    // Singleton para o player.
    public static Player player = null;

    // Guarda o input passado através de um PlayerInput.
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

        // Pega uma referência ao player no iníco e da um erro caso exista mais de um player.
        if (player == null)
            player = this;
        else
            Debug.LogError("(Player) More than one player found! There should only be a single instance of the player script at any given time!");

    }

    void Update () {

    }

}
