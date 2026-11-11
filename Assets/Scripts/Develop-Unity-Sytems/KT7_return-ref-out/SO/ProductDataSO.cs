using UnityEngine;

[CreateAssetMenu(fileName = "Product-Data", menuName = "KT7 - Online Shop/Product")]
public class ProductDataSO : ScriptableObject
{
    [SerializeField] private string _productName;
    [SerializeField] private string _productDescription;
    [SerializeField] private Sprite _productSprite;
    [SerializeField] private double _productPrice;
    [SerializeField] private int _productAmount;
    [SerializeField] private bool _isProductBought = false;

    public string ProductName => _productName;
    public string ProductDescription => _productDescription;
    public Sprite ProductSprite => _productSprite;
    public double ProductPrice => _productPrice;
    public int ProductAmount => _productAmount;
    public bool IsProductBought => _isProductBought;
}
