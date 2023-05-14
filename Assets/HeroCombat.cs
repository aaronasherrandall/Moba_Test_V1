using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCombat : MonoBehaviour
{
    public enum HeroAttackType { Melee, Ranged };
    public HeroAttackType heroAttackType;

    public GameObject targetedEnemy;
    public float attackRange;
    public float rotateSpeedForAttack;

    public Movement movement;
    private Stats stats;
    private Animator anim;

    public bool basicAtkIdle = false;
    public bool isHeroAlive;
    public bool performMeleeAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        stats = GetComponent<Stats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(targetedEnemy != null)
		{
            // if distance between unit and targeted enemy is greater than attack range
            // have unit move towards the enemy
            if(Vector3.Distance(gameObject.transform.position, targetedEnemy.transform.position) > attackRange)
			{
                movement.agent.SetDestination(targetedEnemy.transform.position);
                // stop the unit at the attack range
                movement.agent.stoppingDistance = attackRange;

                // ROTATION
                Quaternion rotationLookAt = Quaternion.LookRotation(targetedEnemy.transform.position - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                    rotationLookAt.eulerAngles.y,
                    ref movement.rotateVelocity,
                    rotateSpeedForAttack * (Time.deltaTime * 5));

                transform.eulerAngles = new Vector3(0, rotationY, 0);
            }
            else
			{
                if(heroAttackType == HeroAttackType.Melee)
				{
                    if(performMeleeAttack)
					{
                        Debug.Log("Attack the Minion");
                        // Start Coroutine
                        StartCoroutine(MeleeAttackInterval());
					}
				}
			}
		}
    }

    private IEnumerator MeleeAttackInterval()
	{
        performMeleeAttack = false;
        anim.SetBool("Basic Attack", true);

        yield return new WaitForSeconds(stats.attackTime / ((100 + stats.attackTime) * 0.01f));

        if(targetedEnemy == null)
		{
            anim.SetBool("Basic Attack", false);
            performMeleeAttack = true;
		}

	}

    public void MeleeAttack()
	{
        if(targetedEnemy != null)
		{
            if(targetedEnemy.GetComponent<Targetable>().characterType == Targetable.CharacterType.Minion)
			{
                targetedEnemy.GetComponent<Stats>().health -= stats.attackDmg;
			}
		}

        performMeleeAttack = true;
	}
}
