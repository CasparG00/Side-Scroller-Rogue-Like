using UnityEngine;

public class Stomp : MonoBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (rb.velocity.y < -0.05f)
        {
            other.transform.parent.GetComponent<IDamageable>().TakeDamage(1);
            transform.parent.GetComponent<Movement>().DoJump(Vector2.up, 10);
        }
    }
}