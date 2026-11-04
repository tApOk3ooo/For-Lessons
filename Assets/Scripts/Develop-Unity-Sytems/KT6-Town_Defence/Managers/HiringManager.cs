using UnityEngine;

public class HiringManager : MonoBehaviour
{
    [SerializeField] private TimerDataSO _timerData;
    [SerializeField] private WalletManager _walletManager;
    [SerializeField] private PriceManager _priceManager;
    [SerializeField] private UnitSpawner _unitSpawner;
    private float _nextAvailableHireTime;
    private float _priceSubtraction;

    public bool CanHire => Time.time >= _nextAvailableHireTime;

    public void TryHire(UnitDataSO unit)
    {
        if (!CanHire)
        {
            print("Нельзя нанять!");
            return;
        }

        if (!_walletManager.TrySpendMoney(_priceManager.CurrentPrice))
        {
            print("Денег недостаточно!");
            return;
        }

        _nextAvailableHireTime = Time.time + _timerData.TimeForRollbackHire;

        _unitSpawner.SpawnUnit(unit);
        _priceManager.IncreasePrice();
    }
}
