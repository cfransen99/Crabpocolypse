using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextLevelTrigger : MonoBehaviour
{
    private LevelLoader levelLoader;
    private DataCarryOver dco;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dco = FindObjectOfType<DataCarryOver>();
            dco.ResetCarryOver();

            levelLoader = FindObjectOfType<LevelLoader>();
            levelLoader.LoadNewLevel("Crapsolute0Fight");
        }
    }
}
