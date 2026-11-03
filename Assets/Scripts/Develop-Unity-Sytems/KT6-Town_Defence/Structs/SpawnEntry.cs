using UnityEngine;

public struct SpawnEntry
{
    [SerializeField] private UnitDataSO _unitData;
    [SerializeField] private int _count;
    [SerializeField] private float _delayBeforeSpawn;

    public UnitDataSO UnitData => _unitData;
    public int Count => _count;
    public float DelayBeforeSpawn => _delayBeforeSpawn;
}