using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBase : MonoBehaviour
{
    public int ID;
    public bool isTriggered;

    protected abstract void OnTriggerEnter(Collider other);
}
