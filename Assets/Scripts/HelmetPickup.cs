using UnityEngine;

public class HelmetPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<Character>().health < 2)
        {
            other.GetComponent<Character>().Heal(1);

            Destroy(gameObject);
        }
    }
}
