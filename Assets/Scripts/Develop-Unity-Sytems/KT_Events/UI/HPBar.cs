using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _fillImage;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.HealthChanged += UpdateBar;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= UpdateBar;
    }

    private void UpdateBar(int current, int max)
    {
        _fillImage.fillAmount = (float)current / max;

        _healthText.text = $"{current}/{max}";
    }
}
