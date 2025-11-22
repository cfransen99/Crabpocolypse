using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool isTriggered, willEnableOverdrive;

    public int ID;

    private DataCarryOver dco;
    private SaveHandler saveHandler;

    private IEnumerator Start()
    {

        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();

        yield return new WaitForSeconds(.1f);
        if (saveHandler.GetDidLoadSave())
        {
            isTriggered = saveHandler.isTriggered[ID];
        }
        else
        {
           isTriggered = dco.isTriggered[ID];
        }
    }

    public void TriggerDialogue()
    {
        if(!isTriggered)
        {
            isTriggered = true;
            dco.isTriggered[ID] = isTriggered;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        if (willEnableOverdrive)
        {
            dco.overdriveLocked = false;
        }
    }

    public void TriggerDialogue(CinemachineVirtualCamera cam)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
            dco.isTriggered[ID] = isTriggered;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, cam);
        }
        if (willEnableOverdrive)
        {
            dco.overdriveLocked = false;
        }
    }

    public void TriggerDialogue(CinemachineVirtualCamera[] cam, int[] nums)
    {
        if (!isTriggered)
        {
            isTriggered = true;
            dco.isTriggered[ID] = isTriggered;
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, cam, nums);
        }
        if (willEnableOverdrive)
        {
            dco.overdriveLocked = false;
        }
    }
}
