using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Scripts/Player/Player")]
public class Player : MonoBehaviour {

    [System.Serializable]
    public class Input {

        public float horizontalAxis;
        public bool jumpButton;

    } public Input input;

    public List<Hability> habilities;

    void Start () {

    }

    void Update () {

    }

}
