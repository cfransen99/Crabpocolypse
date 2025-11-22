using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Level1Manager : LevelManagerBase
{
    private DialogueTrigger dialogue;

    public DialogueTrigger progressTrigger;
    public Player player;
    PlayerStats stats;

    [SerializeField] private CinemachineVirtualCamera[] keyCam;
    public int[] indexes;

    private void Awake()
    {
        dialogue = GetComponent<DialogueTrigger>();

        stats = player.GetComponent<PlayerStats>();

        stats.overdriveLocked = true;
        stats.playerDrive = 0;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);

        if(!dialogue.isTriggered)
        {
            keyCam[0].Priority = 11;
            dialogue.TriggerDialogue(keyCam, indexes);
        }
    }

    public void Progress()
    {
        if (stats.playerDrive == 0)
        {
            stats.overdriveLocked = false;
            player.ChangeDrive(100);
        }
    }
}
