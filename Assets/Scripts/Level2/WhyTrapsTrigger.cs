using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhyTrapsTrigger : TriggerBase
{
    private DialogueTrigger dialogueTrigger;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!dialogueTrigger.isTriggered)
            {
                dialogueTrigger.TriggerDialogue();
            }
        }
    }
}
