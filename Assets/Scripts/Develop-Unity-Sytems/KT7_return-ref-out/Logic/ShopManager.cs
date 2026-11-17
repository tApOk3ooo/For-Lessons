using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ProductDataSO[] _allProducts;
    [SerializeField] private GameObject _productCardPrefab;
    [SerializeField] private Transform _productContentlTransform;
    [SerializeField] private TextMeshProUGUI _sumText;
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

            GameObject productCardObject = Instantiate(_productCardPrefab, _productContentlTransform);

            ProductCardUI currentCard = productCardObject.GetComponent<ProductCardUI>();

            currentCard.Setup(item, this);
        }
    }
}
