using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOffMapTrigger : MonoBehaviour
{
    SaveHandler saveHandler;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            saveHandler = FindObjectOfType<SaveHandler>();
            saveHandler.LoadSaveScene();
        }
    }
}
