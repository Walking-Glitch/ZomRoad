using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }

    void Start()
    {
        CurrentHealth = MaxHealth;
    }
    public void Damage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

   
}
