using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerStats : MonoBehaviour
{
    public static int health = 1;
    
    public static event Action DeathEvent;
    public GameObject helmet;
    public GameObject helmetRagdoll;

    Character c;

    float lastHealth;

    bool invoked;
    
    void Start()
    {
        c = GetComponent<Character>();
        c.health = health;
    }
    
    void Update()
    {
        if (c.health <= 0)
        {
            if (DeathEvent != null  && !invoked)
            {
                DeathEvent.Invoke();
                invoked = true;
            }
        }
        
        helmet.SetActive(c.health >= 2);

        if (c.health < lastHealth && c.health == 1)
        {
            Instantiate(helmetRagdoll, transform.position + Vector3.up, Quaternion.identity);
            
            var jumpDirection = Random.insideUnitCircle;
            jumpDirection.y = Mathf.Abs(jumpDirection.y);
            jumpDirection = (jumpDirection + Vector2.up).normalized;
            
            GetComponent<Movement>().Jump(jumpDirection, 4);
        }

        lastHealth = c.health;

        health = c.health;
    }
}
