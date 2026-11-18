using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KT7;

public class CartManager : MonoBehaviour
{
    [SerializeField] private Transform _cartContent;
    [SerializeField] private GameObject _cartCardPrefab;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] private TextMeshProUGUI _totalSumText;
    [SerializeField] private Button _nextDayButton;
    [SerializeField] private Button _buyCartButton;
    [SerializeField] private KT7.WalletManager _walletManager;

    private readonly List<CartProductCardUI> _cartCards = new List<CartProductCardUI>();
    private int _currentDay = 1;
    [SerializeField] private float _inflationRate = 0.15f;

    private void Start()
    {
        UpdateDayUI();
        UpdateTotalSumUI();

        _nextDayButton.onClick.AddListener(OnNextDayButtonClicked);
        _buyCartButton.onClick.AddListener(OnBuyCartButtonClicked);
    }

    public void AddProduct(ProductDataSO currentProduct)
    {
        CartProductCardUI existingCard = _cartCards.Find(c => c.Data == currentProduct);

        if (existingCard != null)
        {
            existingCard.IncreaseCount();
        }
        else
        {
            GameObject cardObj = Instantiate(_cartCardPrefab, _cartContent);
            CartProductCardUI card = cardObj.GetComponent<CartProductCardUI>();
            card.Initialize(currentProduct);
            _cartCards.Add(card);
        }

        UpdateTotalSumUI();
    }

    public float CalculateTotalSum()
    {
        float sum = 0f;
        foreach (var card in _cartCards)
        {
            sum += card.TotalPrice;
        }
        return sum;
    }

    public void OnNextDayButtonClicked()
    {
        _currentDay++;
        UpdateDayUI();

        foreach (var card in _cartCards)
        {
            float price = card.CurrentPrice;

            ApplyInflation(ref price, _inflationRate);

            card.UpdatePrice(price);
        }

        UpdateTotalSumUI();
    }

    public void ApplyInflation(ref float price, float rate)
    {
        price += price * rate;
    }

    public void OnBuyCartButtonClicked()
    {
        float total = CalculateTotalSum();
        if (total <= 0)
        {
            return;
        }

        if (_walletManager.TrySpend(total))
        {
            foreach (var card in _cartCards)
            {
                Destroy(card.gameObject);
            }
            _cartCards.Clear();

            UpdateTotalSumUI();
        }
        else
        {
            print("денег нет");
        }
    }

    private void UpdateDayUI()
    {
        _dayText.SetText("Day: " + _currentDay);
    }

    private void UpdateTotalSumUI()
    {
        _totalSumText.SetText("Sum: " + CalculateTotalSum().ToString("F1") + " $");
    }

    private void OnDestroy()
    {
        _nextDayButton.onClick.RemoveListener(OnNextDayButtonClicked);
        _buyCartButton.onClick.RemoveListener(OnBuyCartButtonClicked);
    }
}
