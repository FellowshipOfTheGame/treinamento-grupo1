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

        if (GameController.instance == null)
            return;

        if (GameController.instance.currentState == GameController.GameState.Play || GameController.instance.currentState == GameController.GameState.Cutscene) {

            float hNewVel; // Nova velocidade horizontal do player.
            float vNewVel; // Nova velocidade vertical do player.

            // Movimento horizontal, também garante que o player nunca ultrapasse a velocidade máxima.
            if(GameController.instance.currentState == GameController.GameState.Cutscene)
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

                    Player.instance.Death();
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
            bool blocked = Physics2D.CapsuleCast(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), new Vector2(_collider.size.x, _collider.size.y * 0.95f), CapsuleDirection2D.Vertical, 0, Player.instance.facing * Vector2.right, 0.05f, rayMask);

            if (blocked)
                hNewVel = 0;

            // DEBUG
#if UNITY_EDITOR
            if (blocked)
                Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), transform.position + Player.instance.facing * (new Vector3(0.05f, 0, 0) + new Vector3(_collider.size.x / 2, 0, 0)), Color.red);
            else
                Debug.DrawLine(transform.position + new Vector3(_collider.offset.x, _collider.offset.y, 0), transform.position + Player.instance.facing * (new Vector3(0.05f, 0, 0) + new Vector3(_collider.size.x / 2, 0, 0)), Color.green);
#endif

            // Evita a soma de addForces no pulo.
            if (target.input.jumpButton && grounded && GameController.instance.currentState == GameController.GameState.Play)
                vNewVel = 0;

            if(GameController.instance.currentState == GameController.GameState.Cutscene){
                hNewVel = velocity;
            }

            // Aplica a velocidade ao player.
            _rigidbody.velocity = new Vector2(hNewVel, vNewVel);

            // Realiza o pulo.
            if (target.input.jumpButton && grounded && GameController.instance.currentState == GameController.GameState.Play)
                _rigidbody.AddForce(_rigidbody.mass * Mathf.Sqrt(2 * jumpHeight * 10) * Vector2.up, ForceMode2D.Impulse);
        }
	}

    // Usado para "colar" o player em uma plataforma que se mova.
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Plataform")
            transform.SetParent(other.transform);
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Plataform")
            transform.SetParent(null);
    }


    //Usado para parar o player nos do créditos.
    IEnumerator StopPlayer() {

        while (velocity > 0 || jumpHeight > 0) {
            yield return new WaitForSeconds(0.1f);
            velocity -= 0.17f;
            jumpHeight -= 0.11f;
            if (velocity < 0f) {
                StopCoroutine(StopPlayer());
                velocity = 0;
            }
            if (jumpHeight < 0f) jumpHeight = 0;
        }
    }

    // Chama as funções necessárias relacionadas ao player em uma cutscene.
    public void StartCutscene() {
        StartCoroutine(StopPlayer());
    }

}