using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetractableSpikeTrap : TrapBase
{
    public Transform[] points = new Transform[2];

    [SerializeField] float speed;

    private float distance;

    private void Update()
    {
        distance = transform.position.x - player.transform.position.x;
        Act();
    }

    public void GoToPoint(int dest)
    {
        transform.position = points[dest].position;
    }

    public override void Act()
    {
        if (distance < 3)
        {
            GoToPoint(0);
        }
        else
        {
            GoToPoint(1);
        }
    }
}
