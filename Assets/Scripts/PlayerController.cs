using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 10f; // Not used for jump, but for flipping speed
    private Rigidbody2D rb;
    private bool isTop; // Tracks if we are on the ceiling or floor

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Endless Running (Always move right)
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        // 2. Gravity Flip Mechanic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipGravity();
        }
    }

    void FlipGravity()
    {
        // Flip the physics gravity
        rb.gravityScale *= -1;

        // Flip the sprite visually so feet hit the new floor
        Vector3 scaler = transform.localScale;
        scaler.y *= -1;
        transform.localScale = scaler;
    }
}
