using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabwich : BackCrab
{
    public GameObject disguise;

    //Destroys the sandwich diguise and acts like the Back Crab
    public override void Attack()
    {
        base.Attack();

        if(isSeen)
        {
            Destroy(disguise);
        }
    }
}
