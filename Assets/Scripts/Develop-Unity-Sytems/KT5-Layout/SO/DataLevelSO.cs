using UnityEngine;

[CreateAssetMenu(fileName = "DataLevelSO", menuName = "Data/Level")]
public class DataLevelSO : ScriptableObject
{
    [SerializeField] private byte _stars = 0;

    [SerializeField] private bool _isLocked = true;

    [SerializeField] private int _numberLevel;

    public byte Stars => _stars;

    public bool IsLocked => _isLocked;

    public int NumberLevel => _numberLevel;
}
