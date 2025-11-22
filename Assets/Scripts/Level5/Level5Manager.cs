using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Manager : MonoBehaviour
{
    private DialogueTrigger startDialogue;
    private PlayerMovement pm;

    public MenuStory story;
    public bool cutsceneFinished = false;
    private bool onlySendOnce = false;
    private bool onlySendOnce2 = false;

    private DataCarryOver dco;
    private SaveHandler saveHandler;

    private void Awake()
    {
        startDialogue = GetComponent<DialogueTrigger>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        dco.learnJump = true;
        pm.GetComponent<Player>().learnJump = true;

        yield return new WaitForSeconds(.5f);
        StartCoroutine("Test");
    }

    // Update is called once per frame
    void Update()
    {
        if (story.storyFinished && !onlySendOnce)
        {
            onlySendOnce = true;
            cutsceneFinished = true;
            pm.EnableMovement();
        }
        if (cutsceneFinished && !onlySendOnce2)
        {
            onlySendOnce2 = true;
            StartCoroutine("InitialDialogue");
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(.1f);

        if (!startDialogue.isTriggered)
        {
            pm.DisableMovement();
            story.DoStory();
        }
        else
        {
            cutsceneFinished = true;
        }
    }

    IEnumerator InitialDialogue()
    {
        yield return new WaitForSeconds(.1f);

        if (!startDialogue.isTriggered)
        {
            startDialogue.TriggerDialogue();
        }
    }
}
