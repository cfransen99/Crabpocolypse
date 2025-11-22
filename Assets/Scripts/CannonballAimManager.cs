using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballAimManager : MonoBehaviour
{
    public GameObject cannonballAimer;

    private Transform player;
    
    public float rateOfFire;
    private float lastFired;

    public Transform fireAt;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        lastFired = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastFired > rateOfFire)
        {
            lastFired = Time.time;
            Fire();
        }
    }

    private Vector3 Aim()
    {
        return new Vector3(player.position.x, player.position.y - .58f, player.position.z);
    }

    private void Fire()
    {
        fireAt.position = Aim();

        Instantiate(cannonballAimer, fireAt.position, fireAt.rotation);
    }
}
