using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabsaluteZero : BossBase
{
    public override void Move()
    {
        animator.SetBool("isRunning", true);
        agent.SetDestination(player.position);
    }
}
