using UnityEngine;

[AddComponentMenu("Scripts/Utility/Projectile")]
public class Projectile : MonoBehaviour {

    public float velocity = 7.5f;
    public Rigidbody2D _rigidbody;

    void Start() {

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        Vector3 vel = new Vector3(0, velocity, 0);

        _rigidbody.velocity = transform.InverseTransformDirection(vel);

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        Destroy(gameObject);

    }
}
