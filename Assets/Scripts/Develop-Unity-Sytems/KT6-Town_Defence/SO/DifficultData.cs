using UnityEngine;

[CreateAssetMenu(fileName = "Difficult-Data", menuName = "Tower Defence Data/Dificult")]
public class DifficultData : ScriptableObject
{
    [SerializeField] private float _healthMultiplierPerLevel;

    [SerializeField] private float _damageMultiplierPerLevel;

    [SerializeField] private float _moveSpeedMultiplierPerLevel;

    [SerializeField] private float _attackIntervalMultiplierPerLevel;

    public float HealthMultiplierPerLevel => _healthMultiplierPerLevel;
    public float DamageMultiplierPerLevel => _damageMultiplierPerLevel;
    public float MoveSpeedMultiplierPerLevel => _moveSpeedMultiplierPerLevel;
    public float AttackIntervalMultiplierPerLevel => _attackIntervalMultiplierPerLevel;
}
