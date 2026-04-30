using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 6f;
    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;
    public float ceilingY = 4f;
    public float floorY = -4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb.gravityScale = 4f;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    // Called by Jump Button (OnClick)
    public void OnJumpPressed()
    {
        if (isGrounded) Jump();
    }

    // Called by Flip Button (OnClick)
    public void OnFlipPressed()
    {
        FlipGravity();
    }

    void Jump()
    {
        float jumpDirection = Mathf.Sign(rb.gravityScale);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * jumpDirection);
        anim.SetTrigger("Jump");
        isGrounded = false;
    }

    void FlipGravity()
    {
        rb.gravityScale *= -1;
        float targetY = rb.gravityScale < 0 ? ceilingY : floorY;
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        Vector3 scaler = transform.localScale;
        scaler.y *= -1;
        transform.localScale = scaler;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}