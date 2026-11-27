using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private TimerDataSO _timerData;
    [SerializeField] private DifficultDataSO _difficultData;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private UnitDataSO _defaultEnemyData;
    [SerializeField] private List<LevelDataSO> _customWaves = new List<LevelDataSO>();
    [SerializeField] private List<DefenseStructure> _defenseStructures = new List<DefenseStructure>();
    [SerializeField] private bool _useFormulaCombat = false;

    private Coroutine _waveRoutine;

    public int CurrentWaveNumber { get; private set; }
    public int TotalWavesToWin => _difficultData.TotalWavesToWin;

    public event Action<float, float, int> OnWaveTimerTick;
    public event Action<int, int> OnWaveStart;
    public event Action<int, int> WaveCleared;
    public event Action AllWavesCleared;
    public event Action VillageFallen;

    public void StartWaves()
    {
        StopWaves();

        CurrentWaveNumber = 0;

        foreach (var structure in _defenseStructures)
        {
            structure.ResetStructure();
        }

        _waveRoutine = StartCoroutine(WaveLoopRoutine());
    }

    public void StopWaves()
    {
        if (_waveRoutine != null)
        {
            StopCoroutine(_waveRoutine);
            _waveRoutine = null;
        }
    }

    private IEnumerator WaveLoopRoutine()
    {
        float interval = Mathf.Max(2f, _timerData.TimeForWar);

        for (int wave = 0; wave < TotalWavesToWin; wave++)
        {
            CurrentWaveNumber = wave;
            int enemyCount = GetEnemyCount(wave);

            for (float timeLeft = interval; timeLeft > 0f; timeLeft -= Time.deltaTime)
            {
                OnWaveTimerTick?.Invoke(timeLeft, interval, enemyCount);
                yield return null;
            }

            OnWaveTimerTick?.Invoke(0f, interval, enemyCount);
            OnWaveStart?.Invoke(wave + 1, enemyCount);

            if (!_useFormulaCombat && _defaultEnemyData.UnitPrefab != null)
            {
                yield return StartCoroutine(SpawnPhysicalWave(wave, enemyCount));
                if (_unitSpawner.TotalPlayerUnits == 0)
                {
                    VillageFallen?.Invoke();
                    yield break;
                }
            }
            else if (!ResolveFormulaCombat(enemyCount))
            {
                VillageFallen?.Invoke();
                yield break;
            }

            WaveCleared?.Invoke(wave + 1, enemyCount);
        }

        AllWavesCleared?.Invoke();
        _waveRoutine = null;
    }

    private int GetEnemyCount(int waveIndex)
    {
        if (waveIndex < _customWaves.Count && _customWaves[waveIndex] != null && _customWaves[waveIndex].SpawnEntries.Count > 0)
        {
            int total = 0;

            foreach (var entry in _customWaves[waveIndex].SpawnEntries)
            {
                total += entry.Count;
            }
            return total;
        }

        return _difficultData.BaseEnemiesPerWave + waveIndex * _difficultData.EnemiesAddedPerWave;
    }

    private IEnumerator SpawnPhysicalWave(int waveIndex, int fallbackCount)
    {
        var customWave = waveIndex < _customWaves.Count ? _customWaves[waveIndex] : null;

        if (customWave != null && customWave.SpawnEntries.Count > 0)
        {
            foreach (var entry in customWave.SpawnEntries)
            {
                if (entry.DelayBeforeSpawn > 0f)
                {
                    yield return new WaitForSeconds(entry.DelayBeforeSpawn);
                }

                for (int spawnIndex = 0; spawnIndex < entry.Count; spawnIndex++)
                {
                    _unitSpawner.SpawnUnit(entry.UnitData, _difficultData, waveIndex);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        else
        {
            for (int spawnIndex = 0; spawnIndex < fallbackCount; spawnIndex++)
            {
                _unitSpawner.SpawnUnit(_defaultEnemyData, _difficultData, waveIndex);
                yield return new WaitForSeconds(0.5f);
            }
        }

        while (_unitSpawner.EnemyCount > 0 && _unitSpawner.TotalPlayerUnits > 0)
        {
            yield return null;
        }
    }

    private bool ResolveFormulaCombat(int enemies)
    {
        int warriorsPerEnemy = _difficultData.WarriorsPerEnemy;

        int warriors = _unitSpawner.WarriorCount;

        if (warriors > 0)
        {
            int stopped = Mathf.Min(enemies, warriors / warriorsPerEnemy);
            int lost = stopped * warriorsPerEnemy;
            if (stopped < enemies && warriors - lost > 0)
            {
                lost = warriors;
                stopped++;
            }

            _unitSpawner.KillWarriors(lost);
            enemies -= stopped;
        }

        if (enemies <= 0)
        {
            return true;
        }

        int workersPerEnemy = _difficultData.WorkersPerEnemy;

        int workers = _unitSpawner.WorkerCount;

        if (workers >= enemies * workersPerEnemy)
        {
            _unitSpawner.KillWorkers(enemies * workersPerEnemy);
            return true;
        }

        _unitSpawner.KillWorkers(workers);

        return false;
    }
}
