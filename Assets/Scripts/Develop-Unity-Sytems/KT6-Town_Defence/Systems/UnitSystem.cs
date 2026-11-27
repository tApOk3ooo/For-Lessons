using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitSystem : MonoBehaviour
{
    [SerializeField] private float _aggroRadius = 8f;

    private UnitDataSO _data;
    private NavMeshAgent _agent;
    private Transform _defaultTarget;
    private UnitSystem _combatTarget;
    private DefenseStructure _defenseTarget;

    private float _currentHealth;
    private float _damage;
    private float _attackInterval;
    private float _nextAttackTime;
    private float _nextScanTime;
    private bool _isDead;

    public UnitDataSO Data => _data;
    public UnitDataSO.Type UnitType => _data.UnitType;
    public UnitDataSO.Team UnitTeam => _data.UnitTeam;
    public Transform CurrentTarget
    {
        get
        {
            if (_combatTarget != null)
            {
                return _combatTarget.transform;
            }

            if (_defenseTarget != null && !_defenseTarget.IsDestroyed)
            {
                return _defenseTarget.transform;
            }

            return _defaultTarget;
        }
    }

    public event Action<UnitSystem> Died;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Initialize(UnitDataSO data, DifficultDataSO difficulty = null, int waveIndex = 0)
    {
        _data = data;
        _isDead = false;

        float healthMultiplier = 1f;
        float damageMultiplier = 1f;
        float speedMultiplier = 1f;
        float attackIntervalMultiplier = 1f;

        if (data.UnitTeam == UnitDataSO.Team.Enemy && waveIndex > 0)
        {
            healthMultiplier = Mathf.Pow(difficulty.HealthMultiplierPerLevel, waveIndex);
            damageMultiplier = Mathf.Pow(difficulty.DamageMultiplierPerLevel, waveIndex);
            speedMultiplier = Mathf.Pow(difficulty.MoveSpeedMultiplierPerLevel, waveIndex);
            attackIntervalMultiplier = Mathf.Pow(difficulty.AttackIntervalMultiplierPerLevel, waveIndex);
        }

        _currentHealth = data.MaxHealth * healthMultiplier;
        _damage = data.Damage * damageMultiplier;
        _attackInterval = Mathf.Max(0.2f, data.AttackInterval * attackIntervalMultiplier);

        _agent.speed = _data.MoveSpeed * speedMultiplier;
        _agent.stoppingDistance = _data.AttackRange * 0.9f;
    }

    public void SetDefaultTarget(Transform target)
    {
        _defaultTarget = target;
    }

    private void Update()
    {
        if (_isDead)
        {
            return;
        }

        if (Time.time >= _nextScanTime)
        {
            _nextScanTime = Time.time + 0.3f;
            ScanForTargets();
        }

        Transform target = CurrentTarget;
        if (target == null)
        {
            return;
        }

        if (_agent.enabled && _agent.isOnNavMesh)
        {
            _agent.SetDestination(target.position);
        }

        AftermathCheckDistance(target);
    }

    private void ScanForTargets()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _aggroRadius);

        UnitSystem bestUnit = null;

        DefenseStructure bestDefense = null;

        float bestUnitDistanceSqr = float.MaxValue;

        foreach (Collider hit in hits)
        {
            if (UnitTeam == UnitDataSO.Team.Enemy && hit.TryGetComponent(out DefenseStructure defense) && !defense.IsDestroyed)
            {
                bestDefense = defense;
            }

            if (!hit.TryGetComponent(out UnitSystem candidate) || candidate._isDead || candidate.UnitTeam == UnitTeam)
            {
                continue;
            }

            float distanceSqr = (candidate.transform.position - transform.position).sqrMagnitude;

            bool candidateIsWarrior = candidate.UnitType == UnitDataSO.Type.Warrior;

            bool bestIsWarrior = bestUnit != null && bestUnit.UnitType == UnitDataSO.Type.Warrior;

            if (bestUnit == null || (candidateIsWarrior && !bestIsWarrior) || (candidateIsWarrior == bestIsWarrior && distanceSqr < bestUnitDistanceSqr))
            {
                bestUnit = candidate;
                bestUnitDistanceSqr = distanceSqr;
            }
        }

        _combatTarget = bestUnit;
        _defenseTarget = bestDefense;
    }

    private void AftermathCheckDistance(Transform target)
    {
        Vector3 targetPos = target.position;

        if (_combatTarget == null && _defenseTarget != null && target == _defenseTarget.transform)
        {
            targetPos = _defenseTarget.GetComponent<Collider>().ClosestPoint(transform.position);
        }

        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance <= _data.AttackRange)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (Time.time < _nextAttackTime)
        {
            return;
        }

        if (_combatTarget != null && !_combatTarget._isDead)
        {
            _combatTarget.TakeDamage(_damage);

            _nextAttackTime = Time.time + _attackInterval;

            return;
        }

        if (_defenseTarget != null && !_defenseTarget.IsDestroyed)
        {
            _defenseTarget.TakeDamage(_damage);

            _nextAttackTime = Time.time + _attackInterval;
        }
    }

    public void TakeDamage(float amount)
    {
        if (_isDead)
        {
            return;
        }

        _currentHealth -= amount;
        if (_currentHealth <= 0f)
        {
            Die();
        }
    }

    public void ForceKill()
    {
        if (!_isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead)
        {
            return;
        }

        _isDead = true;

        Died?.Invoke(this);

        Destroy(gameObject);
    }
}
