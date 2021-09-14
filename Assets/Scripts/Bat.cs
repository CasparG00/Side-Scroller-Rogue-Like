using UnityEngine;

public class Bat : Enemy
{

    public Transform target;
    public float speed;

    public float hearingRange = 15f;
    
    SpriteRenderer sr;
    Rigidbody2D rb;

    bool playerIsDead;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        PlayerStats.DeathEvent += OnPlayerDeath;
    }

    void Update()
    {
        UpdateSprite();
    }

    void FixedUpdate()
    {
        if (!playerIsDead)
        {
            if (Vector2.Distance(target.position, transform.position) < hearingRange)
            {
                var direction = (target.position - transform.position).normalized;
                rb.velocity = direction * speed;
            }
        }
    }
    
    void UpdateSprite()
    {
        sr.flipX = rb.velocity.x < -0.1f;
    }

    void OnPlayerDeath()
    {
        playerIsDead = true;
        PlayerStats.DeathEvent -= OnPlayerDeath;
    }
}
