using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ProductDataSO[] _allProducts;
    [SerializeField] private GameObject _productCardPrefab;
    [SerializeField] private Transform _productContentTransform;
    [SerializeField] private CartManager _cartManager;
    private float _sumValueOfProducts;

    private void Start()
    {
        CreateProductCard();
    }

    void CreateProductCard()
    {
        foreach (var item in _allProducts)
        {
            if (item == null)
            {
                print("пропущен data файл в массиве");
            }

            GameObject productCardObject = Instantiate(_productCardPrefab, _productContentTransform);

            ShopProductCardUI currentCard = productCardObject.GetComponent<ShopProductCardUI>();

            currentCard.Initialize(item, this, _cartManager);
        }
    }
}
