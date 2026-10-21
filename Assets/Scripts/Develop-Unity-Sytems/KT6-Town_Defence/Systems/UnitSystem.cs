using UnityEngine;

public class UnitSystem : MonoBehaviour
{
    [SerializeField] private UnitScriptableObjectScript _unitData;


    private float _currentHealth;
    private Transform _currentPosition;
    private bool _isCanAttack = false;


    private void Initialize()
    {
        if (_unitData == null)
        {
            print("Не найден _unitData");
        }

        _currentHealth = _unitData.MaxHealth;
    }

    private void Attack()
    {
        if (_currentPosition.transform.position.magnitude < _unitData.AttackRange)
        {
            _isCanAttack = true;
        }

        if (_isCanAttack == true)
        {
            
        }
    }

    private void TakeDamage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}