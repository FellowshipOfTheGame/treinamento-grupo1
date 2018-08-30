/*using UnityEngine;

//modifica ponto de respawn caso em contato
[AddComponentMenu("Scripts/Utility/Respawn Point")]
public class RespawnPoint : MonoBehaviour {

	public string hability;//habilida o qual o player terá quando respawnado
    public Transform respawnPosition;//posicao onde player será respawnado

    void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.tag == "Player")
            Player.player.respawnPoint = gameObject;

    }
}
*/