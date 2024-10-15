using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behaviour : MonoBehaviour
{
    //Public Variables
    public Transform raycast;
    public LayerMask raycastMask;
    public float raycastLength;
    public float attackDistance;
    public float moveSpeed;
    public float cooldownTimer;

    //Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

    void Awake()
    {
        intTimer = cooldownTimer;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if(inRange)
        {
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength, raycastMask);
            RaycastDebug();
            animator.SetBool("IsAttacking", true);
        }

        if (hit.collider != null)
        {
            EnemyLogic();
            animator.SetBool("IsAttacking", true);
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (!inRange == false)
        {
            animator.SetBool("IsAttacking", false);
            StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Player")
        {
            target = trigger.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        animator.SetBool("Walk", true);
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    void Move()
    {
        animator.SetBool("Walk", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(""))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

    }

    void Attack()
    {
        animator.SetBool("Walk", false);
        animator.SetBool("IsAttacking", true);
        cooldownTimer = intTimer;
        attackMode = true;
    }

    void StopAttack()
    {
        animator.SetBool("IsAttacking", false);
        cooling = false;
        attackMode = false;
    }


    void RaycastDebug()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.green);
        }
    }
}
