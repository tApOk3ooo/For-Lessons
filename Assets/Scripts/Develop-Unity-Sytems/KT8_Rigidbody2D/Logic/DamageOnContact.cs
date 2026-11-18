using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] private int _damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out var targetHealth))
        {
            targetHealth.TakeDamage(_damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Health>(out var targetHealth))
        {
            targetHealth.TakeDamage(_damage);
        }
    }
}
