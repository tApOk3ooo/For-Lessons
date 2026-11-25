using UnityEngine;
using System.Collections;

public class BouncingBall : MonoBehaviour
{
    [SerializeField] private float _bounceSpeed = 8f;
    [SerializeField] private float _speedThreshold = 2.5f;
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Material _material;
    private Coroutine _lowSpeedRoutine;
    private void Awake()
    {
        _rigidBody.constraints = RigidbodyConstraints.FreezePositionX
                        | RigidbodyConstraints.FreezePositionZ
                        | RigidbodyConstraints.FreezeRotation;
        _material.color = _normalColor;
    }
    private void FixedUpdate()
    {
        if (_lowSpeedRoutine == null && _rigidBody.linearVelocity.magnitude < _speedThreshold)
        {
            _lowSpeedRoutine = StartCoroutine(TrackLowSpeed());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        _rigidBody.linearVelocity = new Vector3(0f, _bounceSpeed, 0f);
    }
    private IEnumerator TrackLowSpeed()
    {
        float duration = 0f;

        _material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);

        do
        {
            yield return new WaitForFixedUpdate();
            duration += Time.fixedDeltaTime;
        }
        while (_rigidBody.linearVelocity.magnitude < _speedThreshold);

        _material.color = _normalColor;
        _lowSpeedRoutine = null;
    }
}