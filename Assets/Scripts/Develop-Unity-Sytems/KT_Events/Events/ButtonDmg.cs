using System;
using UnityEngine;

public class ButtonDmg : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 10;
    public event Action<int> Damaged;

    public void ClickButton()
    {
        Damaged?.Invoke(_damageAmount);
    }
}
