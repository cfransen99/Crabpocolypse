using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Item/ Consumable")]
public class Consumables : Item
{
    public float healthIncrease;
    public float driveIncrease;
    
    public float speedIncrease;
    public float attackIncrease;
    public float defenseIncrease;

    //Uses the item
    public override void Use()
    {
        GameObject player = Inventory.instance.player;
        PlayerStats s = player.GetComponent<PlayerStats>();

        float maxHealth = s.playerMaxHealth;
        float maxDrive = s.playerMaxDrive;

        //Increases the players health and drive if it is less than the maximum
        if (s.playerHealth < maxHealth || s.playerDrive < maxDrive)
        {
            s.playerHealth += healthIncrease;
            if(!s.overdriveLocked)
            {
                s.playerDrive += (driveIncrease / maxDrive) * 100;
            }
            amount--;
        }
    }
}