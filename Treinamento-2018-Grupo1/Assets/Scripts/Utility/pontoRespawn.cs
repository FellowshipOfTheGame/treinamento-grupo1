using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//modifica ponto de respawn caso em contato
public class pontoRespawn : MonoBehaviour {

	public string hability;//habilida o qual o player terá quando respawnado
    public GameObject posicao;//posicao onde player será respawnado

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().pontoRespawn = gameObject;

    }
}
