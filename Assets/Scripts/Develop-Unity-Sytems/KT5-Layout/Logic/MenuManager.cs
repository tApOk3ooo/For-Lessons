using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;

    [SerializeField]  private GameObject _levelChoosePanel;


    private void Awake()
    {
        _levelChoosePanel.SetActive(false);
    }

    public void StartGame()
    {
        _menuPanel.SetActive(false);
        _levelChoosePanel.SetActive(true);
    }

    public void ReturnToMenu()
    {
        _levelChoosePanel.SetActive(false);
        _menuPanel.SetActive(true);
    }
}