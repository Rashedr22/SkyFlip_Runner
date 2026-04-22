using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float jumpForce = 6f; // Increased slightly so the jump feels impactful

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded; // Prevents the player from infinite jumping

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. Endless Running (Always move right)
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        // 2. Normal Jump (Spacebar) - Only works if we are touching the platform
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // 3. Gravity Flip ("R" key)
        if (Input.GetKeyDown(KeyCode.R))
        {
            FlipGravity();
        }
    }

    void Jump()
    {
        // Check our gravity. If it's normal (1), we jump UP. 
        // If gravity is flipped (-1), we need to jump DOWN toward the ceiling!
        float jumpDirection = Mathf.Sign(rb.gravityScale);

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * jumpDirection);

        // Trigger the jump animation
        anim.SetTrigger("Jump");

        // We are in the air now, so we can't jump again until we land
        isGrounded = false;
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
    

    // 4. Ground Check (Unity calls this automatically when you hit a collider)
    void OnCollisionEnter2D(Collision2D collision)
    {
        // When we touch a platform, we are grounded again and can jump!
        isGrounded = true;
    }
}
