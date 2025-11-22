using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordConsole : MonoBehaviour, IInteractable
{
    public PasswordPanel passwordPanel;

    private UIController uiController;
    private PlayerMovement playerMove;
    private KeyItemInventory inventory;

    private bool isActive;

    public bool IsActive { get => isActive; set => isActive = value; }

    private void Start()
    {
        uiController = FindObjectOfType<UIController>();

        playerMove = FindObjectOfType<PlayerMovement>();
        inventory = playerMove.GetComponent<KeyItemInventory>();
    }

    public void Interact()
    {
        if(!isActive && Input.GetKeyDown(KeyCode.E) && playerMove.canMove)
        {
            passwordPanel.gameObject.SetActive(true);
            playerMove.DisableMovement();
            isActive = true;

            foreach (GameObject i in uiController.defaultPanels)
            {
                i.SetActive(false);
            }

            if (inventory.CheckInv("C"))
            {
                inventory.RemoveItem("C");
                passwordPanel.HasC();
            }
            if(inventory.CheckInv("R"))
            {
                inventory.RemoveItem("R");
                passwordPanel.HasR();
            }
            if(inventory.CheckInv("A"))
            {
                inventory.RemoveItem("A");
                passwordPanel.HasA();
            }
            if(inventory.CheckInv("B"))
            {
                inventory.RemoveItem("B");
                passwordPanel.HasB();
            }
        }
        else if(isActive && Input.GetKeyDown(KeyCode.E))
        {
            passwordPanel.gameObject.SetActive(false);
            playerMove.EnableMovement();
            isActive = false;
            uiController.ResetPanels();
        }
    }

    public void Uninteract()
    {

    }

}
