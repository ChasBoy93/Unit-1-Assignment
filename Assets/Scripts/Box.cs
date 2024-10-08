using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int maxHealth = 300;
    public int health;
    public Transform respawnPoint;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        if(gameObject.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.position;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
