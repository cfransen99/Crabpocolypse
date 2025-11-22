using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level11Manager : MonoBehaviour
{
    private DialogueTrigger startDialogue;

    private void Awake()
    {
        startDialogue = GetComponent<DialogueTrigger>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);

        if (!startDialogue.isTriggered)
        {
            startDialogue.TriggerDialogue();
        }
    }
}
