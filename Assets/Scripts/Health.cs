using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public Animator animator;
    public GameObject endText;

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
            endText.SetActive(true);
            animator.SetBool("IsAttacking", false);
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Died");
        animator.SetBool("IsDead", true);

        Destroy(gameObject, 4);

        StartCoroutine(TextDelay());
    }

    private IEnumerator TextDelay()
    {
        yield return new WaitForSeconds(3);
        endText.SetActive(false);
    }
}
