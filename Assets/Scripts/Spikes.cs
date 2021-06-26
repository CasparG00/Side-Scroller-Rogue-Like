using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var character = other.transform.parent.GetComponent<Character>();
        if (character != null)
        {
            character.Damage(1);
        }
    }
}
