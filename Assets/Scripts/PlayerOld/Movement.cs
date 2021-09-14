using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 4f;

    [Header("Jumping")]
    public float jumpVelocity = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    [Space]
    public float groundCheckRadius = 0.1f;
    public LayerMask excludePlayer;
    
    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer sr;
    
    public bool isGrounded;
    float direction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, excludePlayer);

        if (Input.GetButton("Jump") && isGrounded)
        {
            Jump();
        }
        
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
        
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * (Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
        
        animator.SetBool("IsJumping", !isGrounded);
        if (direction < 0) sr.flipX = true;
        else if (direction > 0) sr.flipX = false;
    }

    void FixedUpdate()
    {
        var input = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(input));
        direction = input;

        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }
    
    public void Jump()
    {
        rb.velocity = Vector2.up * jumpVelocity;
    }

    public void Jump(Vector2 direction, float velocity)
    {
        rb.velocity = direction * velocity;
    }
}
