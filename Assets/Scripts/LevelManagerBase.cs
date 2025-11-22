using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelManagerBase : MonoBehaviour
{
    [SerializeField] protected int enemiesInLevel;


    public int GetEnemiesInLevel()
    {
        return enemiesInLevel;
    }
}
