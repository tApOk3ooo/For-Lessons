using System;
using UnityEngine;

public class ButtonHeal : MonoBehaviour
{
    [SerializeField] private int _healAmount = 10;
    public event Action<int> Healed;

    public void ClickButton()
    {
        Healed?.Invoke(_healAmount);
        Healed(_healAmount);
    }
}
