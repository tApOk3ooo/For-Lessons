using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [Header("Ability Settings")]
    [SerializeField] private string _abilityName = "Ability 1";
    [SerializeField, Min(0.1f)] private float _cooldownTime = 3f;
    [SerializeField] private Button _button;

    private bool _isOnCooldown;
    private Coroutine _cooldownCorurine;

    private void OnEnable()
    {
        _button.onClick.AddListener(UseAbility);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(UseAbility);

        if (_cooldownCorurine != null)
        {
            StopCoroutine(_cooldownCorurine);
            _cooldownCorurine = null;
        }

        _isOnCooldown = false;
        _button.interactable = false;
    }

    public void UseAbility()
    {
        if (_isOnCooldown)
        {
            print($"{_abilityName} - на перезарядке, использовать нельзя");
            return;
        }

        print($"{_abilityName} - использована! откат через {_cooldownTime} в секундах");

        _cooldownCorurine = StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        _isOnCooldown = true;
        _button.interactable = false;

        yield return new WaitForSeconds(_cooldownTime);

        _isOnCooldown = false;
        _button.interactable = true;
        _cooldownCorurine = null;

        print($"{_abilityName} - перезарядка завершена");
    }
}
