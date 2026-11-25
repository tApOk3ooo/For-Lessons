using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _moveOffset = new Vector2(4f, 0f);
    [SerializeField] private float _speed = 1.5f;
    private Vector2 _startPos;
    private void Start()
    {
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _startPos = _rb.position;
    }
    private void FixedUpdate()
    {
        float transform = (Mathf.Sin(Time.fixedTime * _speed) + 1f) * 0.5f;
        _rb.MovePosition(_startPos + _moveOffset * transform);
    }
}
