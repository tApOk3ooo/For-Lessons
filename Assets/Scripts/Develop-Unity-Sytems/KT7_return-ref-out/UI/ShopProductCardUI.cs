using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopProductCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Button _addToCartButton;
    [SerializeField] private Image _icon;
    private ProductDataSO _productData;
    private ShopManager _shopManager;
    private CartManager _cartManager;
    private int _currentStock;
    private const string AmountPostscript = " pcs";
    private const string ValuteSymbol = " $";

    //private string _convertedToStringAmount;

    public void Initialize(ProductDataSO currentData, ShopManager shopManager, CartManager cartManager)
    {
        _productData = currentData;
        _shopManager = shopManager;
        _cartManager = cartManager;

        Setup();
    }

    private void Setup()
    {
        // никаких null проверок!!!
        _nameText.SetText(_productData.ProductName);

        _descriptionText.SetText(_productData.ProductDescription);

        _icon.sprite = _productData.ProductSprite;

        _priceText.SetText(_productData.ProductPrice.ToString() + ValuteSymbol);

        _currentStock = _productData.ProductAmount;

        UpdateAmountText();

        _addToCartButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void UpdateAmountText()
    {
        _amountText.SetText(_currentStock + AmountPostscript);
    }

    private void OnBuyButtonClicked()
    {
        if (TryBuy(out ProductDataSO currentProduct))
        {
            _cartManager.AddProduct(currentProduct);
        }
    }

    public bool TryBuy(out ProductDataSO currentProduct)
    {
        if (_currentStock > 0)
        {
            _currentStock--;

            UpdateAmountText();

            currentProduct = _productData;
            return true;
        }

        currentProduct = null;
        return false;
    }

    private void OnDestroy()
    {
        _addToCartButton.onClick.RemoveListener(OnBuyButtonClicked);
    }
}