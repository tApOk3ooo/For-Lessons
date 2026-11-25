using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Explosion : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private CircleCollider2D _circleCollider;
    [SerializeField] private PointEffector2D _effector;
    [SerializeField] private Sprite _explosionSprite;
    [SerializeField, Min(0.01f)] private float _duration = 0.4f;
    [SerializeField] private float _force = 25f;
    [SerializeField, Min(0.01f)] private float _radius = 3f;
    [SerializeField] private LayerMask _affectedLayers;

    private void Awake()
    {
        ApplyChanges();
    }

    private void Start()
    {
        Destroy(gameObject, _duration);
    }

    private void ApplyChanges()
    {
        _spriteRenderer.sprite = _explosionSprite;

        _circleCollider.isTrigger = true;
        _circleCollider.usedByEffector = true;
        _circleCollider.radius = _radius;

        _effector.useColliderMask = true;
        _effector.colliderMask = _affectedLayers;
        _effector.forceMagnitude = _force;
    }
}
