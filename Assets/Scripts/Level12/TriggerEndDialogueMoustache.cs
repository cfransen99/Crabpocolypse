using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndDialogueMoustache : MonoBehaviour
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
        if (dialogueManager.noActiveDialogue && dialogueTriggered)
        {
            dco.ResetCarryOver();
            levelLoader.LoadNewLevel("Level13");
            dialogueTriggered = false;
        }
    }

    public IEnumerator FadedAway()
    {
        dialogue.TriggerDialogue();
        yield return new WaitForSeconds(2);
        dialogueTriggered = true;
    }
}
