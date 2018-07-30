using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Realiza a movimentação do Player.
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Scripts/Player/Player Movement")]
public class PlayerMovement : MonoBehaviour {

    private Player target; // Player que recebe o input desse script.
    private Rigidbody2D _rigidbody; // Rigidbody do target.
    private CapsuleCollider2D _collider; // Collider do target.

    public float hAce = 1.5f; // Aceleracao horizontal(andar).
	public float hMax = 2.0f; // Velocidade horizontal maxima.

    public float jumpHeight = 2.0f; // Altura maxima do pulo

    public LayerMask rayMask; // Mascára usada para filtrar raycasts.

    public bool grounded; // Verifica se está tocando no solo.

    private float hNewVel; // Nova velocidade horizontal do player.
	private float vNewVel; // Nova velocidade vertical do player.


    void Start () {

        // Pega as referências necessárias ao player.
        target = Player.player;
        _rigidbody = target._rigidbody;
        _collider = target._collider;

    }

    void Update () {

        // Movimento horizontal, também garante que o player nunca ultrapasse a velocidade máxima.
        hNewVel = Mathf.Clamp(hAce * target.input.horizontalAxis + _rigidbody.velocity.x, -hMax, hMax);
        // Movimento vertical.
        vNewVel = _rigidbody.velocity.y;            

        // Verifica que esta tocando no chão.
        grounded = Physics2D.CapsuleCast( transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), new Vector2(_collider.size.x * 0.5f, _collider.size.y), CapsuleDirection2D.Vertical, 0, -Vector2.up, 0.05f, rayMask);

        // DEBUG
        #if UNITY_EDITOR
        if (grounded)
            Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) - new Vector3(0, _collider.size.y / 2, 0), transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) - new Vector3(0, _collider.size.y / 2, 0) - new Vector3(0, _collider.size.y * 0.05f, 0), Color.red);
        else
            Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) - new Vector3(0, _collider.size.y / 2, 0), transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) - new Vector3(0, _collider.size.y / 2, 0) - new Vector3(0, _collider.size.y * 0.05f, 0), Color.green);
        #endif

        // Verifica se o player foi esmagado.
        if (grounded) {
            bool crushed = Physics2D.Raycast(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0), Vector2.up, _collider.size.y * 0.05f, rayMask);
            if (crushed)
                Debug.Log("Player was crushed!");

            // DEBUG
            #if UNITY_EDITOR
            if (crushed)
                Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0), transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0) + new Vector3(0, _collider.size.y * 0.05f, 0), Color.red);
            else
                Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0), transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0) + new Vector3(0, _collider.size.y * 0.05f, 0), Color.green);
            #endif
        }

        // Verifica se o Player está tentando andar contra uma parede, usado para evitar que ele possa "grudar em paredes" durante um pulo.
        bool blocked = Physics2D.CapsuleCast(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), new Vector2(_collider.size.x, _collider.size.y / 2), CapsuleDirection2D.Vertical, 0, Player.player.facing * Vector2.right, 0.05f, rayMask);

        if (blocked)
            hNewVel = 0;

        // DEBUG
#if UNITY_EDITOR
        if (blocked)
            Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), transform.position + Player.player.facing * (new Vector3(0.05f, 0, 0) + new Vector3(_collider.size.x / 2, 0, 0)), Color.red);
        else
            Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), transform.position + Player.player.facing * (new Vector3(0.05f, 0, 0) + new Vector3(_collider.size.x / 2, 0, 0)), Color.green);
        #endif

        // Evita a soma de addForces no pulo.
        if (target.input.jumpButton && grounded)
            vNewVel = 0;

        // Aplica a velocidade ao player.
        _rigidbody.velocity = new Vector2(hNewVel, vNewVel);

        // Realiza o pulo.
        if (target.input.jumpButton && grounded)
            _rigidbody.AddForce( _rigidbody.mass * Mathf.Sqrt(2 * jumpHeight * 10) * Vector2.up, ForceMode2D.Impulse);	

	}

    //"colando" player a plataforma
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Plataform")
            transform.SetParent (other.transform);
    }

    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.tag == "Plataform")
            transform.SetParent (null);
    }
}
