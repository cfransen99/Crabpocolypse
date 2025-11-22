using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level13Manager : MonoBehaviour
{
    private DialogueTrigger startDialogue;
    private PlayerMovement pm;

    public MenuStory story;
    public bool cutsceneFinished = false;
    private bool onlySendOnce = false;
    private bool onlySendOnce2 = false;

    private void Awake()
    {
        startDialogue = GetComponent<DialogueTrigger>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    private IEnumerator Start()
    {
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