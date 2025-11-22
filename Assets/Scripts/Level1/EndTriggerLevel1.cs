using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTriggerLevel1 : MonoBehaviour, IInteractable
{
    public Doors door;

    private DataCarryOver dco;

    private Player player;

    private bool isInteracting;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        player = FindObjectOfType<Player>();
    }

    public void Interact()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            dco.ResetCarryOver();
            door.GoToLevel();
            isInteracting = false;
        }
    }

    public void Uninteract()
    {

    }
}
