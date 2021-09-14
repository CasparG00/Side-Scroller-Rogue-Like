using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int health;
    [SerializeField] private int maxHealth = 1;

    public GameObject corpse;

    [HideInInspector] public bool isInvulnerable;

    void LateUpdate()
    {
        if (health <= 0)
        {
            Instantiate(corpse, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (health > maxHealth) health = maxHealth;
    }

    public void Damage(int amount)
    {
        if (!isInvulnerable) health -= amount;
        StartCoroutine(MakeInvulnerableForSeconds(0.1f));
    }

    public void Heal(int amount)
    {
        health += amount;
    }

    public IEnumerator MakeInvulnerableForSeconds(float duration)
    {
        isInvulnerable = true;

        yield return new WaitForSeconds(duration);
        
        isInvulnerable = false;
    }
}
