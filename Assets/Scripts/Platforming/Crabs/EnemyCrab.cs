using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrab : EnemyBase
{
    public Transform[] points = new Transform[2];   
    [SerializeField]protected int destination = 0;

    //Moves enemy along set path
    public override void Move()
    {
        base.Move();
        if (agent.enabled && !agent.pathPending && agent.remainingDistance < .5)
        {
            GoToNextPoint();
        }
    }

    //Sets the new destination and the path to it
    public void GoToNextPoint()
    {
        agent.SetDestination(points[destination].position);
        destination = (destination + 1) % points.Length;
    }

    public override void Attack()
    {
        
    }
}
