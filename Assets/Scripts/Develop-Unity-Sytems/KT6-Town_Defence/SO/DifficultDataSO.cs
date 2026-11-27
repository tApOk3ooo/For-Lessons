using UnityEngine;

[CreateAssetMenu(fileName = "Difficult-Data", menuName = "KT6 - Tower Defence Data/Dificult")]
public class DifficultDataSO : ScriptableObject
{
    [Header("Enemy Stat Scaling Per Wave")]
    [SerializeField] private float _healthMultiplierPerLevel = 1.15f;
    [SerializeField] private float _damageMultiplierPerLevel = 1.1f;
    [SerializeField] private float _moveSpeedMultiplierPerLevel = 1.02f;
    [SerializeField] private float _attackIntervalMultiplierPerLevel = 0.98f;

    [Header("Wave Progression")]
    [SerializeField] private int _baseEnemiesPerWave = 2;
    [SerializeField] private int _enemiesAddedPerWave = 2;
    [SerializeField] private int _totalWavesToWin = 5;

    [Header("Village Combat Ratios")]
    [Tooltip("Сколько воинов нужно для 1 врага (1:1)")]
    [SerializeField] private int _warriorsPerEnemy = 1;
    [Tooltip("Сколько крестьян гибнет за 1 врага, если воинов нет (3:1)")]
    [SerializeField] private int _workersPerEnemy = 3;

    public float HealthMultiplierPerLevel => _healthMultiplierPerLevel;
    public float DamageMultiplierPerLevel => _damageMultiplierPerLevel;
    public float MoveSpeedMultiplierPerLevel => _moveSpeedMultiplierPerLevel;
    public float AttackIntervalMultiplierPerLevel => _attackIntervalMultiplierPerLevel;

    public int BaseEnemiesPerWave => Mathf.Max(1, _baseEnemiesPerWave);
    public int EnemiesAddedPerWave => Mathf.Max(0, _enemiesAddedPerWave);
    public int TotalWavesToWin => Mathf.Max(1, _totalWavesToWin);

    public int WarriorsPerEnemy => Mathf.Max(1, _warriorsPerEnemy);
    public int WorkersPerEnemy => Mathf.Max(1, _workersPerEnemy);
}
