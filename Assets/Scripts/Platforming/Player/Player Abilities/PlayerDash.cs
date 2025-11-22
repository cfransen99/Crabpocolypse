using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : PlayerAbilityBase
{
    [SerializeField] protected float dashForce;
    [SerializeField] protected float dashTime;

    protected bool canDash;
    protected bool isDashing;
    public virtual IEnumerator DashLength()
    {
        stats.playerDrive -= cost;
        Use();
        player.animPlayer.SetBool("isRunning", true);
        yield return new WaitForSeconds(dashTime);
        StopDash();
    }

    public override void Use()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        isDashing = true;
        if (playerMove.lookRight)
        {
            r.AddForce(Vector2.right * dashForce, ForceMode.Impulse);
        }
        else
        {
            r.AddForce(Vector2.left * dashForce, ForceMode.Impulse);
        }
    }

    public bool IsDashing()
    {
        return isDashing;
    }

    public virtual void StopDash()
    {
        isDashing = false;
        timeBetweenAbility = abilityCooldown;
    }
}
