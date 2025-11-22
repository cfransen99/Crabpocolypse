using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LincrabCannons : MonoBehaviour
{
    [SerializeField] private Transform missleSpawn;


    [SerializeField] private float initOffset;
    private float offset;
   
    [SerializeField] private float rateOfFire;
    private float lastFired;

    public bool canShoot;

    [SerializeField] private GameObject[] missles;

    private void Start()
    {
        lastFired = Time.time;
        offset = initOffset;
    }

    private void Update()
    {
        offset -= Time.deltaTime;
        //Debug.Log(offset + " " + gameObject.name);
        if(canShoot && offset < 0)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (Time.time - lastFired > 1 / rateOfFire)
        {
            lastFired = Time.time;
            ChooseProjectile();
        }
    }

    private void ChooseProjectile()
    {
        int random = Random.Range(1, 7);

        Instantiate(missles[random], missleSpawn.position, missleSpawn.rotation);
    }
}
