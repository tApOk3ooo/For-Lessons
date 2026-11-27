using UnityEngine;

[CreateAssetMenu(fileName = "Timer-Data", menuName = "KT6 - Tower Defence Data/Timer")]
public class TimerDataSO : ScriptableObject
{
    [Header("Economy Timers (seconds)")]
    [SerializeField] private float _timeForAccrualValute = 5f;
    [SerializeField] private float _timeForFeed = 8f;

    [Header("Hiring")]
    [SerializeField] private float _timeForRollbackHire = 3f;

    [Header("Waves & Random Events")]
    [SerializeField] private float _timeForWar = 25f;
    [SerializeField] private float _timeForDisaster = 20f;
    [Range(0f, 1f)]
    [SerializeField] private float _disasterChance = 0.35f;

    public float TimeForAccrualValute => _timeForAccrualValute;
    public float TimeForFeed => _timeForFeed;
    public float TimeForRollbackHire => _timeForRollbackHire;
    public float TimeForWar => _timeForWar;
    public float TimeForDisaster => _timeForDisaster;
    public float DisasterChance => _disasterChance;
}
