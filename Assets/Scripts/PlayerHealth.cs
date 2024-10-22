using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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



        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(TextDelay());
        Destroy(gameObject);
    }

    private IEnumerator TextDelay()
    {
        yield return new WaitForSeconds(3);
        endText.SetActive(false);
    }
}
