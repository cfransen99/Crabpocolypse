using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //Singleton
    #region
    public static Inventory instance;
    private SaveHandler saveHandler;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;

        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
    }
    #endregion

    public bool isEmpty = true;
    public List<Item> items = new List<Item>();
    public GameObject player;
    public ItemList itemList;

    private DataCarryOver dco;

    private void Start()
    {
        player = gameObject;

        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        if (!saveHandler.GetDidLoadSave())
        {
            PopulateFromDCO();
        }
    }

    //Add items to the inventory
    public void AddItem(Item item)
    {
        //If the item is already in the inventory, then increase the amount
        foreach (Item i in items)
        {
            if (item.name == i.name)
            {
                item.amount++;
                return;
            }
        }
        //Adds a new item to the inventory
        item.amount = 1;
        items.Add(item);
        item.itemIndex = items.Count;

        isEmpty = false;
    }

    //Removes an item from the inventory
    public void RemoveItem(Item item)
    {
        if (item.amount >= 1)
        {
            item.amount--;
        }
    }

    //Returns the inventorys size
    public int InventorySize()
    {
        return items.Count;
    }

    public void PopulateFromDCO()
    {
        if (dco.invMeatS > 0)
        {
            for (int i = 0; i < dco.invMeatS; i++)
            {
                AddItem(itemList.MeatS);
            }
        }
        if (dco.invMeatM > 0)
        {
            for (int i = 0; i < dco.invMeatM; i++)
            {
                AddItem(itemList.MeatM);
            }
        }
        if (dco.invMeatL > 0)
        {
            for (int i = 0; i < dco.invMeatL; i++)
            {
                AddItem(itemList.MeatL);
            }
        }
        if (dco.invMeatXL > 0)
        {
            for (int i = 0; i < dco.invMeatXL; i++)
            {
                AddItem(itemList.MeatXL);
            }
        }
        if (dco.invPow > 0)
        {
            for (int i = 0; i < dco.invPow; i++)
            {
                AddItem(itemList.Pow);
            }
        }
        if (dco.invDef > 0)
        {
            for (int i = 0; i < dco.invDef; i++)
            {
                AddItem(itemList.Def);
            }
        }
        if (dco.invSpeed > 0)
        {
            for (int i = 0; i < dco.invSpeed; i++)
            {
                AddItem(itemList.Speed);
            }
        }
        if (dco.invSpeed2 > 0)
        {
            for (int i = 0; i < dco.invSpeed2; i++)
            {
                AddItem(itemList.Speed2);
            }
        }
        if (dco.invMagic > 0)
        {
            for (int i = 0; i < dco.invMagic; i++)
            {
                AddItem(itemList.Magic);
            }
        }
        if (dco.invCheat > 0)
        {
            for (int i = 0; i < dco.invCheat; i++)
            {
                AddItem(itemList.Cheat);
            }
        }
    }
    public void PopulateFromFile()
    {
        if (saveHandler.invMeatS > 0)
        {
            for (int i = 0; i < saveHandler.invMeatS; i++)
            {
                AddItem(itemList.MeatS);
            }
        }
        if (saveHandler.invMeatM > 0)
        {
            for (int i = 0; i < saveHandler.invMeatM; i++)
            {
                AddItem(itemList.MeatM);
            }
        }
        if (saveHandler.invMeatL > 0)
        {
            for (int i = 0; i < saveHandler.invMeatL; i++)
            {
                AddItem(itemList.MeatL);
            }
        }
        if (saveHandler.invMeatXL > 0)
        {
            for (int i = 0; i < saveHandler.invMeatXL; i++)
            {
                AddItem(itemList.MeatXL);
            }
        }
        if (saveHandler.invPow > 0)
        {
            for (int i = 0; i < saveHandler.invPow; i++)
            {
                AddItem(itemList.Pow);
            }
        }
        if (saveHandler.invDef > 0)
        {
            for (int i = 0; i < saveHandler.invDef; i++)
            {
                AddItem(itemList.Def);
            }
        }
        if (saveHandler.invSpeed > 0)
        {
            for (int i = 0; i < saveHandler.invSpeed; i++)
            {
                AddItem(itemList.Speed);
            }
        }
        if (saveHandler.invSpeed2 > 0)
        {
            for (int i = 0; i < saveHandler.invSpeed2; i++)
            {
                AddItem(itemList.Speed2);
            }
        }
        if (saveHandler.invMagic > 0)
        { 
            for (int i = 0; i < saveHandler.invMagic; i++)
            {
                AddItem(itemList.Magic);
            }
        }
        if (saveHandler.invCheat > 0)
        {
            for (int i = 0; i < saveHandler.invCheat; i++)
            {
                AddItem(itemList.Cheat);
            }
        }
    }
}
