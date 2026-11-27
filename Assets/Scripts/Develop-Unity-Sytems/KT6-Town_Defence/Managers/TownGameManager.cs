using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownGameManager : MonoBehaviour
{
    public enum GameState
    {
        StartMenu,
        Playing,
        Paused,
        Victory,
        Defeat
    }

    [Header("Core References")]
    [SerializeField] private TimerDataSO _timerData;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private HiringManager _hiringManager;
    [SerializeField] private WaveSpawner _waveSpawner;
    [SerializeField] private DefenseStructure _defenseStructure;

    [Header("Starting Units")]
    [SerializeField] private UnitDataSO _startingWorkerData;
    [SerializeField] private int _startingWorkers = 2;
    [SerializeField] private UnitDataSO _startingWarriorData;
    [SerializeField] private int _startingWarriors = 1;

    [Header("Audio")]
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private AudioClip _gatherClip;
    [SerializeField] private AudioClip _eatClip;
    [SerializeField] private AudioClip _spawnClip;
    [SerializeField] private AudioClip _attackClip;
    [SerializeField] private AudioClip _eventClip;
    [SerializeField] private AudioClip _victoryClip;
    [SerializeField] private AudioClip _defeatClip;

    private Coroutine _eventRoutine;

    public GameState CurrentState { get; private set; } = GameState.StartMenu;
    public int WavesSurvived { get; private set; }
    public int UnitsHired { get; private set; }
    public float ResourcesGathered { get; private set; }
    public int EnemiesDefeated { get; private set; }

    public event Action<GameState> GameStateChanged;
    public event Action<string, string> RandomEventTriggered;

    private void OnEnable()
    {
        _waveSpawner.OnWaveStart += HandleWaveStart;
        _waveSpawner.WaveCleared += HandleWaveCleared;
        _waveSpawner.AllWavesCleared += HandleAllWavesCleared;
        _waveSpawner.VillageFallen += HandleVillageFallen;
        _hiringManager.UnitHired += HandleUnitHired;
        _resourceManager.ResourcesGathered += HandleResourcesGathered;
        _resourceManager.FoodConsumed += HandleFoodConsumed;
    }

    private void OnDisable()
    {
        _waveSpawner.OnWaveStart -= HandleWaveStart;
        _waveSpawner.WaveCleared -= HandleWaveCleared;
        _waveSpawner.AllWavesCleared -= HandleAllWavesCleared;
        _waveSpawner.VillageFallen -= HandleVillageFallen;
        _hiringManager.UnitHired -= HandleUnitHired;
        _resourceManager.ResourcesGathered -= HandleResourcesGathered;
        _resourceManager.FoodConsumed -= HandleFoodConsumed;
    }

    private void Start()
    {
        SetState(GameState.StartMenu);
    }

    public void StartGame()
    {
        StopAllSystems();

        WavesSurvived = 0;
        UnitsHired = 0;
        EnemiesDefeated = 0;
        ResourcesGathered = 0f;

        _resourceManager.ResetResources();

        _hiringManager.ResetState();

        _unitSpawner.ClearAllUnits();

        for (int workerIndex = 0; workerIndex < _startingWorkers; workerIndex++)
        {
            _unitSpawner.SpawnUnit(_startingWorkerData);
        }

        for (int warriorIndex = 0; warriorIndex < _startingWarriors; warriorIndex++)
        {
            _unitSpawner.SpawnUnit(_startingWarriorData);
        }

        SetState(GameState.Playing);

        _resourceManager.StartCycles();

        _waveSpawner.StartWaves();

        _eventRoutine = StartCoroutine(RandomEventRoutine());
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Playing)
        {
            SetState(GameState.Paused);
        }
    }

    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            SetState(GameState.Playing);
        }
    }

    public void ReturnToMenu()
    {
        StopAllSystems();

        _unitSpawner.ClearAllUnits();

        SetState(GameState.StartMenu);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("TEST");
    }

    public void PlayButtonClick()
    {
        PlaySoundEffect(_clickClip);
    }

    private void HandleWaveStart(int waveNumber, int enemyCount)
    {
        PlaySoundEffect(_attackClip);
    }

    private void HandleWaveCleared(int wavesSurvived, int enemiesDefeated)
    {
        WavesSurvived = wavesSurvived;
        EnemiesDefeated += enemiesDefeated;
    }

    private void HandleAllWavesCleared()
    {
        EndGame(GameState.Victory, _victoryClip);
    }

    private void HandleVillageFallen()
    {
        EndGame(GameState.Defeat, _defeatClip);
    }

    private void HandleUnitHired(UnitDataSO unitData)
    {
        UnitsHired++;
        PlaySoundEffect(_spawnClip);
    }

    private void HandleResourcesGathered(float gatheredAmount)
    {
        ResourcesGathered += gatheredAmount;

        PlaySoundEffect(_gatherClip);
    }

    private void HandleFoodConsumed(float consumedAmount)
    {
        PlaySoundEffect(_eatClip);
    }

    private void EndGame(GameState state, AudioClip clip)
    {
        StopAllSystems();

        PlaySoundEffect(clip);

        SetState(state);
    }

    private void StopAllSystems()
    {
        _resourceManager.StopCycles();

        _hiringManager.ResetState();

        _waveSpawner.StopWaves();

        if (_eventRoutine != null)
        {
            StopCoroutine(_eventRoutine);
            _eventRoutine = null;
        }
    }

    private void SetState(GameState state)
    {
        CurrentState = state;

        Time.timeScale = state == GameState.Playing ? 1f : 0f;

        GameStateChanged?.Invoke(state);
    }

    private IEnumerator RandomEventRoutine()
    {
        var wait = new WaitForSeconds(Mathf.Max(3f, _timerData.TimeForDisaster));
        float chance = _timerData.DisasterChance;

        while (true)
        {
            yield return wait;

            if (UnityEngine.Random.value > chance)
            {
                continue;
            }

            PlaySoundEffect(_eventClip);

            int roll = UnityEngine.Random.Range(0, 3);

            if (roll == 0)
            {
                int killed = _unitSpawner.KillRandomPlayerUnits(1);
                RandomEventTriggered?.Invoke("чума!", killed > 0 ? $"погиб {killed} житель." : "деревня не пострадала.");
            }
            else if (roll == 1)
            {
                _resourceManager.LoseWheatPercent(0.25f);
                _defenseStructure.TakeDamage(20f);
                RandomEventTriggered?.Invoke("наводнение!", "потеряно 25% пшеницы.");
            }
            else
            {
                _resourceManager.AddResource(UnitDataSO.ResourceType.Wheat, 15f);
                RandomEventTriggered?.Invoke("урожайный год!", "+15 пшеницы в амбары.");
            }
        }
    }

    private void PlaySoundEffect(AudioClip clip)
    {
        // надо будет найти звуки
        print("умоляю найди звуки потом");
        if (clip != null)
        {
            _sfxSource.PlayOneShot(clip);
        }
    }
}
