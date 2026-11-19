using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    private ButtonDmg buttonDmg;
    private ButtonHeal buttonHeal;

    private void OnEnable()
    {
        buttonDmg.Damaged += GetDamage;
        buttonHeal.Healed += GetHealth;
    }
    private void OnDisable()
    {
        buttonDmg.Damaged -= GetDamage;
        buttonHeal.Healed -= GetHealth;
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void GetDamage(int amount)
    {
        _currentHealth -= amount;
    }

    private void GetHealth(int amount)
    {
        _currentHealth += amount;
    }
}
