using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu("Scripts/Player/Player")]
// Controla o player.
public class Player : MonoBehaviour {

    // Singleton para o player.
    public static Player player = null;

    // Script que controla a moviumentação do player.
    public PlayerMovement playerMov;

    // SpriteRenderer do player.
    public SpriteRenderer playerRenderer;

    // Animator do player.
    public Animator playerAnim;

    // Guarda o input passado através de um PlayerInput.
    [System.Serializable]
    public class Input {

        public float horizontalAxis = 0.0f;
        public float verticalAxis = 0.0f;
        public bool jumpButton = false;
        public bool useButton = false;
        public bool fireButton = false;

    } public Input input;

    // Guarda os efeitos visuais produzidos pelo player..
    [System.Serializable]
    public class Effects {

        public SpriteRenderer habilityEffect;

    }
    public Effects effects;

    // Deve conter todas as habilidades que o player pode usar.
    public List<Hability> habilities;
    public Hability currentHability = null;

    // Spawns dos projéteis do player. Coloca-los começando da direita em sentido horário.
    public Transform[] projectileSpawns;

    // Usado para contar o delay entre disparo de projéteis.
    private float projectileDelay = 0;

    // Guarda para que lado o player está olhando.
    public int playerFacing = 1;

    void Awake () {

        // Pega uma referência ao player no iníco e da um erro caso exista mais de um player.
        if (player == null)
            player = this;
        else
            Debug.LogError("(Player) More than one player found! There should only be a single instance of the player script at any given time!");


        // Verfica se foi passado um PlayerMovement para esse script, se não tenta encontrar um.
        if (playerMov == null)
            playerMov = GetComponent<PlayerMovement>();

        // Verfica se foi passado um SpriteRenderer para esse script, se não tenta encontrar um.
        if (playerRenderer == null)
            playerRenderer = GetComponentInChildren<SpriteRenderer>();

        // Verfica se foi passado um Animator para esse script, se não tenta encontrar um.
        if (playerAnim == null)
            playerAnim = GetComponentInChildren<Animator>();

    }

    void Update () {

        if (Mathf.Abs(input.horizontalAxis) > 0.2f) {
            playerAnim.SetBool("Walking", true);       
        } else
            playerAnim.SetBool("Walking", false);


        if (input.horizontalAxis > 0)
            playerFacing = 1;
        else if (input.horizontalAxis < 0)
            playerFacing = -1;
            
        if (playerMov.grounded)
            playerAnim.SetBool("InAir", false);
        else
            playerAnim.SetBool("InAir", true);

        if(projectileDelay > 0)
            projectileDelay -= Time.deltaTime;


        if (input.fireButton) {

            if(currentHability != null && currentHability.type == Hability.Type.Projectile) {

                if (projectileDelay <= 0) {
                    if(input.verticalAxis > 0) {
                        Instantiate(currentHability.projectileSettings._object, projectileSpawns[1].position, projectileSpawns[1].rotation);
                        projectileDelay = currentHability.projectileSettings.delay;
                    } else if (playerFacing == 1) {
                        Instantiate(currentHability.projectileSettings._object, projectileSpawns[0].position, projectileSpawns[0].rotation);
                        projectileDelay = currentHability.projectileSettings.delay;
                    }
                    else if (playerFacing == -1) {
                        Instantiate(currentHability.projectileSettings._object, projectileSpawns[2].position, projectileSpawns[2].rotation);
                        projectileDelay = currentHability.projectileSettings.delay;
                    }
                }

            }

        }

        if (playerFacing == -1)
            playerRenderer.flipX = true;
        else
            playerRenderer.flipX = false;

    }

    // Muda a habilidade do player.
    public void SetHability(string hability) {
        
        for(int i = 0; i < habilities.Count; i++) { 
            if(habilities[i]._name == hability) { // Encontra na lista a habilidade desejada.

                currentHability = habilities[i];

                // (NÃO IMPLEMENTADO) <<<<------------ Resto das coisas que devêm acontecer

                effects.habilityEffect.sprite = currentHability.displayEffect;

                return;
            }
        }

        // Mostra um erro caso a habilidade seja inválida,
        Debug.LogError("(Player) Hability <+" + hability + "+> is not valid!");

    }
}
