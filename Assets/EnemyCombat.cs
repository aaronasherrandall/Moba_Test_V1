using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public enum EnemyAttackType { Melee, Ranged };
    public EnemyAttackType enemyAttackType;

    public GameObject targetedEnemy;

    public GameObject targetedPlayer;
    public float attackRange;
    public float rotateSpeedForAttack;

    public EnemyMovement enemyMovement;
    private Stats enemyStats;
    private Animator enemyAnim;
    private EnemyAI enemyAI;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyStats = GetComponent<Stats>();
        enemyAnim = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyAI.player != null)
        {
            // if distance between unit and targeted enemy is greater than attack range
            // have unit move towards the enemy
            if (Vector3.Distance(gameObject.transform.position, enemyAI.player.transform.position) > attackRange)
            {
                enemyMovement.agent.SetDestination(enemyAI.player.transform.position);
                // stop the unit at the attack range
                enemyMovement.agent.stoppingDistance = attackRange;

                // ROTATION
                Quaternion rotationLookAt = Quaternion.LookRotation(enemyAI.player.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationLookAt.eulerAngles.y,
                    ref enemyMovement.rotateVelocity,
                    rotateSpeedForAttack * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
            {
                if (enemyAttackType == EnemyAttackType.Melee)
                {
                    if (performMeleeAttack)
                    {
                        Debug.Log("Attack the Minion");
                        // Start Coroutine
                        StartCoroutine(MeleeAttackInterval());
                    }
                }
            }
        }
    }

    public IEnumerator MeleeAttackInterval()
    {
        performMeleeAttack = false;
        enemyAnim.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(enemyStats.attackTime / ((100 + enemyStats.attackTime) * 0.01f));

        if (enemyAI.player == null)
        {
            enemyAnim.SetBool("Basic Attack", false);
            performMeleeAttack = true;
        }

    }

    public void MeleeAttack()
    {
        if (enemyAI.player != null)
        {
            if (enemyAI.player.GetComponent<Targetable>().characterType == Targetable.CharacterType.FootSoldier)
            {
                enemyAI.player.GetComponent<Stats>().health -= enemyStats.attackDmg;
            }
        }

        performMeleeAttack = true;
    }

    public void AttackTest()
	{
        Debug.Log("ENEMY ATTACKING!");
	}
}
