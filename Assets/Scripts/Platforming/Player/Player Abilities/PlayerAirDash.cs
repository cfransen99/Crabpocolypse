using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirDash : PlayerDash
{
    public IEnumerator DashUpLength()
    {
        stats.playerDrive -= cost;
        DashUp();
        yield return new WaitForSeconds(dashTime);
        this.StopDash();
    }

    public override void StopDash()
    {
        base.StopDash();
        r.velocity = new Vector3(0, r.velocity.y, 0);
    }

    public void DashUp()
    {
        if(CanUse())
        {
            sm.sfxPlayer.PlayOneShot(sm.soundDash);
            isDashing = true;
            r.velocity = Vector3.zero;
            r.AddForce(Vector2.up * (dashForce / 2), ForceMode.Impulse);
        }
    }
}
