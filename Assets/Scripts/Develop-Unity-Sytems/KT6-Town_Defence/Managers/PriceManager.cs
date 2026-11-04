using UnityEngine;

public class PriceManager : MonoBehaviour
{
    public float _basePriceOfUnit;
    private int _lastValueOfBoughtUnits = 0;
    private float _priceMultiplier = 1.5f;
    private HiringManager _hiringManager;

    public float CurrentPrice => _basePriceOfUnit;

    public void IncreasePrice()
    {
        _basePriceOfUnit *= _priceMultiplier;
    }
}
