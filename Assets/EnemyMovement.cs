using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;

    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    private EnemyCombat enemyCombat;
    private EnemyAI enemyAI;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        enemyCombat = GetComponent<EnemyCombat>();
        enemyAI = GetComponent<EnemyAI>();
    }


}