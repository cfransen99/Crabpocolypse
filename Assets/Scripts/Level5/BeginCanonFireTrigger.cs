using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginCanonFireTrigger : TriggerBase
{

    [SerializeField] private GameObject canonballManager;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canonballManager.SetActive(true);
        }
    }
}
