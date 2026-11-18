using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartProductCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Image _icon;

    private ProductDataSO _data;
    private float _currentPrice;
    private int _countInCart = 1;
    private const string ValuteSymbol = " $";

    public ProductDataSO Data => _data;
    public float CurrentPrice => _currentPrice;
    public int CountInCart => _countInCart;
    public float TotalPrice => _currentPrice * _countInCart;

    public void Initialize(ProductDataSO data)
    {
        _data = data;
        _currentPrice = data.ProductPrice;
        _countInCart = 1;

        _nameText.SetText(_data.ProductName);
        _icon.sprite = _data.ProductSprite;

        UpdateUI();
    }

    public void IncreaseCount()
    {
        _countInCart++;
        UpdateUI();
    }

    public void UpdatePrice(float newPrice)
    {
        _currentPrice = newPrice;
        UpdateUI();
    }

    private void UpdateUI()
    {
        _countText.SetText("x" + _countInCart);
        _priceText.SetText(_currentPrice.ToString("F1") + ValuteSymbol);
    }
}
