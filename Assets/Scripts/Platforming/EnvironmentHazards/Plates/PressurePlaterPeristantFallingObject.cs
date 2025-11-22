using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlaterPeristantFallingObject : PressurePlateBase
{
    public GameObject fallingObject;
    public Transform spawner;
    private bool doOnce;
    
    public override void Activate()
    {
        base.Activate();
        StartCoroutine(WaitToDrop());
    }

    public override void OnCollisionEnter(Collision collision)
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject != fallingObject && !doOnce)
        {
            Activate();
        }
    }

    public override void OnCollisionExit(Collision collision)
    {
        
    }

    IEnumerator WaitToDrop()
    {
        doOnce = true;
        yield return new WaitForSeconds(2f);
        doOnce = false;
        Instantiate(fallingObject, spawner.position, spawner.rotation);
    }
}
