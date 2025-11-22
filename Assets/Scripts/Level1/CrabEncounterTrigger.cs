using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CrabEncounterTrigger : TriggerBase
{
    [SerializeField] private CinemachineVirtualCamera crabCam;
    private DialogueTrigger dialogueTrigger;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(!dialogueTrigger.isTriggered)
            {
                crabCam.Priority = 11;
                dialogueTrigger.TriggerDialogue(crabCam);
            }
        }
    }
}
