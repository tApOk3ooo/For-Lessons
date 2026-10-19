using UnityEngine;

[CreateAssetMenu(fileName = "Unit-Data", menuName = "Tower Defence Data/Unit")]
public class UnitScriptableObjectScript : ScriptableObject
{
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _damage;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _attackInterval;

    [SerializeField] private float _attackRange;

    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public float AttackInterval => _attackInterval;
    public float AttackRange => _attackRange;
}
