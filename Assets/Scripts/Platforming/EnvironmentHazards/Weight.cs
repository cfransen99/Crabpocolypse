using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weight : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bc;
    private bool isGrabbed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ClawAttack")
        {
            other.transform.parent.GetComponentInParent<Claw>().grabTrigger.enabled = false;
            other.transform.parent.GetComponentInParent<Claw>().load = gameObject;
            transform.parent = other.transform.parent.GetComponentInParent<Claw>().loadPos;
            transform.position = other.transform.parent.GetComponentInParent<Claw>().loadPos.position;
            rb.isKinematic = true;
            rb.useGravity = false;
            bc.enabled = false;
        }
    }

    public void DropFromClaw()
    {
        transform.parent = null;
        rb.isKinematic = false;
        rb.useGravity = true;
        bc.enabled = true;
        bc.isTrigger = true;
    }
}
