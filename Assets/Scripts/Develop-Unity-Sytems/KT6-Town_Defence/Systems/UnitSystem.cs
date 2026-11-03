using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UnitSystem : MonoBehaviour
{
    private UnitDataSO _data;
    private float _currentHealth;
    private float _attackInterval;
    private float _attackRange;
    private float _consumablePerTimerTick;
    private float _damage;
    private float _moveSpeed;
    private GameObject _unitPrefab;


    private void Initialize(UnitDataSO data)
    {
        _data = data;

        _currentHealth = data.MaxHealth;

        _attackInterval = data.AttackInterval;

        _attackRange = data.AttackRange;

        _consumablePerTimerTick = data.ConsumablePerFeed;

        _damage = data.Damage;

        _moveSpeed = data.MoveSpeed;

        _unitPrefab = data.UnitPrefab;
        
    }

    private void Attack()
    {
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