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


    private float _currentTime;
    private float _autoTimerCurrentTime;
    private bool _isRunning;
    private bool _isAutoTimerRunning;
    private int _launchAmount;
    private int _resourceValue;

    private void Update()
    {
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
        if (_currentTime <= 0f)
        {
            _currentTime = 0f;
            _isRunning = false;
        }
        _currentTime -= Time.deltaTime;

        _timerImage.fillAmount = _currentTime / _setedTime;

        if (_isRunning == false)
        {
            _setedTime += Time.deltaTime;

            _timerImage.fillAmount = 1;
        }

        if (_autoTimerCurrentTime >= 1)
        {
            _autoTimerCurrentTime = 0f;
            _isAutoTimerRunning = false;
        }

        _autoTimerCurrentTime += Time.deltaTime;

        _autoTimerImage.fillAmount = _autoTimerCurrentTime / _setedAutoTimerTime;

        if (_isAutoTimerRunning == false)
        {
            _setedAutoTimerTime -= Time.deltaTime;
            _autoTimerImage.fillAmount = 1;
        }
    }

    public void LaunchTimer()
    {
        if (_setedTime <= 0f)
        {
            Debug.Log("нужно положительное число");
        }

        _currentTime = _setedTime;

        _isRunning = true;

        _launchAmount++;

        SetLaunchAmountText();

        TimerRoutine();
    }

    void SetLaunchAmountText()
    {
        _lauchAmountText.text = Convert.ToString(_launchAmount);
    }
}