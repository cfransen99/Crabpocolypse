using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoyTrapsButton : ButtonLeverBase
{
    [SerializeField] private GameObject[] traps;

    public override void Interact()
    {
        isActive = true;
        gameObject.GetComponent<Renderer>().material.color = Color.green;
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
        GetComponent<BoxCollider>().enabled = false;
        dco.isActive[ID] = isActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Interact();
        }
    }
}
