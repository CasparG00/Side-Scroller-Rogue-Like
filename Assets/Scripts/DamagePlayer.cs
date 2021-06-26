using UnityEngine;

public class DamagePlayer : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.parent.GetComponent<Character>().Damage(1);
    }
}
