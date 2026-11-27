using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private TownGameManager _gameManager;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private HiringManager _hiringManager;
    [SerializeField] private WaveSpawner _waveSpawner;

    [Header("Hire Unit Configs")]
    [SerializeField] private UnitDataSO _peasantData;
    [SerializeField] private UnitDataSO _warriorData;
    [SerializeField] private UnitDataSO _fishermanData;
    [SerializeField] private UnitDataSO _hunterData;

    [Header("Panels")]
    [SerializeField] private GameObject _startMenuPanel;
    [SerializeField] private GameObject _gameplayPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _endGamePanel;

    [Header("HUD")]
    [SerializeField] private TMP_Text _wheatText;
    [SerializeField] private TMP_Text _extraFoodText;
    [SerializeField] private TMP_Text _warriorsText;
    [SerializeField] private TMP_Text _workersText;
    [SerializeField] private TMP_Text _waveInfoText;
    [SerializeField] private TMP_Text _waveTimerText;
    [SerializeField] private Slider _waveTimerSlider;

    [Header("Buttons & Sliders")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _hirePeasantButton;
    [SerializeField] private Button _hireWarriorButton;
    [SerializeField] private Button _hireFishermanButton;
    [SerializeField] private Button _hireHunterButton;
    [SerializeField] private TMP_Text _hirePeasantText;
    [SerializeField] private TMP_Text _hireWarriorText;
    [SerializeField] private Slider _hirePeasantSlider;
    [SerializeField] private Slider _hireWarriorSlider;
    [SerializeField] private Button _upgradeButton;
    [SerializeField] private TMP_Text _upgradeText;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _pauseRestartButton;
    [SerializeField] private Button _pauseMenuButton;

    [Header("End Game & Events")]
    [SerializeField] private TMP_Text _endTitleText;
    [SerializeField] private TMP_Text _endStatsText;
    [SerializeField] private Button _endRestartButton;
    [SerializeField] private Button _endMenuButton;
    [SerializeField] private GameObject _eventBanner;
    [SerializeField] private TMP_Text _eventBannerText;

    private Coroutine _bannerRoutine;

    private void Awake()
    {
        Bind(_startButton, _gameManager.StartGame);
        BindHire(_hirePeasantButton, _peasantData);
        BindHire(_hireWarriorButton, _warriorData);
        BindHire(_hireFishermanButton, _fishermanData);
        BindHire(_hireHunterButton, _hunterData);
        Bind(_upgradeButton, BuyUpgrade);
        Bind(_pauseButton, _gameManager.PauseGame);
        Bind(_resumeButton, _gameManager.ResumeGame);
        Bind(_pauseRestartButton, _gameManager.RestartGame);
        Bind(_pauseMenuButton, _gameManager.ReturnToMenu);
        Bind(_endRestartButton, _gameManager.RestartGame);
        Bind(_endMenuButton, _gameManager.ReturnToMenu);
    }

    private void OnEnable()
    {
        _gameManager.GameStateChanged += HandleGameStateChanged;
        _gameManager.RandomEventTriggered += HandleRandomEvent;
        _resourceManager.ResourcesChanged += HandleResourcesChanged;
        _unitSpawner.UnitsChanged += RefreshUnits;
        _hiringManager.OnHiring += HandleHiring;
        _hiringManager.UpgradesChanged += RefreshButtons;
        _waveSpawner.OnWaveTimerTick += HandleWaveTimerTick;
    }

    private void OnDisable()
    {
        _gameManager.GameStateChanged -= HandleGameStateChanged;
        _gameManager.RandomEventTriggered -= HandleRandomEvent;
        _resourceManager.ResourcesChanged -= HandleResourcesChanged;
        _unitSpawner.UnitsChanged -= RefreshUnits;
        _hiringManager.OnHiring -= HandleHiring;
        _hiringManager.UpgradesChanged -= RefreshButtons;
        _waveSpawner.OnWaveTimerTick -= HandleWaveTimerTick;
    }

    private void Bind(Button button, System.Action action)
    {
        button.onClick.AddListener(() =>
        {
            _gameManager.PlayButtonClick();
            action();
        });
    }

    private void BindHire(Button button, UnitDataSO unitData)
    {
        button.onClick.AddListener(() =>
        {
            _gameManager.PlayButtonClick();
            _hiringManager.TryHire(unitData);
            RefreshButtons();
        });
    }

    private void BuyUpgrade()
    {
        _hiringManager.TryBuyUpgrade();
    }

    private void HandleGameStateChanged(TownGameManager.GameState state)
    {
        _startMenuPanel.SetActive(state == TownGameManager.GameState.StartMenu);
        _gameplayPanel.SetActive(state == TownGameManager.GameState.Playing || state == TownGameManager.GameState.Paused);
        _pausePanel.SetActive(state == TownGameManager.GameState.Paused);
        _endGamePanel.SetActive(state == TownGameManager.GameState.Victory || state == TownGameManager.GameState.Defeat);

        if (state == TownGameManager.GameState.Playing)
        {
            _eventBanner.SetActive(false);

            HandleResourcesChanged(_resourceManager.Wheat, _resourceManager.Fish, _resourceManager.Meat);

            RefreshUnits();
        }
        else if (state == TownGameManager.GameState.Victory || state == TownGameManager.GameState.Defeat)
        {
            _endTitleText.text = state == TownGameManager.GameState.Victory ? "Победа!" : "Поражение!";
            _endStatsText.text = $"Волн сдержано: {_gameManager.WavesSurvived}\nНанято юнитов: {_gameManager.UnitsHired}\nСобрано ресурсов: {Mathf.FloorToInt(_gameManager.ResourcesGathered)}\nПобеждено врагов: {_gameManager.EnemiesDefeated}";
        }
    }

    private void HandleResourcesChanged(float wheat, float fish, float meat)
    {
        _wheatText.text = $"Пшеница: {Mathf.FloorToInt(wheat)}";
        _extraFoodText.text = $"Рыба: {Mathf.FloorToInt(fish)} | Мясо: {Mathf.FloorToInt(meat)}";
        RefreshButtons();
    }

    private void RefreshUnits()
    {
        _warriorsText.text = $"Воины: {_unitSpawner.WarriorCount}";
        _workersText.text = $"Крестьяне: {_unitSpawner.WorkerCount}";
    }

    private void RefreshButtons()
    {
        _hirePeasantButton.interactable = _hiringManager.CanAfford(_peasantData);
        _hireWarriorButton.interactable = _hiringManager.CanAfford(_warriorData);
        _hireFishermanButton.interactable = _hiringManager.CanAfford(_fishermanData);
        _hireHunterButton.interactable = _hiringManager.CanAfford(_hunterData);

        _hirePeasantText.text = $"Крестьянин ({_peasantData.HireCost}) [{_hiringManager.WorkerQueueCount}]";
        _hireWarriorText.text = $"Воин ({_warriorData.HireCost}) [{_hiringManager.WarriorQueueCount}]";

        _upgradeButton.interactable = _resourceManager.HasEnoughWheat(_hiringManager.NextUpgradeCost);
        _upgradeText.text = $"Апгрейд Ур.{_hiringManager.UpgradeLevel + 1} ({_hiringManager.NextUpgradeCost})";
    }

    private void HandleHiring(UnitDataSO.Type type, float progress, int queueCount)
    {
        if (type == UnitDataSO.Type.Worker)
        {
            _hirePeasantSlider.value = progress;
        }
        else if (type == UnitDataSO.Type.Warrior)
        {
            _hireWarriorSlider.value = progress;
        }

        RefreshButtons();
    }

    private void HandleWaveTimerTick(float timeLeft, float totalDuration, int enemyCount)
    {
        _waveInfoText.text = $"Волна {_waveSpawner.CurrentWaveNumber + 1}/{_waveSpawner.TotalWavesToWin}: {enemyCount} врагов";
        _waveTimerText.text = $"Атака через: {Mathf.CeilToInt(timeLeft)}с";
        _waveTimerSlider.value = 1f - (timeLeft / totalDuration);
    }

    private void HandleRandomEvent(string title, string description)
    {
        _eventBannerText.text = $"{title} {description}";
        _eventBanner.SetActive(true);

        if (_bannerRoutine != null)
        {
            StopCoroutine(_bannerRoutine);
        }

        _bannerRoutine = StartCoroutine(HideBanner());
    }

    private IEnumerator HideBanner()
    {
        yield return new WaitForSeconds(4f);
        _eventBanner.SetActive(false);
    }
}
