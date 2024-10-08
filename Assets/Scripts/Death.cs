using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public Transform respawnPoint;
    public GameObject player;

    private void Start()
    {
        PlayerDeath();
    }

    void PlayerDeath()
    {
        if (gameObject.CompareTag("Player"))
        {
            player.transform.position = respawnPoint.position;
        }
    }
}
