using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateQuestion : PressurePlateBase
{
    public bool isCurrectAnswer;

    [SerializeField] private GameObject door;
    [SerializeField] private PressurePlateQuestion plate;
    [SerializeField] private GameObject[] texts = new GameObject[2];
    [SerializeField] private CrossBridge[] crossing = new CrossBridge[2];

    private bool doOnce = false;

    public override void OnCollisionEnter(Collision collision)
    {
        Activate();
    }

    public override void OnCollisionExit(Collision collision)
    {
        animator.SetBool("Activate", false);
    }

    public override void Activate()
    {
        base.Activate();
        if(!doOnce)
        {
            if (isCurrectAnswer)
            {
                door.GetComponent<Animator>().SetTrigger("Close");
                plate.doOnce = true;
                if(crossing != null)
                {
                    foreach (CrossBridge cross in crossing)
                    {
                        cross.canTravel = true;
                    }
                }
                foreach (GameObject text in texts)
                {
                    Destroy(text);
                }
            }
            else
            {
                FindObjectOfType<PlayerStats>().Damage(5);
            }
            doOnce = true;
        }
    }
}
