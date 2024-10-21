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
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask destroyLayer;
    public int attackDamage = 20;
    public LayerMask enemyLayers;


    //Private Variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

    Health health;
    PlayerHealth playerHealth;
    void Awake()
    {
        intTimer = timer;
        //health = gameObject.AddComponent<Health>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(inRange)
        {
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength);
            RaycastDebug();
        }

        //When Player is Detected
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
            target = trigger.gameObject;
            inRange = true;
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if (distance > attackDistance)
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
            Cooldown();
            anim.SetBool("IsAttacking", false);
        }
    }

    void Move()
    {
        anim.SetBool("Walk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;

        anim.SetBool("Walk", false);
        anim.SetBool("IsAttacking", true);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    void Cooldown()
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
        anim.SetBool("IsAttacking", false );
    }

    void RaycastDebug()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.red);
        }
        else if(attackDistance < distance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        cooling = true;
    }
}
