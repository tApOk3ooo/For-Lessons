using System;
using System.Collections;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TimerDataSO _timerData;
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private float _startingWheat = 50f;

    public float Wheat { get; private set; }
    public float Fish { get; private set; }
    public float Meat { get; private set; }
    public float ProductionMultiplier { get; set; } = 1f;

    private Coroutine _gatherRoutine;
    private Coroutine _feedRoutine;

    public event Action<float, float, float> ResourcesChanged;
    public event Action<float> ResourcesGathered;
    public event Action<float> FoodConsumed;
    public event Action<int> UnitsStarved;

    private void Awake()
    {
        ResetResources();
    }

    public void ResetResources()
    {
        Wheat = _startingWheat;
        Fish = 0f;
        Meat = 0f;
        ProductionMultiplier = 1f;
        ResourcesChanged?.Invoke(Wheat, Fish, Meat);
    }

    public void StartCycles()
    {
        StopCycles();

        _gatherRoutine = StartCoroutine(GatherRoutine());
        _feedRoutine = StartCoroutine(FeedRoutine());
    }

    public void StopCycles()
    {
        if (_gatherRoutine != null)
        {
            StopCoroutine(_gatherRoutine);
            _gatherRoutine = null;
        }

        if (_feedRoutine != null)
        {
            StopCoroutine(_feedRoutine);
            _feedRoutine = null;
        }
    }

    public bool HasEnoughWheat(float amount)
    {
        return Wheat >= amount;
    }

    public bool TrySpendWheat(float amount)
    {
        if (amount < 0f || Wheat < amount)
        {
            return false;
        }

        Wheat -= amount;
        ResourcesChanged?.Invoke(Wheat, Fish, Meat);
        return true;
    }

    public void AddResource(UnitDataSO.ResourceType type, float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        if (type == UnitDataSO.ResourceType.Wheat)
        {
            Wheat += amount;
        }
        else if (type == UnitDataSO.ResourceType.Fish)
        {
            Fish += amount;
        }
        else if (type == UnitDataSO.ResourceType.Meat)
        {
            Meat += amount;
        }

        ResourcesChanged?.Invoke(Wheat, Fish, Meat);
    }

    public void LoseWheatPercent(float lossPercent)
    {
        Wheat = Mathf.Max(0f, Wheat - Mathf.Round(Wheat * Mathf.Clamp01(lossPercent)));

        ResourcesChanged?.Invoke(Wheat, Fish, Meat);
    }

    private IEnumerator GatherRoutine()
    {
        var wait = new WaitForSeconds(Mathf.Max(0.5f, _timerData.TimeForAccrualValute));

        while (true)
        {
            yield return wait;
            float total = 0f;

            foreach (var worker in _unitSpawner.GetAllWorkerData())
            {
                float amount = worker.ProductionPerTick * ProductionMultiplier;
                AddResource(worker.ProducedResource, amount);
                total += amount;
            }

            if (total > 0f)
            {
                ResourcesGathered?.Invoke(total);
            }
        }
    }

    private IEnumerator FeedRoutine()
    {
        var wait = new WaitForSeconds(Mathf.Max(0.5f, _timerData.TimeForFeed));

        while (true)
        {
            yield return wait;

            float consumed = 0f;

            int unfed = 0;

            foreach (var warrior in _unitSpawner.GetAllWarriorData())
            {
                if (TryConsumeFood(warrior.ConsumablePerFeed))
                {
                    consumed += warrior.ConsumablePerFeed;
                }
                else
                {
                    unfed++;
                }
            }

            if (consumed > 0f)
            {
                FoodConsumed?.Invoke(consumed);
            }

            if (unfed > 0)
            {
                int killed = _unitSpawner.KillWarriors(unfed);

                if (killed > 0)
                {
                    UnitsStarved?.Invoke(killed);
                }
            }
        }
    }

    private bool TryConsumeFood(float amount)
    {
        if (amount <= 0f)
        {
            return true;
        }

        if (Wheat + Fish + Meat < amount)
        {
            return false;
        }

        float remainingFood = amount;

        float consumedWheat = Mathf.Min(Wheat, remainingFood);
        Wheat -= consumedWheat;
        remainingFood -= consumedWheat;

        float consumedFish = Mathf.Min(Fish, remainingFood);
        Fish -= consumedFish;
        remainingFood -= consumedFish;

        Meat -= Mathf.Min(Meat, remainingFood);

        ResourcesChanged?.Invoke(Wheat, Fish, Meat);

        return true;
    }
}
