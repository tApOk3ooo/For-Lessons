using UnityEngine;

[CreateAssetMenu(fileName = "Unit-Data", menuName = "Tower Defence Data/Unit")]
public class UnitDataSO : ScriptableObject
{

    [Header("Balance")]
    [SerializeField] private float _maxHealth;

    [SerializeField] private float _damage;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private float _attackInterval;

    [SerializeField] private float _attackRange;

    [SerializeField] private float _consumablePerTimerTick;
    //[SerializeField] private bool _isEnemy;

    enum Team
    {
        Player,
        Enemy
    }


    [Header("System Info")]
    [SerializeField] private string _unitName;
    [SerializeField] private UnitType _unitType;
    [SerializeField] private GameObject _unitPrefab;


    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public float AttackInterval => _attackInterval;
    public float AttackRange => _attackRange;
    public float ConsumablePerFeed => _consumablePerTimerTick;
    public bool IsEnemy => _isEnemy;
    public UnitType UnitType => _unitType;
    public string UnitName => _unitName;
    public GameObject UnitPrefab => _unitPrefab;
}
