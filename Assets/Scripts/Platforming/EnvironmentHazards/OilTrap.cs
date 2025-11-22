using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilTrap : MonoBehaviour
{
    [SerializeField] protected float damage;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!other.gameObject.GetComponent<PlayerBarrier>().UsedShield())
            {
                other.GetComponent<PlayerStats>().Damage(damage);
            }
        }
    }
}
