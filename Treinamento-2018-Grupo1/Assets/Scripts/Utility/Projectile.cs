using UnityEngine;

[AddComponentMenu("Scripts/Utility/Projectile")]
// Script básico para um projétil.
public class Projectile : MonoBehaviour {

    // Velocidade inicial do projétil.
    public float velocity = 7.5f;
    public Rigidbody2D _rigidbody;

    // Tempo de vida máximo do projétil. 0 = INF.
    [Tooltip("0 = Infinity")]
    public float lifeTime = 2.0f;
    // Conta o tempo de vida do projétil.
    private float currentTime = 0.0f;

    private void Start() {

        // Consegue uma referência ao rigidbody do objeto se essa ainda não existir.
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        // Adiciona a velocidade ao objeto em espaço local.
        Vector3 vel = new Vector3(0, velocity, 0);
        _rigidbody.velocity = transform.InverseTransformDirection(vel);

    }

    private void Update() {
        
        // Destrói o objeto depois de um certo tempo de vida.
        if(lifeTime != 0) {

            currentTime += Time.deltaTime;

            if (currentTime >= lifeTime)
                Destroy(gameObject);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        // Destrói o objeto depois de uma colisão.
        Destroy(gameObject);

    }
}
