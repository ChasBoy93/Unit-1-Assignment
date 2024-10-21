using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
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

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            animator.SetBool("IsAttacking", false);
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        animator.SetBool("IsDead", true);

        Destroy(gameObject, 4);
        // GetComponent<Collider2D>().enabled = false;
        //this.enabled = false;
    }
}
