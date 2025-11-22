using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistantFallingObject : FallingObjectBase
{
    public override void Start()
    {
        StartCoroutine(WaitToDestroy());
        base.Start();
    }

    public override void Activate()
    {

    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    public void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<ConveyorBelt>() != null)
        {
            collision.gameObject.GetComponent<ConveyorBelt>().RemoveFromBelt(gameObject);
        }
    }
}
