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

    // Script que controla a moviumenta��o do player.
    public PlayerMovement _movement;

    // SpriteRenderer do player.
    public SpriteRenderer _renderer;

    // Animator do player.
    public Animator _animator;

    // Rigidbody do player.
    public Rigidbody2D _rigidbody;

    // Collider do player.
    public CapsuleCollider2D _collider;

    // Usado para ajustar o tamanho da camera.
    [Space(10)]
    public int pixelsPerUnityRate = 64;

    [HideInInspector]
    public float velY;

    // Guarda o input passado atrav�s de um PlayerInput.
    [System.Serializable]
    public class UserInput {

        public float horizontalAxis = 0.0f;
        public float verticalAxis = 0.0f;
        public bool jumpButton = false;
        public bool useButton = false;
        public bool fireButton = false;

    }
    [Space(10)]
    public UserInput input;

    // Guarda os efeitos visuais produzidos pelo player..
    [System.Serializable]
    public class Effects {

        public SpriteRenderer habilityEffect;

    }
    [Space(10)]
    public Effects effects;

    // Deve conter todas as habilidades que o player pode usar.
    [Space(10)]
    public string startingAbility = "fire";
    public List<Ability> habilities;
    public Ability currentAbility = null;

    // Spawns dos proj�teis do player. Coloca-los começando da direita em sentido horário.
    [Space(10)]
    public Transform[] projectileSpawns;

    // Usado para contar o delay entre disparo de projéteis.
    private float abilityDelay = 0;

    // Guarda para que lado o player est� olhando.
    [Space(10)]
    public int facing = 1;

    private void Awake () {

        DontDestroyOnLoad(gameObject);

        // Pega uma refer�ncia ao player no iníco e da um erro caso exista mais de um player.
        if (player == null)
            player = this;
        else
            Debug.LogError("(Player) More than one player found! There should only be a single instance of the player script at any given time!");

        // Força um poder inicial o player (none = sem poder inicial).
        if (startingAbility != "none" && !SceneScript.currentSceneScript.noAbility) {
            SetAbility(startingAbility);
            effects.habilityEffect.enabled = true;
        }

        /*
        // Ajusta a camera para o tamanho apropriado baseado na resolução.
        CameraAdjustments.Adjust(Camera.main, pixelsPerUnityRate);
        */

        // Verfica se foi passado um PlayerMovement para esse script, se n�o tenta encontrar um.
        if (_movement == null)
            _movement = GetComponent<PlayerMovement>();

        // Verfica se foi passado um SpriteRenderer para esse script, se n�o tenta encontrar um.
        if (_renderer == null)
            _renderer = GetComponentInChildren<SpriteRenderer>();

        // Verfica se foi passado um Animator para esse script, se n�o tenta encontrar um.
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();

        // Verfica se foi passado um Rigidbody2D para esse script, se n�o tenta encontrar um.
        if (_movement == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        // Verfica se foi passado um Collider2D para esse script, se n�o tenta encontrar um.
        if (_renderer == null)
            _collider = GetComponentInChildren<CapsuleCollider2D>();

    }

    private void Update () {

        if (GameController.gameController == null)
            return;


        // Toca a animação de andar.
        if (Mathf.Abs(input.horizontalAxis) != 0)
            _animator.SetBool("Walking", true);       
        else
            _animator.SetBool("Walking", false);
        
        // Toca a animação de estar no ar.
        if (_movement.grounded)
            _animator.SetBool("InAir", false);
        else
            _animator.SetBool("InAir", true);

        // Controla o delay entre o uso de abilidades.
        if(abilityDelay > 0)
            abilityDelay -= Time.deltaTime;


        if (GameController.gameController.currentState == GameController.GameState.Play) {

            // Pausa o jogo.
            if (Input.GetButtonDown("Cancel"))
                GameController.gameController.PauseGame();


            // Controla para que lado o player está olhando.
            if (input.horizontalAxis > 0)
                facing = 1;
            else if (input.horizontalAxis < 0)
                facing = -1;

            // Faz o player usar habilidades.
            if (input.fireButton) {

                // Faz o player disparar projéteis.
                if (currentAbility != null && currentAbility.type == Ability.Type.Projectile) {

                    if (abilityDelay <= 0) {
                        if (input.verticalAxis > 0) {
                            Instantiate(currentAbility.projectileSettings._object, projectileSpawns[1].position, projectileSpawns[1].rotation);
                            abilityDelay = currentAbility.projectileSettings.delay;
                        }
                        else if (facing == 1) {
                            Instantiate(currentAbility.projectileSettings._object, projectileSpawns[0].position, projectileSpawns[0].rotation);
                            abilityDelay = currentAbility.projectileSettings.delay;
                        }
                        else if (facing == -1) {
                            Instantiate(currentAbility.projectileSettings._object, projectileSpawns[2].position, projectileSpawns[2].rotation);
                            abilityDelay = currentAbility.projectileSettings.delay;
                        }
                    }

                }
            }

        } else if (GameController.gameController.currentState == GameController.GameState.Paused) {
            // Despausa o jogo.
            if (Input.GetButtonDown("Cancel"))
                GameController.gameController.UnpauseGame();
        }

        if (facing == -1)
            _renderer.flipX = true;
        else
            _renderer.flipX = false;

    }

    // Muda a habilidade do player.
    public void SetAbility(string hability) {

        for(int i = 0; i < habilities.Count; i++) { 
            if(habilities[i]._name == hability) { // Encontra na lista a habilidade desejada.

                currentAbility = habilities[i];

                // Mostra o efeito da habilidade.
                if (hability != "none")
                    effects.habilityEffect.enabled = true;

                effects.habilityEffect.sprite = currentAbility.displayEffect;

                return;
            }
        }

        // Mostra um erro caso a habilidade seja inv�lida,
        Debug.LogError("(Player) Hability <" + hability + "> is not valid!");

        

    }

    //Respawna o player
    public void Death() {

        GameController.gameController.currentState = GameController.GameState.Dead;
        GameController.gameController.deathScreen.SetActive(true);
        Destroy(gameObject);

    }

    void LateUpdate(){
        velY = _rigidbody.velocity.y;
    }
}
