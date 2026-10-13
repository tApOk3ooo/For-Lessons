using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;
    [SerializeField] private GameObject _lockedIcon;
    [SerializeField] private Button _levelButton;


    private DataLevelSO _dataLevel;

    private LevelManager _levelManager;

    public void Setup(DataLevelSO currentData, LevelManager lm)
    {
        if (_dataLevel.IsLocked == true)
        {
            //_levelButton.;
        }

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