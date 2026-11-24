using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private ButtonDmg _buttonDmg;
    [SerializeField] private ButtonHeal _buttonHeal;

    private int _currentHealth;
    public event Action<int, int> HealthChanged;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void OnEnable()
    {
        _buttonDmg.Damaged += TakeDamage;

        _buttonHeal.Healed += RestoreHealth;
    }

    private void OnDisable()
    {
        _buttonDmg.Damaged -= TakeDamage;

        _buttonHeal.Healed -= RestoreHealth;
    }

    private void Start()
    {
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void TakeDamage(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - amount, 0, _maxHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    private void RestoreHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public int TakeInfo()
    {
        return _currentHealth;
    }
}
