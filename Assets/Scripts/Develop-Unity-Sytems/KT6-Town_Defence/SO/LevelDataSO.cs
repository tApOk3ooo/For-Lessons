using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level-Data", menuName = "Tower Defence Data/Level")]
public class LevelDataSO : ScriptableObject
{
    [SerializeField] private List<SpawnEntry> _spawns;

    public List<SpawnEntry> SpawnEntries => _spawns;
}
