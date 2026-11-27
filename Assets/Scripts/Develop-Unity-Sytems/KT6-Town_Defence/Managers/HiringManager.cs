using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringManager : MonoBehaviour
{
    [SerializeField] private TimerDataSO _timerData;
    [SerializeField] private ResourceManager _resourceManager;
    [SerializeField] private UnitSpawner _unitSpawner;

    [Header("Upgrades")]
    [SerializeField] private float _upgradeBaseCost = 25f;
    [SerializeField] private float _hireSpeedStep = 0.25f;
    [SerializeField] private float _productionStep = 0.25f;

    private readonly List<UnitDataSO> _workerOrders = new List<UnitDataSO>();
    private readonly List<UnitDataSO> _warriorOrders = new List<UnitDataSO>();
    private Coroutine _workerRoutine;
    private Coroutine _warriorRoutine;

    public float HireSpeedMultiplier { get; private set; } = 1f;
    public int HireBatchBonus { get; private set; } = 0;
    public int UpgradeLevel { get; private set; } = 0;

    public int WorkerQueueCount => _workerOrders.Count;
    public int WarriorQueueCount => _warriorOrders.Count;
    public float NextUpgradeCost => Mathf.Round(_upgradeBaseCost * Mathf.Pow(1.5f, UpgradeLevel));

    public event Action<UnitDataSO.Type, float, int> OnHiring;
    public event Action<UnitDataSO> UnitHired;
    public event Action UpgradesChanged;

    public bool CanAfford(UnitDataSO unit)
    {
        return _resourceManager.HasEnoughWheat(unit.HireCost);
    }

    public bool TryHire(UnitDataSO unit)
    {
        if (!_resourceManager.TrySpendWheat(unit.HireCost))
        {
            return false;
        }

        if (unit.UnitType == UnitDataSO.Type.Warrior)
        {
            _warriorOrders.Add(unit);
            if (_warriorRoutine == null)
            {
                _warriorRoutine = StartCoroutine(HireRoutine(UnitDataSO.Type.Warrior, _warriorOrders));
            }
        }
        else
        {
            _workerOrders.Add(unit);
            if (_workerRoutine == null)
            {
                _workerRoutine = StartCoroutine(HireRoutine(UnitDataSO.Type.Worker, _workerOrders));
            }
        }

        return true;
    }

    public bool TryBuyUpgrade()
    {
        if (!_resourceManager.TrySpendWheat(NextUpgradeCost))
        {
            return false;
        }

        UpgradeLevel++;
        HireSpeedMultiplier += _hireSpeedStep;
        _resourceManager.ProductionMultiplier += _productionStep;

        if (UpgradeLevel % 2 == 0)
        {
            HireBatchBonus++;
        }

        UpgradesChanged?.Invoke();
        return true;
    }

    private IEnumerator HireRoutine(UnitDataSO.Type type, List<UnitDataSO> orders)
    {
        while (orders.Count > 0)
        {
            UnitDataSO unit = orders[0];

            float baseTime = unit.HireDuration > 0f ? unit.HireDuration : _timerData.TimeForRollbackHire;
            float duration = Mathf.Max(0.25f, baseTime / HireSpeedMultiplier);

            for (float elapsedTime = 0f; elapsedTime < duration; elapsedTime += Time.deltaTime)
            {
                OnHiring?.Invoke(type, Mathf.Clamp01(elapsedTime / duration), orders.Count);

                yield return null;
            }

            orders.RemoveAt(0);

            int count = unit.HireBatchCount + HireBatchBonus;

            for (int index = 0; index < count; index++)
            {
                _unitSpawner.SpawnUnit(unit);
            }

            UnitHired?.Invoke(unit);
            OnHiring?.Invoke(type, 0f, orders.Count);
        }

        if (type == UnitDataSO.Type.Warrior)
        {
            _warriorRoutine = null;
        }
        else
        {
            _workerRoutine = null;
        }
    }

    public void ResetState()
    {
        if (_workerRoutine != null)
        {
            StopCoroutine(_workerRoutine);
            _workerRoutine = null;
        }

        if (_warriorRoutine != null)
        {
            StopCoroutine(_warriorRoutine);
            _warriorRoutine = null;
        }

        _workerOrders.Clear();
        _warriorOrders.Clear();

        HireSpeedMultiplier = 1f;
        HireBatchBonus = 0;
        UpgradeLevel = 0;

        OnHiring?.Invoke(UnitDataSO.Type.Worker, 0f, 0);
        OnHiring?.Invoke(UnitDataSO.Type.Warrior, 0f, 0);
        UpgradesChanged?.Invoke();
    }
}
