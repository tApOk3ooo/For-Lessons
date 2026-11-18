using TMPro;
using UnityEngine;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health _targetHealth;
    [SerializeField] private TextMeshProUGUI _healthText;

    private void OnEnable()
    {
        if (_targetHealth != null)
        {
            _targetHealth.HealthChanged += UpdateText;

            UpdateText(_targetHealth.CurrentHealth, _targetHealth.MaxHealth);
        }
    }

    private void OnDisable()
    {
        _targetHealth.HealthChanged -= UpdateText;
    }

    private void UpdateText(int current, int max)
    {
        _healthText.SetText($"HP: {current} / {max}");
    }
}
