using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonLeverBase : MonoBehaviour, IInteractable
{
    private SaveHandler saveHandler;
    protected DataCarryOver dco;

    public bool isActive;

    [SerializeField] public int ID;

    private IEnumerator Start()
    {
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        yield return new WaitForSeconds(.1f);
        if (saveHandler.GetDidLoadSave())
        {
            isActive = saveHandler.isActive[ID];
        }
        else
        {
            isActive = dco.isActive[ID];
        }

        if(isActive)
        {
            Interact();
        }
    }

    public abstract void Interact();

    public void Uninteract()
    {
        
    }
}
