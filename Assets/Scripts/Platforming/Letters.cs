using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letters : MonoBehaviour, IInteractable
{
    private KeyItemInventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<KeyItemInventory>();

        Debug.Log(name);
        name = name.Replace("(Clone)", "");
    }

    public void Interact()
    {
        inventory.AddItem(name);
        Destroy(gameObject);
    }

    public void Uninteract()
    {
        
    }
}
