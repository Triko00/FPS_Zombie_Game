﻿using System.Collections;
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

    float DistanceToPlayer()
    {
        return Vector3.Distance(target.transform.position, this.transform.position);
    }

    bool CanSeePlayer()
    {
        /*if (this.transform.name == "zombiegirl")
            Debug.Log("Name = " + this.transform.name + " DistanceToPlayer = " + DistanceToPlayer());*/

        if (DistanceToPlayer() < 10)
            return true;
        return false;
    }

    bool ForgetPlayer()
    {
        if (DistanceToPlayer() > 20)
            return true;
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case STATE.IDLE:
                if (CanSeePlayer()) state = STATE.CHASE;
                else
                    state = STATE.WANDER;
                break;
            case STATE.WANDER:
                if (!agent.hasPath)
                {
                    float newX = this.transform.position.x + Random.Range(-5, 5);
                    float newZ = this.transform.position.z + Random.Range(-5, 5);
                    float newY = Terrain.activeTerrain.SampleHeight(new Vector3(newX, 0, newZ));
                    Vector3 dest = new Vector3(newX, newY, newZ);
                    agent.SetDestination(dest);
                    agent.stoppingDistance = 0;
                    TurnOffTriggers();
                    anim.SetBool("isWalking", true);

                    /* debug
                    if (this.transform.name == "jill")
                    {
                        Vector3 agentDest = agent.destination;
                        Debug.Log("name = " + this.transform.name + " newX = " + newX + " newY = " + newY + " newZ = " + newZ);
                        Debug.Log("name = " + this.transform.name + " agentDestX = " + agentDest.x + " agentDestY = " + agentDest.y + " agentDestZ = " + agentDest.z);
                    }*/
                }

                if (CanSeePlayer()) state = STATE.CHASE;

                break;
            case STATE.CHASE:
                agent.SetDestination(target.transform.position);
                agent.stoppingDistance = 5;
                TurnOffTriggers();
                anim.SetBool("isRunning", true);

                if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
                {
                    state = STATE.ATTACK;
                }

                if (ForgetPlayer())
                {
                    state = STATE.WANDER;
                    agent.ResetPath();
                }
                
                break;
            case STATE.ATTACK:
                TurnOffTriggers();
                anim.SetBool("isAttacking", true);
                this.transform.LookAt(target.transform.position);
                if (DistanceToPlayer() > agent.stoppingDistance)
                    state = STATE.CHASE;
                break;
            case STATE.DEAD:
                break;
        }
    }
}
