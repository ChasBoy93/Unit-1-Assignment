using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    LayerMask groundLayerMask;
    public bool playerGrounded;

    //Enemy Patrol
    public SpriteRenderer sr;
    public Rigidbody rb;

    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");

        //Enemy Patrol
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    public void DoRayCollisionCheck()
    {
        float rayLength = 0.5f;

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayerMask);

        Color hitColour = Color.white;

        if (hit.collider != null)
        {
            playerGrounded = true;
            //print("Player has collided with Ground Layer");
            hitColour = Color.red;
            
        }
        Debug.DrawRay(transform.position, Vector2.down * rayLength, hitColour);
    }

    public bool DoJump()
    {
        if (playerGrounded == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Enemy Patrol
    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.5f;
        bool hitObject = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength);

        Color hitColour = Color.red;

        if (hit.collider != null)
        {
            print("Enemy has Collided");
            hitColour = Color.green;
            hitObject = true;
        }
        Debug.DrawRay(transform.position + offset, -Vector3.up * rayLength, hitColour);
        return hitObject;

    }
}
