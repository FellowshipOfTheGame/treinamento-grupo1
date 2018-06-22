using UnityEngine;

// Script usado para passar inputs ao player.
[AddComponentMenu("Scripts/Player/Input")]
public class PlayerInput : MonoBehaviour {

    // Player que recebe o input desse script.
    public Player target;

    void Start () {

        // Verfica se foi passado um Player para esse script, se não tenta achar um.
        if(target == null) {
            target = GetComponent<Player>();
            Debug.LogWarning("(Player Input) No Player assigned! This script will attempt to find a Player script but it's recommended to assign it in the Inspector.");
        }

    }

    void Update () {

        // Detecta e passa os inputs do teclado ao player. 
        target.input.horizontalAxis = Input.GetAxis("Horizontal");
        target.input.jumpButton = Input.GetButton("Jump");
        target.input.useButton = Input.GetButton("Use");

    }

}
