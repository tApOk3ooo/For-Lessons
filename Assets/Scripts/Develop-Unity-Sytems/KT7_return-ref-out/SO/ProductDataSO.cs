using UnityEngine;

[CreateAssetMenu(fileName = "Product-Data", menuName = "KT7 - OnlineShop/Product")]
public class ProductDataSO : ScriptableObject
{
    private string _productName;
    private string _description;
    private double _price;
    private int _amount;
    private bool _isBought = false;

    public string ProductName => _productName;
    public string Description => _description;
    public double Price => _price;
    public int Amount => _amount;
    public bool IsBought => _isBought;
}
