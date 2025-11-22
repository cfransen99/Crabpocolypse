using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] protected float health;

    [SerializeField] private GameObject[] traps;

    [SerializeField] protected GameObject explosionEffect;

    [SerializeField] protected int batteryID;

    protected DataCarryOver dco;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();


        if(dco.isActive[batteryID])
        {
            Activate();
        }
    }

    public void Damage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Activate();
        }
    }

    public virtual void Activate()
    {
        dco.isActive[batteryID] = true;
        foreach (GameObject trap in traps)
        {
            if (trap != null)
            {
                if (trap.tag == "GunTrap")
                {
                    trap.GetComponent<Gunner>().DisableGun();
                }
                else
                {
                    Destroy(trap);
                }
            }
        }

        Instantiate(explosionEffect, transform.position, transform.rotation);

        Destroy(gameObject, .5f);
    }
}
