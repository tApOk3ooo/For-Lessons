using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 3.0f;
    [SerializeField] private int _damage = 25;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    public void Launch(Vector2 direction, float force)
    {
        _rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HandleHit(other.gameObject);
    }

    private void HandleHit(GameObject target)
    {
        if (target.CompareTag("Player"))
        {
            return;
        }

        if (target.TryGetComponent<Health>(out var health))
        {
            health.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }
}
