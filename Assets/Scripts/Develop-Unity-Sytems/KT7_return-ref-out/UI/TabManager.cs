using UnityEngine;

public class TabManager : MonoBehaviour
{
    [SerializeField] private GameObject _shopTab;
    [SerializeField] private GameObject _cartTab;

    private void Awake()
    {
        OpenShopTab();
    }

    public void OpenShopTab()
    {
        _cartTab.SetActive(false);

        _shopTab.SetActive(true);
    }

    public void OpenCartTab()
    {
        _shopTab.SetActive(false);

        _cartTab.SetActive(true);
    }
}
