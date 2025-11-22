using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightFallingObject : ResettingFallingObject
{
    public void OnTriggerEnter(Collider collision)
    {
        if (isActive)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().PlayDieAnim();
            }
            else if (collision.gameObject.tag == "Boss")
            {
                collision.gameObject.GetComponent<BossBase>().Stun();
            }
        }
    }
}
