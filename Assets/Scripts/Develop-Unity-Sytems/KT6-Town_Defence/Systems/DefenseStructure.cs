using System;
using UnityEngine;

public class DefenseStructure : MonoBehaviour
{
    [SerializeField] private string _structureName = "Частокол";
    [SerializeField] private float _maxHealth = 100f;

    private float _currentHealth;

    public string StructureName => _structureName;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;
    public bool IsDestroyed => _currentHealth <= 0f;

    public event Action<float, float> HealthChanged;
    public event Action<DefenseStructure> Destroyed;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void ResetStructure()
    {
        _currentHealth = _maxHealth;

        gameObject.SetActive(true);

        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (IsDestroyed || amount <= 0f)
        {
            return;
        }

        _currentHealth = Mathf.Max(0f, _currentHealth - amount);

        HealthChanged?.Invoke(_currentHealth, _maxHealth);

        if (_currentHealth <= 0f)
        {
            Destroyed?.Invoke(this);

            gameObject.SetActive(false);
        }
    }

    public void Repair(float amount)
    {
        if (amount <= 0f)
        {
            return;
        }

        _currentHealth = Mathf.Min(_maxHealth, _currentHealth + amount);
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
