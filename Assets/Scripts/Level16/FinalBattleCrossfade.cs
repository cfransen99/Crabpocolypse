using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FinalBattleCrossfade : MonoBehaviour
{
    [SerializeField] private GameObject zero;
    private DialogueTrigger dialogueTrigger;

    [SerializeField] private PlayerMovement player;
    [SerializeField] private CinemachineVirtualCamera cam;

    private DialogueManager dialogueManager;

    private bool startedDialogue;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if(dialogueManager.noActiveDialogue && startedDialogue)
        {
            zero.GetComponent<ZeroFinalDialogueTrigger>().BeginDialogue();
            startedDialogue = false;
        }
    }

    public void SpawnZero()
    {
        zero.SetActive(true);

        player.DisableMovement();
    }

    public void StartLastDialogue()
    {
        cam.Priority = 11;
        dialogueTrigger.TriggerDialogue(cam);
        startedDialogue = true;
    }
}
