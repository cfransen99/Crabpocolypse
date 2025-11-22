using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform sendTo;

    private PlayerMovement playerMove;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMovement>();   
    }

    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E) && playerMove.canMove)
        {
            playerMove.transform.position = new Vector3(sendTo.position.x, sendTo.position.y + 1, sendTo.position.z);
        }
    }

    public void Uninteract()
    {

    }
}
