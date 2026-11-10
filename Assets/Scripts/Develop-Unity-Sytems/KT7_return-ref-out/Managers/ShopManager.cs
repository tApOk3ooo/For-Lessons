using TMPro;
using UnityEngine;

public class ShopManager
{
    [SerializeField] private ProductDataSO[] _allProducts;
    [SerializeField] private GameObject _productCardPrefab;
    [SerializeField] private Transform _productCardTransform;
    [SerializeField] private TextMeshProUGUI _sumText;
    private float _sumValueOfProducts;
}
