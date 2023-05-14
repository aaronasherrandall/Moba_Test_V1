using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float attackDmg;
    public float attackSpeed;
    public float attackTime;

    EnemyCombat enemyCombat;

    // Start is called before the first frame update
    void Start()
    {
        enemyCombat = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            enemyCombat.targetedEnemy = null;
            enemyCombat.performMeleeAttack = false;
        }
    }
}
