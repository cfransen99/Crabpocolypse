using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueDialogueTrigger : MonoBehaviour
{
    private DialogueTrigger dialogue;

    private bool dialogueTriggered;

    private LevelLoader levelLoader;

    private DialogueManager dialogueManager;

    private DataCarryOver dco;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        dialogue = GetComponent<DialogueTrigger>();
        levelLoader = FindObjectOfType<LevelLoader>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if(dialogueManager.noActiveDialogue && dialogueTriggered)
        {
            dco.ResetCarryOver();
            levelLoader.LoadNewLevel("Level9");
            dialogueTriggered = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Clawdius")
        {
            Debug.Log("did trigger");
            dialogue.TriggerDialogue();
            dialogueTriggered = true;
        }
    }
}
