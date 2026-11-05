using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSystem : MonoBehaviour
{
    private UnitDataSO _data;
    private Transform _defaultTarget;
    private UnitSystem _combatTarget;
    private float _currentHealth;
    private float _nextAttackTime;
    public UnitDataSO.Type UnitType => _data.UnitType;
    public UnitDataSO.Team UnitTeam => _data.UnitTeam;

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
        if (!other.TryGetComponent(out UnitSystem candidate))
        {
            return;
            print("на компоненте нет UnitSystem");
        }

        if (candidate.UnitTeam == this.UnitTeam)
        {
            return;
        }

        if (_combatTarget == null)
        {
            _combatTarget = candidate;
            return;
        }

        if (_combatTarget.UnitType == UnitDataSO.Type.Worker && candidate.UnitType == UnitDataSO.Type.Warrior)
        {
            _combatTarget = candidate;
        }
    }

    private void AftermathCheckDistance()
    {
        float distance = Vector3.Distance(transform.position, CurrentTaget.position);

        if (distance <= _data.AttackRange)
        {
            TryAttack();
        }
        else
        {

        }
    }

    private void TryAttack()
    {
        if (Time.time < _nextAttackTime)
        {
            return;
        }


        _combatTarget.TakeDamage(_data.Damage);

        _nextAttackTime = Time.time + _data.AttackInterval;
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