using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class VFXGraphController : MonoBehaviour
{
    [SerializeField] private VisualEffect _effect;
    [SerializeField] private bool _isActive = true;

    private static readonly int IsActiveId = Shader.PropertyToID("IsPortalActive");

    void Awake()
    {
        if (_effect == null)
        {
            _effect = GetComponent<VisualEffect>();
        }
    }

    private void Start()
    {
        SetPortalActive(_isActive);
    }

    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard.spaceKey.wasReleasedThisFrame)
        {
            print("тест");
            SetPortalActive(!_isActive);
        }
    }

    public void SetPortalActive(bool isActive)
    {
        _isActive = isActive;

        _effect.SetBool(IsActiveId, isActive);
    }

    //private void OnValidate()
    //{
    //    SetPortalActive(_isActive);
    //}
}
