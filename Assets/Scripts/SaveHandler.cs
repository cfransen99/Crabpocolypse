using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveHandler : MonoBehaviour
{
    public GameObject player;
    private DataCarryOver dco;

    private float playerHealth, playerDrive, playerPosX, playerPosY, playerPosZ;
    private bool learnDash, learnAir, learnJump, learnBarrier, overdriveLocked, didLoadSave;
    private int sceneSaved, playerExp, playerPoints, playerLevel, pointsPow, pointsDef, pointsSpeed, playerMoney, playerPow, playerDef, playerSpeed;
    public int invMeatS, invMeatM, invMeatL, invMeatXL, invPow, invDef, invSpeed, invSpeed2, invMagic, invCheat, bossHP;
    public List<string> keyItemInventory;

    public bool[] enemyDead;
    public bool[] isActive;
    public bool[] itemPickedUp;
    public bool[] isBroken;
    public bool[] isGotten;
    public bool[] isTriggered;

    private PlayerStats stats;
    private Transform playerTransform;
    private Player p;
    private PlayerLevel level;
    private SkillTree skillTree;
    private Inventory inv;
    private KeyItemInventory playersKeyItems;
    private Crabsolute0FightManager fightManager;

    private LevelLoader levelLoader;
    
    private EnemyBase[] enemiesInLevel;

    private ButtonLeverBase[] buttonsAndLevers;

    private ItemPickup[] itemPickups;

    private BreakableBarricade[] breakables;

    private KeyItem[] keyItems;

    private DialogueTrigger[] triggers;

    private UIController uIController;

    private const string SAVE_SEPERATOR = " #STRING_SEPERATOR# ";

    void Awake()
    {
        //Finds all instances of this object
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SaveHandler");
        //Deletes any instances beyond the first one; re-entering the level will try to load another instance
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        //Allows object to persist between scenes
        DontDestroyOnLoad(gameObject);

    }

    public void SaveAtPoint()
    {
        SavePlayerInfo();
        SaveSceneInfo();
    }

    public void SaveNewScene()
    {
        Debug.Log("Saving");
        SavePlayerInfo();
        SaveSceneInfo();
    }

    public void SavePlayerInfo()
    {
        stats = player.GetComponent<PlayerStats>();
        p = player.GetComponent<Player>();
        level = player.GetComponent<PlayerLevel>();
        uIController = FindObjectOfType<UIController>();
        skillTree = uIController.transform.Find("SkillTreePanel").GetComponent<SkillTree>();
        inv = player.GetComponent<Inventory>();
        playerTransform = player.transform;
        playersKeyItems = player.GetComponent<KeyItemInventory>();

        itemPickups = FindObjectsOfType<ItemPickup>();

        #region Setting Save Info
        playerHealth = stats.playerHealth;
        playerDrive = stats.playerDrive;
        playerPow = stats.playerAttack;
        playerDef = stats.initPlayerDefense;
        playerSpeed = stats.playerSpeed;
        playerMoney = stats.gold;

        playerPosX = playerTransform.position.x;
        playerPosY = playerTransform.position.y;
        playerPosZ = playerTransform.position.z;

        learnAir = p.learnAir;
        learnDash = p.learnDash;
        learnJump = p.learnJump;
        learnBarrier = p.learnBarrier;

        playerLevel = level.level;
        playerExp = level.experience;
        playerPoints = level.skillPoints;

        pointsPow = skillTree.destroyerLevel;
        pointsDef = skillTree.garrisonLevel;
        pointsSpeed = skillTree.slipstreamLevel;

        sceneSaved = SceneManager.GetActiveScene().buildIndex;

        if (inv.isEmpty)
        {
            invMeatS = 0;
            invMeatM = 0;
            invMeatL = 0;
            invMeatXL = 0;
            invPow = 0;
            invDef = 0;
            invSpeed = 0;
            invSpeed2 = 0;
            invMagic = 0;
            invCheat = 0;
        }
        else
        {
            foreach (Item item in inv.items)
            {
                if (item.name == "Crab Meat S")
                {
                    invMeatS = item.amount;
                }
                else if (item.name == "Crab Meat M")
                {
                    invMeatM = item.amount;
                }
                else if (item.name == "Crab Meat L")
                {
                    invMeatL = item.amount;
                }
                else if (item.name == "Sandwich")
                {
                    invMeatXL = item.amount;
                }
                else if (item.name == "Broken Claw")
                {
                    invPow = item.amount;
                }
                else if (item.name == "Robo Crab Meat")
                {
                    invDef = item.amount;
                }
                else if (item.name == "Crabbit Ears")
                {
                    invSpeed = item.amount;
                }
                else if (item.name == "Crab Roll")
                {
                    invSpeed2 = item.amount;
                }
                else if (item.name == "Witch's Hat")
                {
                    invMagic = item.amount;
                }
                else if (item.name == "Crab Puff Supreme")
                {
                    invCheat = item.amount;
                }
            }
        }

        keyItemInventory = new List<string>();
        for(int i = 0; i < playersKeyItems.keyItemInv.Count; i++)
        {
            keyItemInventory.Add(playersKeyItems.keyItemInv[i]);
        }

        overdriveLocked = stats.overdriveLocked;

        #endregion 

        string[] contents = new string[]
        {
            "" + playerHealth,
            "" + playerDrive,
            "" + playerPow,
            "" + playerDef,
            "" + playerSpeed,
            "" + playerMoney,

            "" + playerPosX,
            "" + playerPosY,
            "" + playerPosZ,

            "" + learnAir,
            "" + learnDash,
            "" + learnJump,
            "" + learnBarrier,

            "" + playerLevel,
            "" + playerExp,
            "" + playerPoints,

            "" + pointsPow,
            "" + pointsDef,
            "" + pointsSpeed,

            "" + sceneSaved,

            "" + invMeatS,
            "" + invMeatM,
            "" + invMeatL,
            "" + invMeatXL,
            "" + invPow,
            "" + invDef,
            "" + invSpeed,
            "" + invSpeed2,
            "" + invMagic,
            "" + invCheat,

            "" + overdriveLocked      
        };



        string saveString = string.Join(SAVE_SEPERATOR, contents) + SAVE_SEPERATOR + string.Join(SAVE_SEPERATOR, keyItemInventory);
        File.WriteAllText(Application.dataPath + "/Saves/playerSave.txt", saveString);
    }

    public void SaveSceneInfo()
    {
        #region Save Enemy Info
        enemiesInLevel = FindObjectsOfType<EnemyBase>();

        enemyDead = new bool[enemiesInLevel.Length];

        for(int i = 0; i < enemyDead.Length; i++)
        {
            enemyDead[i] = true;
        }

        foreach (EnemyBase enemy in enemiesInLevel)
        {
            enemyDead[enemy.enemyID] = enemy.isDead;
        }

        string[] enemyDeadString = new string[enemyDead.Length];

        for (int i = 0; i < enemyDead.Length; i++)
        {
            enemyDeadString[i] = enemyDead[i].ToString();
        }
        #endregion

        #region Save Interactables Info
        buttonsAndLevers = FindObjectsOfType<ButtonLeverBase>();

        isActive = new bool[buttonsAndLevers.Length];

        foreach(ButtonLeverBase buttons in buttonsAndLevers)
        {
            isActive[buttons.ID] = buttons.isActive;
        }

        string[] isActiveString = new string[isActive.Length];

        for (int i = 0; i < isActive.Length; i++)
        {
            isActiveString[i] = isActive[i].ToString();
        }
        #endregion

        #region Save Breakables Info
        breakables = FindObjectsOfType<BreakableBarricade>();

        isBroken = new bool[breakables.Length];

        foreach (BreakableBarricade breakable in breakables)
        {
            isBroken[breakable.ID] = breakable.isBroken;
        }

        string[] isBrokenString = new string[isBroken.Length];

        for (int i = 0; i < isBroken.Length; i++)
        {
            isBrokenString[i] = isBroken[i].ToString();
        }
        #endregion

        #region Save Key Item Info
        keyItems = FindObjectsOfType<KeyItem>();

        isGotten = new bool[keyItems.Length];

        foreach (KeyItem item in keyItems)
        {
            isGotten[item.ID] = item.isGotten;
        }

        string[] isGottenString = new string[isGotten.Length];

        for (int i = 0; i < isGotten.Length; i++)
        {
            isGottenString[i] = isGotten[i].ToString();
        }
        #endregion

        #region Save Trigger Info
        triggers = FindObjectsOfType<DialogueTrigger>();

        isTriggered = new bool[triggers.Length];

        foreach (DialogueTrigger trigger in triggers)
        {
            isTriggered[trigger.ID] = trigger.isTriggered;
        }

        string[] isTriggeredString = new string[isTriggered.Length];

        for (int i = 0; i < isTriggered.Length; i++)
        {
            isTriggeredString[i] = isTriggered[i].ToString();
        }
        #endregion

        #region Save Item Info
        itemPickups = FindObjectsOfType<ItemPickup>();
        
        itemPickedUp = new bool[itemPickups.Length];

        foreach (ItemPickup item in itemPickups)
        {
            itemPickedUp[item.itemID] = item.isPickedUp;
        }

        string[] itemPickupString = new string[itemPickedUp.Length];

        foreach (ItemPickup item in itemPickups)
        {
            itemPickupString[item.itemID] = itemPickedUp[item.itemID].ToString();
        }
        #endregion

        //#region Save Boss Info
        //dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        //bossHP = dco.bossHP;

        //string bossHPString = bossHP.ToString();
        //#endregion

        #region Save Tutorial/Overdrive Lock Info
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        bool[] TutorialLocks = new bool[dco.tutorialsUnlocked.Length + 1];

        TutorialLocks[0] = dco.overdriveLocked;
        for (int i = 0; i < dco.tutorialsUnlocked.Length; i++)
        {
            TutorialLocks[i + 1] = dco.tutorialsUnlocked[i];
        }
        #endregion

        string saveString = "";

        if(enemyDead.Length > 0)
        {
            if(saveString != "")
            {
                saveString = saveString + SAVE_SEPERATOR + string.Join(SAVE_SEPERATOR, enemyDeadString);
            }
            else
            {
                saveString = string.Join(SAVE_SEPERATOR, enemyDeadString);
            }
        }

        if (isActive.Length > 0)
        {
            if (saveString != "")
            {
                saveString = saveString + SAVE_SEPERATOR + string.Join(SAVE_SEPERATOR, isActiveString);
            }
            else
            {
                saveString = string.Join(SAVE_SEPERATOR, isActiveString);
            }
        }

        if(isBroken.Length > 0)
        {
            if (saveString != "")
            {
                saveString = saveString + SAVE_SEPERATOR + string.Join(SAVE_SEPERATOR, isBrokenString);
            }
            else
            {
                saveString = string.Join(SAVE_SEPERATOR, isBrokenString);
            }
        }

        if(isGotten.Length > 0)
        {
            if (saveString != "")
            {
                saveString = saveString + SAVE_SEPERATOR + string.Join(SAVE_SEPERATOR, isGottenString);
            }
            else
            {
                saveString = string.Join(SAVE_SEPERATOR, isGottenString);
            }
        }

        if(isTriggered.Length > 0)
        {
            if (saveString != "")
            {
                saveString = saveString + SAVE_SEPERATOR + string.Join(SAVE_SEPERATOR, isTriggeredString);
            }
            else
            {
                saveString = string.Join(SAVE_SEPERATOR, isTriggeredString);
            }
        }

        if (itemPickedUp.Length > 0)
        {
            if (saveString != "")
            {
                saveString = saveString + SAVE_SEPERATOR + string.Join(SAVE_SEPERATOR, itemPickupString);
            }
            else
            {
                saveString = string.Join(SAVE_SEPERATOR, itemPickupString);
            }
        }

        //saveString = saveString + SAVE_SEPERATOR + string.Join(SAVE_SEPERATOR, bossHPString);

        for (int i = 0; i < TutorialLocks.Length; i++)
        {
            saveString += SAVE_SEPERATOR + TutorialLocks[i];
        }

        File.WriteAllText(Application.dataPath + "/Saves/level" + (SceneManager.GetActiveScene().buildIndex) +"Save.txt", saveString);
    }

    public void Load()
    {
        LoadPlayerInfo();
        Debug.Log("Loading Scene Info");
        LoadSceneInfo(sceneSaved);
    }

    public void LoadPlayerInfo()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            stats = player.GetComponent<PlayerStats>();
            p = player.GetComponent<Player>();
            level = player.GetComponent<PlayerLevel>();
            inv = player.GetComponent<Inventory>();
            uIController = FindObjectOfType<UIController>();
            skillTree = uIController.transform.Find("SkillTreePanel").GetComponent<SkillTree>();
            playerTransform = player.transform;
            playersKeyItems = player.GetComponent<KeyItemInventory>();

            string saveString = File.ReadAllText(Application.dataPath + "/Saves/playerSave.txt");

            string[] contents = saveString.Split(new[] { SAVE_SEPERATOR }, System.StringSplitOptions.None);

            #region Loading Save Info from File
            playerHealth = float.Parse(contents[0]);
            playerDrive = float.Parse(contents[1]);
            playerPow = int.Parse(contents[2]);
            playerDef = int.Parse(contents[3]);
            playerSpeed = int.Parse(contents[4]);
            playerMoney = int.Parse(contents[5]);

            playerPosX = float.Parse(contents[6]);
            playerPosY = float.Parse(contents[7]);
            playerPosZ = float.Parse(contents[8]);

            learnAir = bool.Parse(contents[9]);
            learnDash = bool.Parse(contents[10]);
            learnJump = bool.Parse(contents[11]);
            learnBarrier = bool.Parse(contents[12]);

            playerLevel = int.Parse(contents[13]);
            playerExp = int.Parse(contents[14]);
            playerPoints = int.Parse(contents[15]);

            pointsPow = int.Parse(contents[16]);
            pointsDef = int.Parse(contents[17]);
            pointsSpeed = int.Parse(contents[18]);

            invMeatS = int.Parse(contents[20]);
            invMeatM = int.Parse(contents[21]);
            invMeatL = int.Parse(contents[22]);
            invMeatXL = int.Parse(contents[23]);
            invPow = int.Parse(contents[24]);
            invDef = int.Parse(contents[25]);
            invSpeed = int.Parse(contents[26]);
            invSpeed2 = int.Parse(contents[27]);
            invMagic = int.Parse(contents[28]);
            invCheat = int.Parse(contents[29]);
            
            overdriveLocked = bool.Parse(contents[30]);


            for (int i = 31; i < playersKeyItems.keyItemInv.Count; i++)
            {
                playersKeyItems.keyItemInv.Add(contents[i]);
            }
            #endregion

            #region Loading Save Info to Player
            stats.playerHealth = playerHealth;
            stats.playerDrive = playerDrive;
            stats.playerAttack = playerPow;
            stats.playerDefense = playerDef;
            stats.playerSpeed = playerSpeed;

            playerTransform.position = new Vector3(playerPosX, playerPosY, playerPosZ);

            p.learnAir = learnAir;
            p.learnDash = learnDash;
            p.learnJump = learnJump;
            p.learnBarrier = learnBarrier;

            level.level = playerLevel;
            level.experience = playerExp;
            level.skillPoints = playerPoints;

            skillTree.destroyerLevel = pointsPow;
            skillTree.garrisonLevel = pointsDef;
            skillTree.slipstreamLevel = pointsSpeed;

            skillTree.destroyerText.text = pointsPow.ToString();
            skillTree.garrisonText.text = pointsDef.ToString();
            skillTree.slipstreamText.text = pointsSpeed.ToString();

            Inventory.instance.PopulateFromFile();

            stats.overdriveLocked = overdriveLocked;

            stats.EarnGold(playerMoney);
            #endregion

            Time.timeScale = 1;

            uIController.ResetPanels();

            Debug.Log("Loaded Player Info");
        }
    }

    public void LoadSceneInfo(int sceneIndex)
    {
        enemiesInLevel = FindObjectsOfType<EnemyBase>();
        buttonsAndLevers = FindObjectsOfType<ButtonLeverBase>();
        breakables = FindObjectsOfType<BreakableBarricade>();
        keyItems = FindObjectsOfType<KeyItem>();
        triggers = FindObjectsOfType<DialogueTrigger>();
        itemPickups = FindObjectsOfType<ItemPickup>();

        enemyDead = new bool[enemiesInLevel.Length];
        isActive = new bool[buttonsAndLevers.Length];
        isBroken = new bool[breakables.Length];
        isGotten = new bool[keyItems.Length];
        isTriggered = new bool[triggers.Length];
        itemPickedUp = new bool[itemPickups.Length];

        string saveString = File.ReadAllText(Application.dataPath + "/Saves/level" + (sceneIndex) + "Save.txt");

        string[] contents = saveString.Split(new[] { SAVE_SEPERATOR }, System.StringSplitOptions.None);
        Debug.Log(contents.Length + " total");
        Debug.Log(enemyDead.Length + " enemies + " + isActive.Length + " buttons + " + isBroken.Length + " breakables + " + isGotten.Length + " key items" + isTriggered.Length + " triggers + " + itemPickedUp.Length + " items");

        #region Load Level Data to Level
        for (int i = 0; i < enemiesInLevel.Length; i++)
        {
            enemyDead[i] = bool.Parse(contents[i]);
            Debug.Log("Loaded " + contents[i] + " for an enemy");
        }

        for (int i = enemiesInLevel.Length; i < buttonsAndLevers.Length + enemiesInLevel.Length; i++)
        {
            isActive[i - enemiesInLevel.Length] = bool.Parse(contents[i]);
            Debug.Log("Loaded " + contents[i] + " for a button");
        }

        for (int i = buttonsAndLevers.Length + enemiesInLevel.Length; 
            i < breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length; i++)
        {
            isBroken[i - buttonsAndLevers.Length - enemiesInLevel.Length] = bool.Parse(contents[i]);
            Debug.Log("Loaded " + contents[i] + " for a breakable");
        }

        for (int i = breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length; 
            i < keyItems.Length + breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length; i++)
        {
            isGotten[i - breakables.Length - buttonsAndLevers.Length - enemiesInLevel.Length] = bool.Parse(contents[i]);
            Debug.Log("Loaded " + contents[i] + " for a key item");
        }

        for (int i = keyItems.Length + breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length;
            i < triggers.Length + keyItems.Length + breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length; i++)
        {
            isTriggered[i - keyItems.Length - breakables.Length - buttonsAndLevers.Length - enemiesInLevel.Length] = bool.Parse(contents[i]);
            Debug.Log("Loaded " + contents[i] + " for a trigger");
        }
        
        for (int i = triggers.Length + keyItems.Length + breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length;
            i < itemPickups.Length + triggers.Length + keyItems.Length + breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length; i++)
        {
            itemPickedUp[i - triggers.Length - keyItems.Length - breakables.Length - buttonsAndLevers.Length - enemiesInLevel.Length] = bool.Parse(contents[i]);
            Debug.Log("Loaded " + contents[i] + " for an item");
        }

        //bossHP = int.Parse(contents[itemPickups.Length + triggers.Length + keyItems.Length + breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length]);

        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        dco.overdriveLocked = bool.Parse(contents[itemPickups.Length + triggers.Length + keyItems.Length + breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length]);
        for (int i = 0; i < dco.tutorialsUnlocked.Length; i++)
        {
            dco.tutorialsUnlocked[i] = bool.Parse(contents[1 + i + itemPickups.Length + triggers.Length + keyItems.Length + breakables.Length + buttonsAndLevers.Length + enemiesInLevel.Length]);
        }
        #endregion
    }

    public void DeleteFiles()
    {
        if(Directory.Exists(Application.dataPath + "/Saves"))
        {
            Directory.Delete(Application.dataPath + "/Saves", true);
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        }
        else
        {
            Directory.CreateDirectory(Application.dataPath + "/Saves");
        }
    }

    public void LoadSaveScene()
    {
        levelLoader = FindObjectOfType<LevelLoader>();

        string saveString = File.ReadAllText(Application.dataPath + "/Saves/playerSave.txt");

        string[] contents = saveString.Split(new[] { SAVE_SEPERATOR }, System.StringSplitOptions.None);

        Debug.Log("Loading Level " + contents[19]);

        sceneSaved = int.Parse(contents[19]);

        string sceneName = SceneUtility.GetScenePathByBuildIndex(sceneSaved);

        if(levelLoader != null)
        {
            levelLoader.LoadNewLevel(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        didLoadSave = true;
    }

    public bool GetDidLoadSave()
    {
        return didLoadSave;
    }

    public void SetDidLoadSave(bool boolean)
    {
        didLoadSave = boolean;
    }

    public int SavedScene()
    {
        return sceneSaved;
    }
}
