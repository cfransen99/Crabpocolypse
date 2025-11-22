using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateFallingObject : PressurePlateBase
{

    public GameObject fallingObject;

    [SerializeField] private float dropTime = .5f;

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != fallingObject)
        {
            Activate();
        }
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != fallingObject)
        {
            animator.SetBool("Activate", false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Activate();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            animator.SetBool("Activate", false);
        }
    }


    IEnumerator WaitToDrop()
    {
        yield return new WaitForSeconds(dropTime);
        fallingObject.GetComponent<FallingObjectBase>().Activate();
    }

    public override void Activate()
    {
        base.Activate();
        StartCoroutine(WaitToDrop());
    }
}
