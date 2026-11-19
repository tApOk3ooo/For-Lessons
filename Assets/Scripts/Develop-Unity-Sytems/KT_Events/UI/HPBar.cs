using TMPro;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _healthText;
    private ButtonHeal buttonHeal;
    private ButtonDmg buttonDmg;


    private void OnEnable()
    {
        //buttonHeal.Healed += UpdateText;
    }

    private void UpdateText(int current, int max)
    {
        _healthText.SetText($"HP: {current} / {max}");
    }
}
