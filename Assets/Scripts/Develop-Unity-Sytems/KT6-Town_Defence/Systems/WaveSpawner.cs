using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private LevelDataSO _levelData;

    private void Start()
    {
        StartCoroutine(SpawnWaveRoutine(_levelData));    
    }

    private IEnumerator SpawnWaveRoutine(LevelDataSO levelData)
    {
        foreach (var entry in levelData.SpawnEntries)
        {
            yield return new WaitForSeconds(entry.DelayBeforeSpawn);
            for (int i = 0; i < entry.Count; i++)
            {
                _unitSpawner.SpawnUnit(entry.UnitData);

                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
