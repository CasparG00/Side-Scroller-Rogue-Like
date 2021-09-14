using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int myHealth = 1;

    private int health
    {
        get => myHealth;
        set => myHealth = value;
    }
    
    public void TakeDamage(int _amount)
    {
        health -= _amount;
    }

    public void Heal(int _amount)
    {
        health += _amount;
    }
}
