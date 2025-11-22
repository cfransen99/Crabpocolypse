using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBridge : MonoBehaviour, IInteractable
{
    private PlayerMovement playerMove;
    private Player player;

    public Transform moveTo;
    public GameObject disappearWall;

    [SerializeField] private int upOrDownAxis;
    [SerializeField] private GameObject arrow;

    public bool canTravel = true; 

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMovement>();
        player = playerMove.GetComponent<Player>();
    }

    public void Interact()
    {
        if(upOrDownAxis > 0)
        {
            if (Input.GetKeyDown(KeyCode.W) && playerMove.canMove && canTravel)
            {
                playerMove.DisableMovement();
                playerMove.MoveOnZ(moveTo);
                if (disappearWall != null)
                {
                    disappearWall.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("activating arrow");
            arrow.SetActive(true);

            if (Input.GetKeyDown(KeyCode.S) && playerMove.canMove && canTravel)
            {
                playerMove.DisableMovement();
                playerMove.MoveOnZ(moveTo);
                if (disappearWall != null)
                {
                    disappearWall.SetActive(true);
                }
            }
                
        }
    }

    public void Uninteract()
    {
        Debug.Log("exiting cross trigger");
        if (arrow != null)
        {
            Debug.Log("removing arrow");
            arrow.SetActive(false);
        }
    }
}
