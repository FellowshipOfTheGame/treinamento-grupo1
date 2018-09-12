using UnityEngine;
using System.Collections;


// Realiza a movimentação do Player.
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Scripts/Player/Player Movement")]
public class PlayerMovement : MonoBehaviour {

    public Player target; // Player que recebe o input desse script.
    private Rigidbody2D _rigidbody; // Rigidbody do target.
    private CapsuleCollider2D _collider; // Collider do target.

    public float velocity = 3.0f; // Velocidade do player.
    public float airAcelerationMultiplier = 0.025f; // Modifica a aceleração do player no ar.
    public float jumpHeight = 2.0f; // Altura maxima do pulo

    public LayerMask rayMask; // Mascára usada para filtrar raycasts.

    public bool grounded; // Verifica se está tocando no solo.


    void Start () {

        // Pega as referências necessárias ao player.
        //target = Player.player;
        _rigidbody = target._rigidbody;
        _collider = target._collider;

    }

    void Update () {

        if (GameController.gameController == null)
            return;

        if (GameController.gameController.currentState == GameController.GameState.Play || GameController.gameController.currentState == GameController.GameState.Cutscene) {

            float hNewVel; // Nova velocidade horizontal do player.
            float vNewVel; // Nova velocidade vertical do player.

            // Movimento horizontal, também garante que o player nunca ultrapasse a velocidade máxima.
            if(GameController.gameController.currentState == GameController.GameState.Cutscene)
                target.input.verticalAxis = 0f;

            if (grounded)
                hNewVel = target.input.horizontalAxis * velocity;
            else
                hNewVel = Mathf.Clamp(_rigidbody.velocity.x + Mathf.CeilToInt(target.input.horizontalAxis * velocity) * airAcelerationMultiplier, -velocity, velocity);
            // Movimento vertical.
            vNewVel = _rigidbody.velocity.y;

            // Verifica que esta tocando no chão.
            grounded = Physics2D.CapsuleCast(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), new Vector2(_collider.size.x, _collider.size.y), CapsuleDirection2D.Vertical, 0, -Vector2.up, 0.1f, rayMask);

            // DEBUG
#if UNITY_EDITOR
            if (grounded) {
                Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) - new Vector3(0, _collider.size.y / 2, 0), transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) - new Vector3(0, _collider.size.y / 2, 0) - new Vector3(0, _collider.size.y * 0.075f, 0), Color.red);
            }
            else
                Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) - new Vector3(0, _collider.size.y / 2, 0), transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) - new Vector3(0, _collider.size.y / 2, 0) - new Vector3(0, _collider.size.y * 0.075f, 0), Color.green);
#endif

            // Verifica se o player foi esmagado.
            if (grounded) {

                bool crushed = false;

                RaycastHit2D ray;
                if (ray = Physics2D.Raycast(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0), Vector2.up, _collider.size.y * 0.03f, rayMask))
                    if (ray.collider.tag == "Plataform")
                        crushed = true;

                if (crushed) {

                    Player.player.Death();
                }

                // DEBUG
#if UNITY_EDITOR
                if (crushed)
                    Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0), transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0) + new Vector3(0, _collider.size.y * 0.05f, 0), Color.red);
                else
                    Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0), transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0) + new Vector3(0, _collider.size.y / 2, 0) + new Vector3(0, _collider.size.y * 0.05f, 0), Color.green);
#endif
            }

            // Verifica se o Player está tentando andar contra uma parede, usado para evitar que ele possa "grudar em paredes" durante um pulo.
            bool blocked = Physics2D.CapsuleCast(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), new Vector2(_collider.size.x, _collider.size.y * 0.95f), CapsuleDirection2D.Vertical, 0, Player.player.facing * Vector2.right, 0.05f, rayMask);

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

            if(GameController.gameController.currentState == GameController.GameState.Cutscene){
                hNewVel = velocity;
            }

            // Aplica a velocidade ao player.
            _rigidbody.velocity = new Vector2(hNewVel, vNewVel);

            // Realiza o pulo.
            if (target.input.jumpButton && grounded)
                _rigidbody.AddForce(_rigidbody.mass * Mathf.Sqrt(2 * jumpHeight * 10) * Vector2.up, ForceMode2D.Impulse);
        }
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


    //subrotina e funcao utilizada como parte do credito
    IEnumerator subrotina(){

        while(velocity > 0 || jumpHeight > 0){
 		    yield return new WaitForSeconds(0.1f);
            velocity -= 0.17f;
            jumpHeight -= 0.11f;
            if (velocity < 0f) {
                StopCoroutine(subrotina());
                velocity = 0;
            }
            if(jumpHeight < 0f)jumpHeight = 0 ;
        }
	}

    public void desacelerar(){
        StartCoroutine(subrotina());
    }
    
}