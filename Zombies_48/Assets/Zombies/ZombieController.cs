using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public GameObject target;
    Animator anim;
    NavMeshAgent agent;

    enum STATE { IDLE, WANDER, ATTACK, CHASE, DEAD };
    STATE state = STATE.IDLE;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
    }
    
    void TurnOffTriggers()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case STATE.IDLE:
                state = STATE.WANDER;
                break;
            case STATE.WANDER:
                if (!agent.hasPath) {
                    float newX = this.transform.position.x + Random.Range(-5, 5);
                    float newZ = this.transform.position.z + Random.Range(-5, 5);
                    float newY = Terrain.activeTerrain.SampleHeight(new Vector3(newX, 0, newZ));
                    Vector3 dest = new Vector3(newX, newY, newZ);
                    agent.SetDestination(dest);
                    agent.stoppingDistance = 0;
                    TurnOffTriggers();
                    anim.SetBool("isWalking", true);

                    //  debug
                    //  if (this.transform.name == "jill")
                    //  {
                    //      Vector3 agentDest = agent.destination;
                    //      Debug.Log("name = " + this.transform.name + " newX = " + newX + " newY = " + newY + " newZ = " + newZ);
                    //      Debug.Log("name = " + this.transform.name + " agentDestX = " + agentDest.x + " agentDestY = " + agentDest.y + " agentDestZ = " + agentDest.z);
                    //  }
                }
                break;
            case STATE.CHASE:
                break;
            case STATE.ATTACK:
                break;
            case STATE.DEAD:
                break;
        }
    }
}
