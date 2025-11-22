using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuUI : MonoBehaviour
{
    private PlayerStats player;
    private Inventory inventory;

    private Item itemSelected;

    [SerializeField] private Item[] items;

    [SerializeField] private GameObject notEnoughGold, purchased;
    [SerializeField] private GameObject confirmSelection;
    [SerializeField] private Text itemDescription;

    [SerializeField] private GameObject pausePanel;

    private void Awake()
    {
        player = FindObjectOfType<PlayerStats>();
        inventory = player.GetComponent<Inventory>();
    }

    public void SandwichButton()
    {
        notEnoughGold.SetActive(false);
        purchased.SetActive(false);
        itemSelected = items[0];
        itemDescription.text = items[0].itemDescription;
        confirmSelection.SetActive(true);
    }

    public void BrokenClawButton()
    {
        notEnoughGold.SetActive(false);
        purchased.SetActive(false);
        itemSelected = items[1];
        itemDescription.text = itemSelected.itemDescription;
        confirmSelection.SetActive(true);
    }
    
    public void CrabRollButton()
    {
        notEnoughGold.SetActive(false);
        purchased.SetActive(false);
        itemSelected = items[2];
        itemDescription.text = itemSelected.itemDescription;
        confirmSelection.SetActive(true);
    }
    
    public void CrabbitEarsButton()
    {
        notEnoughGold.SetActive(false);
        purchased.SetActive(false);
        itemSelected = items[3];
        itemDescription.text = itemSelected.itemDescription;
        confirmSelection.SetActive(true);
    }
    
    public void CrabMeatLButton()
    {
        notEnoughGold.SetActive(false);
        purchased.SetActive(false);
        itemSelected = items[4];
        itemDescription.text = itemSelected.itemDescription;
        confirmSelection.SetActive(true);
    }
    
    public void CrabMeatMButton()
    {
        notEnoughGold.SetActive(false);
        purchased.SetActive(false);
        itemSelected = items[5];
        itemDescription.text = itemSelected.itemDescription;
        confirmSelection.SetActive(true);
    }
    
    public void CrabMeatSButton()
    {
        notEnoughGold.SetActive(false);
        purchased.SetActive(false);
        itemSelected = items[6];
        itemDescription.text = itemSelected.itemDescription;
        confirmSelection.SetActive(true);
    }
    
    public void RoboCrabMeatButton()
    {
        notEnoughGold.SetActive(false);
        purchased.SetActive(false);
        itemSelected = items[7];
        itemDescription.text = itemSelected.itemDescription;
        confirmSelection.SetActive(true);
    }

    public void Back()
    {
        pausePanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ConfirmButton()
    {
        if (player.gold >= itemSelected.cost)
        {
            player.Buy(itemSelected.cost);
            inventory.AddItem(itemSelected);
            purchased.SetActive(true);
        }
        else
        {
            notEnoughGold.SetActive(true);
        }
        confirmSelection.SetActive(false);
    }
}
