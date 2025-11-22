using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Item item;
    public int itemID = -1;
    public bool isPickedUp;
    private bool canInteract = false;

    private SaveHandler saveHandler;
    private DataCarryOver dco;
    private SoundManager sm;

    private void Awake()
    {
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.GetComponent<SoundManager>();

        if (!saveHandler.GetDidLoadSave())
        {
            for(int i = 0; i < dco.itemPickedUp.Length; i++)
            {
                if (i == itemID)
                {
                    isPickedUp = dco.itemPickedUp[i];
                }
            }
            if (isPickedUp)
            {
                //Destroy(gameObject);
                GetComponent<MeshRenderer>().enabled = false;
                canInteract = true;
            }
        }
    }

    private void OnEnable()
    {
        if (saveHandler.GetDidLoadSave())
        {
            for (int i = 0; i < saveHandler.itemPickedUp.Length; i++)
            {
                if (i == itemID)
                {
                    isPickedUp = saveHandler.itemPickedUp[i];
                }
            }
            if (isPickedUp)
            {
                //Destroy(gameObject);
                GetComponent<MeshRenderer>().enabled = false;
                canInteract = true;
            }
        }

    }

    //Picks up an item
    public void Interact()
    {
        if(!canInteract)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundItem);
            isPickedUp = true;
            Inventory.instance.AddItem(item);
            GetComponent<MeshRenderer>().enabled = false;
            canInteract = true;
        }
    }

    public void Uninteract()
    {

    }
}
