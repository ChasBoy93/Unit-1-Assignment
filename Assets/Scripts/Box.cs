using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int maxHealth = 200;
    public int health;
    public GameObject collectableObject;
    public Transform respawnPoint;
    public GameObject player;

    
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
            collectableObject.SetActive(true);
        }
    }

}
