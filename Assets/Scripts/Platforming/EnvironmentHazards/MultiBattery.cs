using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBattery : Battery
{
    [SerializeField] private MultiBatteryDoor[] doors;
    [SerializeField] private int batteryNum;

    public override void Activate()
    {
        dco.isActive[batteryID] = true;

        foreach (MultiBatteryDoor door in doors)
        {
            door.isDestroyed(batteryNum);
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject, .5f);
    }
}
