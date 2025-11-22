using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBattery : Battery
{

    [SerializeField] private GameObject[] doors;

    public override void Activate()
    {
        dco.isActive[batteryID] = true;

        foreach (GameObject door in doors)
        {
            if (door != null)
            {
                door.GetComponent<Animator>().SetTrigger("Open");
            }
        }

        Destroy(gameObject);

        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject, .5f);
    }

}
