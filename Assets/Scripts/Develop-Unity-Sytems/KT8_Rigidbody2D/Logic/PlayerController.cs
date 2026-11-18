using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private InputAction moveHorizontalAction;

    private float _horizontalInput;
    private Rigidbody2D rb;
    private bool _isGrounded = false;

    private void OnEnable()
    {
        moveHorizontalAction.Enable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 moveVector = moveHorizontalAction.ReadValue<Vector2>();
        _horizontalInput = moveVector.x;

        Keyboard keyboard = Keyboard.current;
        if (keyboard.spaceKey.wasPressedThisFrame && _isGrounded == true)
        {
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_horizontalInput * _moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    private void OnDisable()
    {
        moveHorizontalAction.Disable();
    }
}