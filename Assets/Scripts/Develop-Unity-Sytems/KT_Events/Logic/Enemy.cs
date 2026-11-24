using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 30;
    private GameManager _gm;

    public void Init(GameManager gm)
    {
        _gm = gm;
        _gm.EnemyDamageTick += TakeDamage;
    }

    private void TakeDamage(int damage)
    {
        _health -= damage;
        print($"[Враг {name}] Получил урон: {damage}. Осталось здоровья: {_health}");

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"[Враг {name}] помер!");

        _gm.EnemyDamageTick -= TakeDamage;

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _gm.EnemyDamageTick -= TakeDamage;
    }
}
