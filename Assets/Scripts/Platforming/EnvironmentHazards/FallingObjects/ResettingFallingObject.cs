using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResettingFallingObject : FallingObjectBase
{
    protected Vector3 startingPos;
    protected GameObject player;

    [SerializeField] protected float speed;
    [SerializeField] protected float resetTimer;

    protected bool isReseting;

    public override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startingPos = transform.position;
        base.Start();
    }

    public override void Update()
    {
        if(isReseting)
        {
            transform.position = Vector3.MoveTowards(transform.position, startingPos, speed * Time.deltaTime);
        }
        if(transform.position == startingPos && isReseting)
        {
            isReseting = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        base.Update();
    }

    IEnumerator WaitToReturn()
    {
        yield return new WaitForSeconds(resetTimer);
        GetComponent<Rigidbody>().useGravity = false;
        isReseting = true;
        isActive = false;
    }

    public override void Activate()
    {
        if(!isReseting)
        {
            isActive = true;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            GetComponent<Rigidbody>().useGravity = true;
            StartCoroutine(WaitToReturn());
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.tag == "Ground" && collision.gameObject.name != "PressurePlate")
        {
            float rangeLeft = player.transform.position.x - 10.0f;
            float rangeRight = player.transform.position.x + 10.0f;
            if (transform.position.x <= rangeRight && transform.position.x >= rangeLeft)
            {
                sm.sfxPlayer.PlayOneShot(sm.soundLandMedium);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Battery")
        {
            other.GetComponent<Battery>().Activate();
        }
    }
}
