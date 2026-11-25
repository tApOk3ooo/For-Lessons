using UnityEngine;
using UnityEngine.InputSystem;

public class CarContoller : MonoBehaviour
{
    [SerializeField] private WheelJoint2D[] _wheels;
    [SerializeField] private float _motorSpeed = 800f;
    [SerializeField] private float _maxMotorTorque = 1000f;
    private float _moveInput;
    private void Update()
    {
        _moveInput = 0f;
        if (Keyboard.current.dKey.isPressed)
        {
            _moveInput -= 1f;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            _moveInput += 1f;
        }
    }
    private void FixedUpdate()
    {
        bool hasInput = !Mathf.Approximately(_moveInput, 0f);
        foreach (WheelJoint2D wheel in _wheels)
        {
            wheel.useMotor = hasInput;

            if (!hasInput)
            {
                continue;
            }

            JointMotor2D motor = wheel.motor;
            motor.motorSpeed = -_moveInput * _motorSpeed;
            motor.maxMotorTorque = _maxMotorTorque;
            wheel.motor = motor;
        }
    }
}