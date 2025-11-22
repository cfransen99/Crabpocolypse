using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemInventory : MonoBehaviour
{
    public List<string> keyItemInv;
    private DataCarryOver dco;
    private SoundManager sm;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.GetComponent<SoundManager>();

        keyItemInv = dco.keyItemInventory;
    }

    public void AddItem(string name)
    {
        sm.sfxPlayer.PlayOneShot(sm.soundItem);
        keyItemInv.Add(name);
        dco.keyItemInventory.Add(name);
    }

    public bool CheckInv(string name)
    {
        return keyItemInv.Contains(name);
    }

    public void RemoveItem(string name)
    {
        keyItemInv.Remove(name);
        dco.keyItemInventory.Remove(name);
    }
}
