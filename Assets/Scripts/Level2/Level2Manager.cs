using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : LevelManagerBase
{
    private DialogueTrigger dialogue;

    public Animator doorAnim;

    private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        dialogue = GetComponent<DialogueTrigger>();
        player = FindObjectOfType<Player>();
    }

    private IEnumerator Start()
    {

        doorAnim.SetBool("isOpen", false);

        yield return new WaitForSeconds(.5f);
        if (!dialogue.isTriggered)
        {
            dialogue.TriggerDialogue();

            player.LearnAir();
            player.LearnDash();
        }
    }
}
