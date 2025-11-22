using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] doors;

    public void DestroyDoors()
    {
        foreach(GameObject door in doors)
        {
            Destroy(door);
        }
    }
}
