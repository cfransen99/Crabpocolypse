using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Crab0FightDialogueTrigger : MonoBehaviour
{
    DialogueTrigger dialogueTrigger;
    public CinemachineVirtualCamera[] dialogueCams;

    [SerializeField] private int[] changeCamIndex;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        yield return new WaitForSeconds(.2f);
        if(other.gameObject.tag == "Player")
        {
            if (!dialogueTrigger.isTriggered)
            {
                other.GetComponent<Animator>().SetTrigger("isHurt");
                dialogueCams[0].Priority = 11;
                dialogueTrigger.TriggerDialogue(dialogueCams, changeCamIndex);
            }
        }
    }
}
