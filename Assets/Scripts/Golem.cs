using UnityEngine;

public class Golem : Enemy
{
    public float speed = 10;

    public float turnPadding = 0.1f;
    
    Rigidbody2D rb;
    SpriteRenderer sr;

    bool movingRight;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!Physics2D.Raycast(transform.position + transform.right * (sr.bounds.size.x * (0.5f + turnPadding)), Vector2.down, 1))
        {
            movingRight = !movingRight;
        }

        transform.eulerAngles = movingRight ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
    }

    void FixedUpdate()
    {
        var velocity = transform.right * speed;
        velocity.y = Physics2D.gravity.y;
        rb.velocity = velocity;
    }
}
