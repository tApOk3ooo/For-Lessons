using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private Transform _workerTarget;
    [SerializeField] private Transform _warriorTarget;
    [SerializeField] private Transform _defaultEnemyTarget;
    [SerializeField] private float _spawnSpreadRadius = 1.5f;

    private readonly List<UnitSystem> _spawnedWarriors = new List<UnitSystem>();
    private readonly List<UnitSystem> _spawnedWorkers = new List<UnitSystem>();
    private readonly List<UnitSystem> _spawnedEnemies = new List<UnitSystem>();

    public int WarriorCount => _spawnedWarriors.Count;
    public int WorkerCount => _spawnedWorkers.Count;
    public int EnemyCount => _spawnedEnemies.Count;
    public int TotalPlayerUnits => WarriorCount + WorkerCount;

    public event Action UnitsChanged;
    public event Action<UnitDataSO> UnitSpawned;

    public void SpawnUnit(UnitDataSO data, DifficultDataSO difficulty = null, int waveIndex = 0)
    {
        Transform spawnAnchor = data.UnitTeam == UnitDataSO.Team.Enemy ? _enemySpawnPoint : _playerSpawnPoint;
        Vector2 offset2D = UnityEngine.Random.insideUnitCircle * _spawnSpreadRadius;
        Vector3 spawnPos = spawnAnchor.position + new Vector3(offset2D.x, 0f, offset2D.y);

        if (NavMesh.SamplePosition(spawnPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            spawnPos = hit.position;
        }

        GameObject instance = Instantiate(data.UnitPrefab, spawnPos, spawnAnchor.rotation);
        if (instance.TryGetComponent(out UnitSystem unit))
        {
            unit.Initialize(data, difficulty, waveIndex);
            Transform target = data.UnitTeam == UnitDataSO.Team.Enemy
                ? _defaultEnemyTarget
                : (data.UnitType == UnitDataSO.Type.Warrior ? _warriorTarget : _workerTarget);
            unit.SetDefaultTarget(target);
            unit.Died += HandleUnitDied;
            RegisterPhysicalUnit(unit);
        }
        else
        {
            Debug.LogWarning($"[UnitSpawner] UnitSystem не найден на префабе {data.UnitPrefab.name}");
        }

        UnitSpawned?.Invoke(data);
        UnitsChanged?.Invoke();
    }

    private void RegisterPhysicalUnit(UnitSystem unit)
    {
        if (unit.UnitTeam == UnitDataSO.Team.Enemy)
        {
            _spawnedEnemies.Add(unit);
        }
        else if (unit.UnitType == UnitDataSO.Type.Warrior)
        {
            _spawnedWarriors.Add(unit);
        }
        else
        {
            _spawnedWorkers.Add(unit);
        }
    }

    private void HandleUnitDied(UnitSystem unit)
    {
        unit.Died -= HandleUnitDied;
        _spawnedWarriors.Remove(unit);
        _spawnedWorkers.Remove(unit);
        _spawnedEnemies.Remove(unit);
        UnitsChanged?.Invoke();
    }

    public List<UnitDataSO> GetAllWorkerData()
    {
        List<UnitDataSO> result = new List<UnitDataSO>(_spawnedWorkers.Count);
        foreach (UnitSystem worker in _spawnedWorkers)
        {
            result.Add(worker.Data);
        }
        return result;
    }

    public List<UnitDataSO> GetAllWarriorData()
    {
        List<UnitDataSO> result = new List<UnitDataSO>(_spawnedWarriors.Count);
        foreach (UnitSystem warrior in _spawnedWarriors)
        {
            result.Add(warrior.Data);
        }
        return result;
    }

    public int GetWorkerCountByRole(UnitDataSO.WorkerRole role)
    {
        int count = 0;
        foreach (UnitSystem worker in _spawnedWorkers)
        {
            if (worker.Data.Role == role)
            {
                count++;
            }
        }
        return count;
    }

    public int KillWarriors(int count)
    {
        return KillFromList(_spawnedWarriors, count);
    }

    public int KillWorkers(int count)
    {
        return KillFromList(_spawnedWorkers, count);
    }

    private int KillFromList(List<UnitSystem> list, int count)
    {
        int killed = 0;
        for (int index = 0; index < count; index++)
        {
            if (list.Count == 0)
            {
                break;
            }

            UnitSystem unit = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            if (unit != null)
            {
                unit.Died -= HandleUnitDied;
                unit.ForceKill();
            }
            killed++;
        }

        if (killed > 0)
        {
            UnitsChanged?.Invoke();
        }
        return killed;
    }

    public int KillRandomPlayerUnits(int count)
    {
        int killed = 0;
        for (int index = 0; index < count; index++)
        {
            if (TotalPlayerUnits == 0)
            {
                break;
            }

            bool killWarrior = WarriorCount > 0 && (WorkerCount == 0 || UnityEngine.Random.value < 0.5f);
            killed += killWarrior ? KillWarriors(1) : KillWorkers(1);
        }
        return killed;
    }

    public void ClearAllUnits()
    {
        ClearPhysicalList(_spawnedWarriors);
        ClearPhysicalList(_spawnedWorkers);
        ClearPhysicalList(_spawnedEnemies);
        UnitsChanged?.Invoke();
    }

    private void ClearPhysicalList(List<UnitSystem> list)
    {
        for (int index = list.Count - 1; index >= 0; index--)
        {
            UnitSystem unit = list[index];
            if (unit != null)
            {
                unit.Died -= HandleUnitDied;
                Destroy(unit.gameObject);
            }
        }
        list.Clear();
    }
}
