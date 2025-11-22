using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TriggersDialogue : MonoBehaviour
{
    private DialogueTrigger startDialogue;
    [SerializeField] private CinemachineVirtualCamera cam;

    public bool isTriggered;
    public int ID;

    private SaveHandler saveHandler;
    private DataCarryOver dco;

    private void Awake()
    {
        startDialogue = GetComponent<DialogueTrigger>();
    }

    private IEnumerator Start()
    {

        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();

        yield return new WaitForSeconds(.15f);
        if (saveHandler.GetDidLoadSave())
        {
            isTriggered = saveHandler.isTriggered[ID];
        }
        else
        {
            isTriggered = dco.isTriggered[ID];
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            if (other.gameObject.tag == "Player")
            {
                isTriggered = true;
                if (cam == null)
                {
                    startDialogue.TriggerDialogue();
                }
                else
                {
                    cam.Priority = 11;
                    startDialogue.TriggerDialogue(cam);
                }
            }
        }
    }
}
