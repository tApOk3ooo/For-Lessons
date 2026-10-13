using NUnit.Framework;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private TextMeshProUGUI _levelAmountText;
    [SerializeField] private GameObject _lockedIcon;


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
    }
}
