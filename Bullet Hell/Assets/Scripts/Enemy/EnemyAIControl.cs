using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIControl : MonoBehaviour
{
    public enum States
    {
        Patrolling,
        Waiting,
        Chase,
        Search
    }

    States currentState;

    public NavMeshAgent navMeshAgent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    [Header("Waiting")]
    public float waitTime = 2f;
    float waitedTime;

    [Header("Patrolling")]
    public Vector3 walkPoint;
    public float walkPointMinRange = 1f;
    public float walkPointRange;
    bool walkPointSet;
    float distanceToWalkPoint;

    [Header("Attacking")]
    public Weapon settings;
    bool alreadyAttacked;
    public GameObject bullets;
    public Transform firePoint;

    [Header("Chasing")]
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Search")]
    public float persistedTime = 2f;
    private float timeWithoutSight;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        walkPointSet = false;
        Patrolling();
    }

    void Update()
    {
        if(enemyHealth.currentEnemyHealth > 0 && playerHealth.currentPlayerHealth > 0)
        {
            CheckStates();
        }
    }

    void CheckStates()
    {
        if(currentState != States.Chase && PlayerInSightRange())
        {
            Chase();

            return;
        }

        switch (currentState)
        {
            case States.Patrolling:
                if (CloseToWalkPoint())
                {
                    Waiting();

                    SearchWalkPoint();
                }
                else
                {
                    Patrolling();
                }
                break;
            case States.Waiting:
                if (WaitedEnoughTime())
                {
                    Patrolling();
                }
                break;
            case States.Chase:
                if (!PlayerInSightRange())
                {
                    Search();                    
                }
                else
                {
                    ChasePlayer();
                }
                break;
            case States.Search:
                if (EnoughTimeWithoutSight())
                {
                    Waiting();
                }

                break;
        }
    }

    #region WAITING
    void Waiting()
    {
        currentState = States.Waiting;

        waitedTime = Time.time;
    }

    bool WaitedEnoughTime()
    {
        return waitedTime + waitTime <= Time.time;
    }

    #endregion WAITING

    #region PATROLLING
    void Patrolling()
    {
        currentState = States.Patrolling;
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            navMeshAgent.SetDestination(walkPoint);
        }
    }

    void SearchWalkPoint()
    {   
        float randomZPos = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomXPos = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomXPos, transform.position.y, transform.position.z + randomZPos);

        if (Physics.Raycast(walkPoint, -transform.up, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    bool CloseToWalkPoint()
    {
        distanceToWalkPoint = Vector3.Distance(transform.position, walkPoint);

        return distanceToWalkPoint <= walkPointMinRange;
    }

    #endregion PATROLLING

    #region CHASING
    void Chase()
    {
        currentState = States.Chase;
    }

    bool PlayerInSightRange()
    {
        return Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }

    void ChasePlayer()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (playerHealth.currentPlayerHealth > 0)
        {
            navMeshAgent.SetDestination(player.position);
        }

        if (playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (playerHealth.currentPlayerHealth > 0 && enemyHealth.currentEnemyHealth > 0)
        {
            navMeshAgent.SetDestination(player.position);

            transform.LookAt(player);

            if (!alreadyAttacked)
            {
                GameObject bullet = Instantiate(bullets, firePoint.position, Quaternion.identity);

                Rigidbody rb = bullet.GetComponent<Rigidbody>();

                rb.AddForce(-firePoint.forward * settings.bulletForce, ForceMode.Impulse);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), settings.weaponFireRate);
            }
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }
    #endregion CHASING

    #region SEARCH
    void Search()
    {
        currentState = States.Search;

        timeWithoutSight = Time.time;

        navMeshAgent.SetDestination(transform.position);
    }

    bool EnoughTimeWithoutSight()
    {
        return timeWithoutSight + persistedTime <= Time.time;
    }

    #endregion SEARCH

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
