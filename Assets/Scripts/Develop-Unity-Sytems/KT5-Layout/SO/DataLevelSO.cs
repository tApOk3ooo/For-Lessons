using UnityEngine;

[CreateAssetMenu(fileName = "DataLevelSO", menuName = "Data/Level")]
public class DataLevelSO : ScriptableObject
{
    public byte stars;

    public bool isLocked = true;

    public int numberLevel;
}
