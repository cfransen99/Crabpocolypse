using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarryOver : MonoBehaviour
{
    public string enemyType, sceneName;
    public float playerHealth, playerDrive, playerPosX, playerPosY, playerPosZ;
    public bool inDrive, facingRight, cheating, learnDash, learnAir, learnJump, learnBarrier, isAdvantage, isAmbushed, isNewLevel, overdriveLocked, returningBoss, returningAid;
    public bool beatZero, beatClawdius, beatMoustache, beatShovel, beatLincrab;
    public int invMeatS, invMeatM, invMeatL, invMeatXL, invPow, invDef, invSpeed, invSpeed2, invMagic, invCheat, playerExp, playerPoints, playerLevel, pointsPow, pointsDef, pointsSpeed, playerMoney, playerPow, playerDef, playerSpeed, XPCap, encounterID, bossHP, bossAidHP, bossMoney;
    public bool[] tutorialsUnlocked = new bool[8];
    public List<string> keyItemInventory = new List<string>();

    public bool[] enemyDead = new bool[20];
    public bool[] itemPickedUp = new bool[20];
    public bool[] isActive = new bool[20];
    public bool[] isBroken = new bool[20];
    public bool[] isGotten = new bool[20];
    public bool[] isTriggered = new bool[20];

    //Enables persistence between scenes
    void Awake()
    {
        //Finds all instances of this object
        GameObject[] objs = GameObject.FindGameObjectsWithTag("CarryOver");
        //Deletes any instances beyond the first one; re-entering the level will try to load another instance
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        //Allows object to persist between scenes
        DontDestroyOnLoad(gameObject);
    }

    public void ResetCarryOver()
    {
        for (int i = 0; i < enemyDead.Length; i++)
        {
            enemyDead[i] = false;
        }
        for (int i = 0; i < itemPickedUp.Length; i++)
        {
            itemPickedUp[i] = false;
        }
        for (int i = 0; i < isActive.Length; i++)
        {
            isActive[i] = false;
        }
        for (int i = 0; i < isBroken.Length; i++)
        {
            isBroken[i] = false;
        }
        for (int i = 0; i < isGotten.Length; i++)
        {
            isGotten[i] = false;
        }
        for (int i = 0; i < isTriggered.Length; i++)
        {
            isTriggered[i] = false;
        }
    }
}
