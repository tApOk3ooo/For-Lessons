using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private InputAction moveHorizontalAction;

    private Rigidbody2D rb;
    private bool isGrounded = false;

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
        moveHorizontalAction.ReadValue<float>();
        rb.linearVelocity = new Vector2(_moveSpeed, rb.linearVelocityY);

        Keyboard keyboard = Keyboard.current;
        if (keyboard.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    private void OnDisable()
    {
        moveHorizontalAction.Disable();
    }
}