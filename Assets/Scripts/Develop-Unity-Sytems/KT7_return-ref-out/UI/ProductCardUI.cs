using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProductCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _icon;
    private ProductDataSO _productData;
    private ShopManager _shopManager;
    private string _convertedToStringPrice;
<<<<<<< Updated upstream
    private const string AmountPostscript = " pcs";
    private const string ValuteSymbol = " $";
=======
    private string _convertedToStringAmount;
>>>>>>> Stashed changes

    public void Setup(ProductDataSO currentData, ShopManager shopManager)
    {
        _productData = currentData;
        _shopManager = shopManager;

        Initialize();
    }

    private void Initialize()
    {
        // никаких null проверок!!!
        _nameText.SetText(_productData.ProductName);

        _descriptionText.SetText(_productData.ProductDescription);

        _icon.sprite = _productData.ProductSprite;

        _convertedToStringPrice = Convert.ToString(_productData.ProductPrice);
        _priceText.SetText(_convertedToStringPrice + ValuteSymbol);

<<<<<<< Updated upstream
        _amountText.SetText(_convertedToStringPrice + AmountPostscript);
=======
        _convertedToStringAmount = Convert.ToString(_productData.ProductAmount);
        _amountText.SetText(_convertedToStringAmount);
>>>>>>> Stashed changes
    }
}