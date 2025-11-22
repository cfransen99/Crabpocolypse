using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabSpawnButtons : MonoBehaviour
{
    public GameObject Crab, Crabbit, Robo, Back, Witch, Mimic;

    //This whole script is VERY temporary; just for demonstration purposes
    public void SpawnCrab()
    {
        if (!Crab.activeSelf)
            Crab.SetActive(true);
        else
            Crab.SetActive(false);
    }

    public void SpawnCrabbit()
    {
        if (!Crabbit.activeSelf)
            Crabbit.SetActive(true);
        else
            Crabbit.SetActive(false);
    }

    public void SpawnRobo()
    {
        if (!Robo.activeSelf)
            Robo.SetActive(true);
        else
            Robo.SetActive(false);
    }

    public void SpawnBack()
    {
        if (!Back.activeSelf)
            Back.SetActive(true);
        else
            Back.SetActive(false);
    }

    public void SpawnWitch()
    {
        if (!Witch.activeSelf)
            Witch.SetActive(true);
        else
            Witch.SetActive(false);
    }

    public void SpawnMimic()
    {
        if (!Mimic.activeSelf)
            Mimic.SetActive(true);
        else
            Mimic.SetActive(false);
    }
}
