using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "Item Name";
    public Sprite icon = null;
    public int amount = 0;
    public int cost = 0;
    public int itemIndex = 0;
    [TextArea(3, 10)]
    public string itemDescription;

    //Uses the item
    public virtual void Use()
    {

    }
}
