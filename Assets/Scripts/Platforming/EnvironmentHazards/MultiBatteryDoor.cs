using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBatteryDoor : GarageDoor
{
    private bool[] areDestroyed = new bool[2];


    public void isDestroyed(int num)
    {
        areDestroyed[num] = true;

        CheckIfOpen();
    }

    private void CheckIfOpen()
    {
        foreach(bool battery in areDestroyed)
        {
            Debug.Log(battery.ToString());
            if(battery == false)
            { 
                return;
            }
        }

        GetComponent<Animator>().SetTrigger("Open");
    }
}
