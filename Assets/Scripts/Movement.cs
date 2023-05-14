using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public NavMeshAgent agent;

    public float rotateSpeedMovement = 0.1f;
    public float rotateVelocity;

    private HeroCombat heroCombat;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        heroCombat = GetComponent<HeroCombat>();
    }

    // Update is called once per frame
    void Update()
    {

        if(heroCombat.targetedEnemy != null)
		{
            if(heroCombat.targetedEnemy.GetComponent<HeroCombat>() != null)
			{
                if (!heroCombat.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    heroCombat.targetedEnemy = null;
                }
            }
            
		}



        if(Input.GetMouseButtonDown(0))
		{
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
			{
                if(hit.collider.tag == "Ground")
				{
                    // MOVEMENT
                    // Have the player move to the raycast point
                    agent.SetDestination(hit.point);
                    heroCombat.targetedEnemy = null;
                    agent.stoppingDistance = 0;

                    // ROTATION
                    Quaternion rotationLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,
                        rotationLookAt.eulerAngles.y,
                        ref rotateVelocity,
                        rotateSpeedMovement * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }
               
			}
		}
    }
}
