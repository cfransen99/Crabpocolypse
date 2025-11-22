using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboCrab : EnemyCrab
{
    public float attackDistance;
    private float initSpeed;
    public float newSpeed;
    [SerializeField]protected EnemyStates currentState;

    //States of the enmey: Chasing is when chasing player, Patrolling is when patrolling from 1 location to the next
    protected enum EnemyStates
    {
        Patrolling,
        Chasing
    }

    public override void Start()
    {
        base.Start();
        //Sets initial speed
        initSpeed = agent.speed;
    }

    //Moves eney
    public override void Move()
    {
        base.Move();

        //If the player is close enough the enemy is chasing, otherwise it is patrolling
        if (distance > attackDistance)
        {
            currentState = EnemyStates.Patrolling;
        }
        else
        {
            currentState = EnemyStates.Chasing;
        }

        //If patrolling, speed is the original speed
        if (currentState == EnemyStates.Patrolling)
        {
            agent.speed = initSpeed;
        }
        //If chasing, speed is the new speed and the destination is now the player
        else if (currentState == EnemyStates.Chasing)
        {
            agent.speed = newSpeed;
            if (agent.enabled)
            {
                agent.SetDestination(player.position);
            }
        }
    }
}
