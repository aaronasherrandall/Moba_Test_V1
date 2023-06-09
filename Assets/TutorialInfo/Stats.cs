using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public float attackDmg;
    public float attackSpeed;
    public float attackTime;

    HeroCombat heroCombat;

    // Start is called before the first frame update
    void Start()
    {
        heroCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 )
		{
            Destroy(gameObject);
            heroCombat.targetedEnemy = null;
            heroCombat.performMeleeAttack = false;
		}
    }
}
