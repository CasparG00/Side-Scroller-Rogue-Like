using UnityEngine;

public class HelmetThwack : MonoBehaviour
{
    public float force = 10;
    public float torque = 20;
    
    Rigidbody2D rb;
    Vector3 startVelocity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        startVelocity = (Vector3)Random.insideUnitCircle + Vector3.up;
        startVelocity.y = Mathf.Abs(startVelocity.y);
        startVelocity = startVelocity.normalized;
        
        rb.AddForce(startVelocity * force, ForceMode2D.Impulse);
        rb.AddTorque(startVelocity.x * torque);
        
        Destroy(gameObject, 10);
    }
}
