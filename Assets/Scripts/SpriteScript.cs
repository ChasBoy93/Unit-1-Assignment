using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteScript : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator animator;
    SpriteRenderer sr;
    bool isGrounded;
    bool isJumping;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask destroyLayer;
    public int attackDamage = 20;
    public LayerMask groundLayer;
    Helper helperScript;

    public CollectObject collect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        helperScript = gameObject.AddComponent<Helper>();
    }


    void Update()
    {
        MoveSprite();
       // SpriteJump();
        SpriteAttack();
        SpriteFall();
        helperScript.DoRayCollisionCheck();
        DoJump();
    }

    private void FixedUpdate()
    {
        SpriteLand();
    }

    void MoveSprite()
    {
        animator.SetBool("Walk", false);
        animator.SetFloat("yVelocity", rb.velocity.y);
        

        if (Input.GetKey("left") == true)
        {
            rb.velocity = new Vector2(-3f, rb.velocity.y);
            animator.SetBool("Walk", true);
            sr.flipX = true;
        }

        else
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Input.GetKey("right") == true)
        {
            rb.velocity = new Vector2(3f, rb.velocity.y);
            animator.SetBool("Walk", true);
            sr.flipX = false;
        }

    }
    void SpriteJump()
    {
        if (Input.GetKeyDown("space") && (isGrounded == true))
        {
            isJumping = true;
            rb.AddForce(new Vector3(0, 9, 0), ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);

        }
    }


    void SpriteLand()
    {

        if (isJumping && isGrounded && (rb.velocity.y <= 0))
        {
            isJumping = false;
            animator.SetBool("Jumping", false);
        }

        if( isGrounded && (rb.velocity.y <= 0) )
        {
            animator.SetBool("Falling", false);
        }
       
    }


    public void SpriteAttack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsAttacking", true);

            Collider2D[] hitEnemy =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, destroyLayer);

            foreach(Collider2D enemy in hitEnemy)
            {
                enemy.GetComponent<Box>().TakeDamage(attackDamage);
            }

        }

        
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void SpriteFall()
    {
        if (isGrounded == false && isJumping == false && rb.velocity.y <= -1)
        {
            animator.SetBool("Falling", true);
        }
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

    }

    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    void DoJump()
    {
        if (!IsGrounded())
        {
            return;
        }
        else if (Input.GetKeyDown("space"))
        {
            isJumping = true;
            rb.AddForce(new Vector3(0, 9, 0), ForceMode2D.Impulse);
            animator.SetBool("Jumping", true);

        }

    }


    public void AttackFinished()
    {
        animator.SetBool("IsAttacking", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.gameObject.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            collect.collectCount++;
        }
    }

}
