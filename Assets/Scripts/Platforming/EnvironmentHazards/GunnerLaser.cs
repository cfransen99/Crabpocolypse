using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerLaser : MonoBehaviour
{
    public Gunner connectedGun;

    public void SendShoot()
    {
        connectedGun.SendMessage("ShootGun");
    }
}
