using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    [Header("Параметры")]
    [SerializeField] private float _setedTime;
    [SerializeField] private float _setedAutoTimerTime;


    [Header("Ссылки")]
    [SerializeField] private Image _timerImage;
    [SerializeField] private TextMeshProUGUI _lauchAmountText;
    [SerializeField] private Image _autoTimerImage;
    [SerializeField] private TextMeshProUGUI _resorceValueText;
    [SerializeField] private Button _timerLaunchButton;


    private float _currentTime;
    private float _autoTimerCurrentTime;
    private bool _isRunning;
    private bool _isEnough = false;
    private int _launchAmount = 0;
    private int _resourceValue = 0;

    private void Start()
    {
        SetAmountText();
        CheckResourceValue();
    }

    private void Update()
    {
        AutoTimerRoutine();

        if (!_isRunning)
        {
            return;
        }
        else
        {
            TimerRoutine();
        }
    }

    void TimerRoutine()
    {
        _isRunning = true;

        _currentTime -= Time.deltaTime;

        if (_currentTime <= 0f)
        {
            _currentTime = 0f;
            _isRunning = false;
        }

        _timerImage.fillAmount = _currentTime / _setedTime;

        if (_isRunning == false)
        {
            _timerImage.fillAmount = 1;
        }
    }

    void CheckResourceValue()
    {
        if (_resourceValue <= 4)
        {
            _isEnough = false;
        }
        else
        {
            _isEnough = true;
        }
    }

    void UnlockTimerButton()
    {
        if (_isEnough == true)
        {
            _timerLaunchButton.interactable = true;
        }
        else
        {
            _timerLaunchButton.interactable = false;
        }
    }

    void AutoTimerRoutine()
    {
        if (_setedAutoTimerTime <= 0f) return;

        _autoTimerCurrentTime += Time.deltaTime;

        if (_autoTimerCurrentTime >= _setedAutoTimerTime)
        {
            _autoTimerCurrentTime = 0f;

            _resourceValue += 3;

            SetAmountText();
        }

        _autoTimerImage.fillAmount = _autoTimerCurrentTime / _setedAutoTimerTime;

        CheckResourceValue();
        UnlockTimerButton();
    }

    public void LaunchTimer()
    {
        if (_setedTime <= 0f)
        {
            Debug.Log("нужно положительное число");
        }

        _resourceValue -= 5;

        _currentTime = _setedTime;

        _launchAmount++;

        SetAmountText();

        TimerRoutine();
    }

    void SetAmountText()
    {
        _lauchAmountText.text = Convert.ToString(_launchAmount);
        _resorceValueText.text = Convert.ToString(_resourceValue);
    }
}