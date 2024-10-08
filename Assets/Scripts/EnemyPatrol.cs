using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    Helper helperScript;
    Rigidbody2D rb;
    SpriteRenderer sr;
    float dir;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        helperScript = gameObject.AddComponent<Helper>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        dir = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        rb.velocity = new Vector3(dir, 0, 0);

        if (helperScript.ExtendedRayCollisionCheck(-0.5f, -0.1f) == false)
        {
           if(dir < 0)
            {
                dir = 1;
                sr.flipX = true;
            }
           
        }
        if (helperScript.ExtendedRayCollisionCheck(0.5f, -0.1f) == false)
        {
            if (dir > 0)
            {
                dir = -1;
                sr.flipX = false;
            }

        }
    }
}
