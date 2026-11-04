using UnityEngine;

public class WalletManager : MonoBehaviour
{
    public float ValueToChange;
    private float _currentMoneyValue;

    public void AddMoneyValueToWallet()
    {
        _currentMoneyValue += ValueToChange;
    }

    public bool TrySpendMoney(float amount)
    {
        if (_currentMoneyValue < amount)
        {
            return false;
        }

        _currentMoneyValue -= amount;

        return true;
    }
}