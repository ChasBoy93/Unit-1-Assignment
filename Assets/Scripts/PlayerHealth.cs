using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth < 0)
        {
            Die();
        }
    }

    void Die()
    {

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}