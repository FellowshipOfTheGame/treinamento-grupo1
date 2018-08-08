using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//borda do mundo, caso player toque nela, morra
public class BordaDoMundo : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().morreu();

    }
}
