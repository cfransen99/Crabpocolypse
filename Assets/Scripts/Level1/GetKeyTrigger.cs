using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKeyTrigger : TriggerBase
{
    private bool hasKey;
    private DialogueTrigger dialogue;

    private void Start()
    {
        dialogue = GetComponent<DialogueTrigger>();

        if(dialogue.isTriggered)
        {
            FindObjectOfType<Level1Manager>().Progress();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            hasKey = other.GetComponent<KeyItemInventory>().CheckInv("Key");
            if(hasKey)
            {
                FindObjectOfType<Level1Manager>().Progress();
                GetComponent<DialogueTrigger>().TriggerDialogue();
            }
            other.GetComponent<KeyItemInventory>().RemoveItem("Key");
            hasKey = false;
        }
    }

}
