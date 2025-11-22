using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDropKick : PlayerAbilityBase
{
    public float kickForce;

    public override void Use()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundPLand);
        timeBetweenAbility = abilityCooldown;

        //If the player is looking right, dropkick right
        //Same for left
        if(playerMove.lookRight)
        {
            r.AddForce(new Vector2(1, -1) * kickForce, ForceMode.Impulse);
        }
        else
        {
            r.AddForce(new Vector2(-1, -1) * kickForce, ForceMode.Impulse);
        }
    }

    public float TimeBetweenDropkicks()
    {
        return timeBetweenAbility;
    }
}
