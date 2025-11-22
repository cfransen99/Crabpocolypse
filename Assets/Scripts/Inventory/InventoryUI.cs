using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    Item item;
    public int currentIndex = 0;

    public Text quantity;
    public new Text name;
    public Image icon;

    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        item = inventory.items[currentIndex];

        
        //If the player presses Up arrow key the inventory scrolls up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ScrollNext();
        }
        //If the player presses Down arrow key the inventory scrolls down
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ScrollPrevious();
        }

        //Shows the inventory if there are items in it
        if (inventory.items.Count >= 0)
        {
            InventoryMenu();
        }

        //Pressing enter uses the item if it can be used
        if (item.amount > 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                item.Use();
            }
        }
    }

    //Goes to next item in the inventory
    public void ScrollNext()
    {
        if (inventory.items.Count >= 0)
        {
            if (currentIndex < inventory.items.Count)
            {
                currentIndex++;
            }
            if (currentIndex == inventory.items.Count)
            {
                currentIndex = 0;
            }
        }
    }

    //Goes to previous item in the inventory
    public void ScrollPrevious()
    {
        if (inventory.items.Count >= 0)
        {
            if (currentIndex >= 0)
            {
                currentIndex--;
            }
            if (currentIndex < 0)
            {
                currentIndex = inventory.items.Count - 1;
            }
        }
    }

    //Displays the inventory item
    public void InventoryMenu()
    {
        icon.sprite = inventory.items[currentIndex].icon;
        quantity.text = inventory.items[currentIndex].amount.ToString();
        name.text = inventory.items[currentIndex].name;
    }

    //returns the selected item
    public Item GetItem()
    {
        return item;
    }
}
