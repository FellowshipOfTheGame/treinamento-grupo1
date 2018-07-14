using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Realiza a movimentação do Player.
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Scripts/Player/Player Movement")]
public class PlayerMovement : MonoBehaviour {
	
	public float hAce = 1.5f; // Aceleracao horizontal(andar).
	public float hMax = 2.0f; // Velocidade horizontal maxima.

    public float jumpHeight = 2.0f; // Altura maxima do pulo

    public Player target; // Player que recebe o input desse script.
    private Rigidbody2D rigi; // Rigidbody do target.

    public LayerMask rayMask; // Mascára usada para filtrar raycasts.

    public bool grounded; // Verifica se está tocando no solo.

    private float hNewVel; // Nova velocidade horizontal do player.
	private float vNewVel; // Nova velocidade vertical do player.


    void Start () {

        // Verfica se foi passado um Player para esse script, se nao tenta achar um.
        if(target == null) 
            target = GetComponent<Player>();
		
        // Obtem o Rigidbody.
		rigi = target.GetComponent<Rigidbody2D>();

    }

	void Update () {

        // Movimento horizontal, também garante que o player nunca ultrapasse a velocidade máxima.
        hNewVel = Mathf.Clamp(hAce * target.input.horizontalAxis + rigi.velocity.x, -hMax, hMax);
        // Movimento vertical.
        vNewVel = rigi.velocity.y;            

        // Verifica que esta tocando no chão.
        grounded = Physics2D.Raycast(transform.position, -Vector2.up, 1.1f, rayMask);

        //Veiricando se morreu esmagado
        if(grounded ){
            if( Physics2D.Raycast(transform.position, Vector2.up, 1.1f, rayMask)){
                Debug.Log("MORREU ESMAGADO");
            }
        }

        
        // Verifica se o Player está tentando andar contra uma parede, usado para evitar que ele possa "grudar em paredes" durante um pulo.
        if (Physics2D.Raycast(transform.position - new Vector3( 0, 0.95f, 0), Mathf.Sign(hNewVel) * Vector2.right, 0.32f, rayMask))
            hNewVel = 0;

        // Evita a soma de addForces no pulo.
        if (target.input.jumpButton && grounded)
            vNewVel = 0;

        // Aplica a velocidade ao player.
        rigi.velocity = new Vector2(hNewVel, vNewVel);

        // Realiza o pulo.
        if (target.input.jumpButton && grounded)
			rigi.AddForce(Mathf.Sqrt(2 * jumpHeight * 10) * Vector2.up,ForceMode2D.Impulse);	

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
