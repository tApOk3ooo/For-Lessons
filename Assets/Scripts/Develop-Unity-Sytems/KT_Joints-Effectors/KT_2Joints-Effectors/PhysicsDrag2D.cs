using UnityEngine;
using UnityEngine.InputSystem;

public class PhysicsDrag2D : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _draggableLayers;

    [Header("Pull Settings")]
    [SerializeField] private float _maxForce = 100f;
    [SerializeField] private float _frequency = 5f;
    [SerializeField, Range(0f, 1f)] private float _dampingRatio = 0.5f;

    private TargetJoint2D _activeJoint;

    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryGrabObject();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ReleaseObject();
        }
    }

    private void FixedUpdate()
    {
        if (_activeJoint != null && Mouse.current != null)
        {
            _activeJoint.target = GetMouseWorldPosition();
        }
    }

    private void TryGrabObject()
    {
        Vector2 mouseWorldPosition = GetMouseWorldPosition();
        Collider2D hit = Physics2D.OverlapPoint(mouseWorldPosition, _draggableLayers);

        if (hit == null || hit.attachedRigidbody == null)
        {
            return;
        }

        if (hit.attachedRigidbody.bodyType != RigidbodyType2D.Dynamic)
        {
            return;
        }

        Rigidbody2D rb = hit.attachedRigidbody;
        _activeJoint = rb.gameObject.AddComponent<TargetJoint2D>();
        _activeJoint.autoConfigureTarget = false;
        _activeJoint.anchor = rb.transform.InverseTransformPoint(mouseWorldPosition);
        _activeJoint.target = mouseWorldPosition;
        _activeJoint.maxForce = _maxForce;
        _activeJoint.frequency = _frequency;
        _activeJoint.dampingRatio = _dampingRatio;
    }

    private void ReleaseObject()
    {
        if (_activeJoint == null)
        {
            return;
        }

        Destroy(_activeJoint);
        _activeJoint = null;
    }

    private Vector2 GetMouseWorldPosition()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();
        float distanceToPlane = -_camera.transform.position.z;
        return _camera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceToPlane));
    }
}