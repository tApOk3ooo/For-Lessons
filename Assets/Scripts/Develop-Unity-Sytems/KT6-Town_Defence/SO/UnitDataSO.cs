using UnityEngine;

[CreateAssetMenu(fileName = "Unit-Data", menuName = "KT6 - Tower Defence Data/Unit")]
public class UnitDataSO : ScriptableObject
{
    public enum Team
    {
        Player,
        Enemy
    }
    public enum Type
    {
        Warrior,
        Worker
    }

    [Header("Balance")]
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _damage;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _attackInterval;

    [SerializeField] private float _attackRange;

    [SerializeField] private float _consumablePerTimerTick;

    [SerializeField] private Team _team;
    [SerializeField] private Type _type;



    [Header("System Info")]
    [SerializeField] private string _unitName;
    [SerializeField] private GameObject _unitPrefab;


    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public float AttackInterval => _attackInterval;
    public float AttackRange => _attackRange;
    public float ConsumablePerFeed => _consumablePerTimerTick;
    public Team UnitTeam => _team;
    public Type UnitType => _type;
    public string UnitName => _unitName;
    public GameObject UnitPrefab => _unitPrefab;
}
