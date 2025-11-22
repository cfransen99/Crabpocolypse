using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour, IInteractable
{
    private SaveHandler saveHandler;
    private PlayerMovement playerMove;
    private Player player;
    private SoundManager sm;

    private bool isInteracting;

    private void Start()
    {
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        playerMove = FindObjectOfType<PlayerMovement>();
        player = playerMove.GetComponent<Player>();
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
    }

    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerMove.canMove)
        {
            saveHandler.SaveAtPoint();
            sm.sfxPlayer.PlayOneShot(sm.soundKaching);
            Debug.Log("Saved!");
        }
    }

    public void Uninteract()
    {

    }
}
