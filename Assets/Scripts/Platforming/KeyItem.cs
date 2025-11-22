using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private SaveHandler saveHandler;
    private DataCarryOver dco;

    public bool isGotten;
    public int ID;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        if (saveHandler.GetDidLoadSave())
        {
            isGotten = saveHandler.isGotten[ID];
        }
        else
        {
            isGotten = dco.isGotten[ID];
        }

        if (isGotten)
        {
            GottenItemOnLoad();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GottenItem(other);
        }
    }

    public void GottenItemOnLoad()
    {
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void GottenItem(Collider collider)
    {
        isGotten = true;
        collider.GetComponent<KeyItemInventory>().AddItem(gameObject.name);
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        dco.isGotten[ID] = isGotten;
    }
}
