using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    SpriteRenderer sr;
    private Transform target;
    
    public Animator animator;
    Rigidbody rb;
    bool attack;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask destroyLayer;
    public int attackDamage = 2;
    public LayerMask groundLayerMask;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        attack = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (attack == true)
        {

            animator.SetBool("IsAttacking", true);

            Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, destroyLayer);

            foreach (Collider2D enemy in hitEnemy)
            {
               enemy.GetComponent<Box>().TakeDamage(attackDamage);
            }
        }
        else
        {
            Move();
            attack = false;
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attack = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attack = false;
            animator.SetBool("IsAttacking", false);
        }
    }

    void Move()
    {
        float x = player.transform.position.x;
        float px = player.transform.position.x;
        float ex = transform.position.x;
        animator.SetBool("Walk", true);

        if (ex < px)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

    }

    public void AttackFinished()
    {
        animator.SetBool("IsAttacking", false);
    }

}
