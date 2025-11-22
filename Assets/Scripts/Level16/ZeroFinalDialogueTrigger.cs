using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZeroFinalDialogueTrigger : MonoBehaviour
{
    private DialogueTrigger dialogue;
    [SerializeField] private CinemachineVirtualCamera cam;

    private Animator animator;

    private bool doneWithDialogue;

    private DialogueManager dialogueManager;

    [SerializeField] private Level16Manager levelManager;

    private void Start()
    {
        dialogue = GetComponent<DialogueTrigger>();
        animator = GetComponent<Animator>();
        dialogueManager = FindObjectOfType<DialogueManager>();

    }

    private void Update()
    {
        if (doneWithDialogue && dialogueManager.noActiveDialogue)
        {
            levelManager.EndTheGame();
            doneWithDialogue = false;
        }
    }

    public void BeginDialogue()
    {
        animator.SetBool("getHurt", true);
        cam.Priority = 11;
        dialogue.TriggerDialogue(cam);
        doneWithDialogue = true;
    }


}
