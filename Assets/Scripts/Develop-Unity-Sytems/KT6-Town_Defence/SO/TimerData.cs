using UnityEngine;

[CreateAssetMenu(fileName = "Timer-Data", menuName = "Tower Defence Data/Timer")]
public class TimerData : ScriptableObject
{
    [SerializeField] private float _timeForAccrualValute;
    [SerializeField] private float _timeForFeed;
    [SerializeField] private float _timeForRollbackHire;
    [SerializeField] private float _timeForWar;
    [SerializeField] private float _timeForDisaster;


    public float TimeForAccrualValute => _timeForAccrualValute;
    public float TimeForFeed => _timeForFeed;
    public float TimeForRollbackHire => _timeForRollbackHire;
    public float TimeForWar => _timeForWar;
    public float TimeForDisaster => _timeForDisaster;
}
