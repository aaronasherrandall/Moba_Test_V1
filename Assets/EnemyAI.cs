using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState { Idle, Chase, Attack, ReturnToIdle }
    public EnemyState currentState;

    public GameObject enemySpawnPoint;
    public float chaseRadius;
    public float attackRange;
    public float rotateSpeed;
    public float returnToIdleThreshold = 1f;

    private NavMeshAgent agent;
    private Stats enemyStats;
    private Animator enemyAnim;
    private EnemyCombat enemyCombat;

    // Detecting the Player
    public GameObject player;
    public float detectionRadius = 10f;
    private SphereCollider detectionSphere;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<Stats>();
        enemyAnim = GetComponent<Animator>();
        // DONT WANT TO DO THIS NEEDS TO BE SET DYNAMICALLY
        player = GameObject.FindGameObjectWithTag("Player");
        currentState = EnemyState.Idle;
        detectionSphere = GetComponent<SphereCollider>();
        detectionSphere.radius = detectionRadius;

        //ATTACK
        enemyCombat = GetComponent<EnemyCombat>();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Chase:
                ChasePlayer();
                break;
            case EnemyState.Attack:
                AttackPlayer();
                break;
            case EnemyState.ReturnToIdle:
                ReturnToIdle();
                break;
        }
    }

    void Idle()
    {
        
        agent.isStopped = true;
        StopCoroutine(enemyCombat.MeleeAttackInterval());
        enemyAnim.SetBool("Basic Attack", false);
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionSphere.radius)
        {
            currentState = EnemyState.Chase;
            Debug.Log("Enemy in " + currentState);
        }
    }

    void ChasePlayer()
    {
        agent.isStopped = false;
        agent.SetDestination(player.transform.position);
        agent.stoppingDistance = attackRange;
        StopCoroutine(enemyCombat.MeleeAttackInterval());
        enemyAnim.SetBool("Basic Attack", false);
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currentState = EnemyState.Attack;
            Debug.Log("Enemy in " + currentState);
        }
        else if (Vector3.Distance(transform.position, player.transform.position) > chaseRadius)
        {
            currentState = EnemyState.ReturnToIdle;
            Debug.Log("Enemy in " + currentState);

        }
    }

    void AttackPlayer()
    {
        agent.isStopped = true;
        // Implement attack logic here
        StartCoroutine(enemyCombat.MeleeAttackInterval());
        if (Vector3.Distance(transform.position, player.transform.position) > attackRange)
        {
            currentState = EnemyState.Chase;
            Debug.Log("Enemy in " + currentState);
        }
    }

    void ReturnToIdle()
    {
        agent.isStopped = false;
        StopCoroutine(enemyCombat.MeleeAttackInterval());
        enemyAnim.SetBool("Basic Attack", false);
        // Set a destination to return to if needed
        if (Vector3.Distance(transform.position, player.transform.position) > chaseRadius)
        {
            agent.SetDestination(enemySpawnPoint.transform.position);
            Debug.Log("Enemy in " + currentState);
            // Adjust this value based on how close you want the enemy to be to the spawn point before it goes back to Idle state
            
            if (Vector3.Distance(transform.position, enemySpawnPoint.transform.position) <= returnToIdleThreshold)
            {
                currentState = EnemyState.Idle;
                Debug.Log("Enemy in " + currentState);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            Debug.Log("Detected + " + other.gameObject.tag);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = null;
        }
    }


}
