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

    public enum WorkerRole
    {
        None,
        Plowman,
        Fisherman,
        Hunter
    }

    public enum ResourceType
    {
        Wheat,
        Fish,
        Meat
    }

    [Header("Balance - Combat")]
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _damage = 15f;
    [SerializeField] private float _moveSpeed = 3.5f;
    [SerializeField] private float _attackInterval = 1.2f;
    [SerializeField] private float _attackRange = 1.5f;

    [Header("Balance - Economy & Hiring")]
    [SerializeField] private float _consumablePerTimerTick = 2f;
    [SerializeField] private float _productionPerTick = 5f;
    [SerializeField] private float _hireCost = 10f;
    [SerializeField] private float _hireDuration = 3f;
    [SerializeField] private int _hireBatchCount = 1;

    [Header("Classification")]
    [SerializeField] private Team _team;
    [SerializeField] private Type _type;
    [SerializeField] private WorkerRole _workerRole = WorkerRole.Plowman;
    [SerializeField] private ResourceType _producedResource = ResourceType.Wheat;

    [Header("System Info")]
    [SerializeField] private string _unitName;
    [SerializeField] private GameObject _unitPrefab;

    public float MaxHealth => _maxHealth;
    public float Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public float AttackInterval => _attackInterval;
    public float AttackRange => _attackRange;
    public float ConsumablePerFeed => _consumablePerTimerTick;
    public float ProductionPerTick => _productionPerTick;
    public float HireCost => _hireCost;
    public float HireDuration => _hireDuration;
    public int HireBatchCount => Mathf.Max(1, _hireBatchCount);
    public Team UnitTeam => _team;
    public Type UnitType => _type;
    public WorkerRole Role => _workerRole;
    public ResourceType ProducedResource => _producedResource;
    public string UnitName => _unitName;
    public GameObject UnitPrefab => _unitPrefab;
}
