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
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;

    //Private Variables
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

    void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(!attackMode)
        {
            Move();
        }

        if(!InsideLimit() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            SelectTarget();
        }

        if(inRange)
        {
            hit = Physics2D.Raycast(raycast.position, transform.right, raycastLength, raycastMask);
            RaycastDebug();
        }
        
        //When player is detected
        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            anim.SetBool("Walk", false);
            StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Player")
        {
            target = trigger.transform;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance)
        {
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if(cooling)
        {
            CoolDown();
            anim.SetBool("IsAttacking", false);
        }
    }

    void Move()
    {
        anim.SetBool("Walk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("Walk", false);
        anim.SetBool("IsAttacking", true);
    }

    void CoolDown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("IsAttacking", false);
    }

    public void TriggerCooling()
    { 
       cooling = true; 
    }

    private bool InsideLimit()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    private void Flip()
    {
        
    }

    void RaycastDebug()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, transform.right * raycastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(raycast.position, transform.right * raycastLength, Color.green);
        }
    }
}
