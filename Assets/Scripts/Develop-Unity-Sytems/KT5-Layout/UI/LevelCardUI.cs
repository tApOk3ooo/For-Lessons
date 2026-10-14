using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private GameObject _lockedIcon;
    [SerializeField] private Button _levelButton;
    [SerializeField] private Image _firstStar;

    [SerializeField] private Image _secondStar;
    [SerializeField] private Image _thirdStar;
    [SerializeField] private Sprite _starIcon;



    private DataLevelSO _dataLevel;

    private LevelManager _levelManager;

    public void Setup(DataLevelSO currentData, LevelManager lm)
    {
        _dataLevel = currentData;

        _levelManager = lm;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _levelNumberText.text = Convert.ToString(_dataLevel.NumberLevel);

        if (_dataLevel.IsLocked)
        {
            _lockedIcon.SetActive(true);
        }
        else
        {
            _lockedIcon.SetActive(false);
        }

        if (_dataLevel.Stars == 0)
        {
            return;
        }
        else if (_dataLevel.Stars == 1)
        {
            _firstStar.sprite = _starIcon;
        }
        else if (_dataLevel.Stars == 2)
        {
            _firstStar.sprite = _starIcon;
            _secondStar.sprite = _starIcon;
        }
        else if (_dataLevel.Stars == 3)
        {
            _firstStar.sprite = _starIcon;
            _secondStar.sprite = _starIcon;
            _thirdStar.sprite = _starIcon;
        }
        else
        {
            Debug.Log("Вероятно на объекте выставлены звёзды за пределами диапозона 0 - 3");
        }
    }
}