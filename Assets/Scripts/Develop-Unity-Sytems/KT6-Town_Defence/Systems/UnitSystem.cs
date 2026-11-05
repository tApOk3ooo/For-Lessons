using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSystem : MonoBehaviour
{
    private UnitDataSO _data;
    private Transform _defaultTarget;
    private Transform _combatTarget;
    private float _currentHealth;

    // если _combatTarget != null, то узнаём текущее значение _combatTarget и присваиваем _defaultTarget
    public Transform CurrentTaget => _combatTarget != null ? _combatTarget.transform : _defaultTarget; 

    public event Action<UnitSystem> OnDied;

    public void Initialize(UnitDataSO data)
    {
        _data = data;

        _currentHealth = data.MaxHealth;       
    }

    public void SetDefaultTarget(Transform target)
    {
        _defaultTarget = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_combatTarget != null)
        {
            if (other.TryGetComponent(out UnitDataSO potentialEnemy))
            {
                if (potentialEnemy.UnitTeam == potentialEnemy.UnitTeam.Player)
                {
                    _combatTarget = potentialEnemy;
                }
            }       
        }
    }

    private void Attack()
    {
        float damage = _data.Damage;
        float range = _data.AttackRange;
        float attackInterval = _data.AttackInterval;


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
        OnDied?.Invoke(this); // оповещение
        Destroy(gameObject);
    }
}