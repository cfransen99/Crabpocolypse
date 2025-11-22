using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator battleTransition;
    public float transitionTime = 1;
    public GameObject player;

    private PlayerEnemyCollision collide;
    private PlayerStats stats;
    private PlayerMovement move;
    private Player play;
    private PlayerLevel level;
    private Inventory inv;
    private DataCarryOver dco;
    private SoundManager sm;

    private EnemyBase[] enemies;
    private ItemPickup[] itemPickups;

    private bool isNewLevel;

    private void Start()
    {
        collide = player.GetComponent<PlayerEnemyCollision>();
        stats = player.GetComponent<PlayerStats>();
        move = player.GetComponent<PlayerMovement>();
        play = player.GetComponent<Player>();
        level = player.GetComponent<PlayerLevel>();
        inv = player.GetComponent<Inventory>();
        if (SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "DemoOver")
        {
            dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
            sm = dco.GetComponent<SoundManager>();
        }

        enemies = FindObjectsOfType<EnemyBase>();
        itemPickups = FindObjectsOfType<ItemPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collide != null)
        {
            if (collide.Trigger())
            {
                LoadBattleScene();
            }
        }
    }

    public void LoadBattleScene()
    {
        StartCoroutine(LoadLevel("BattleScene"));

        stats.EndCoroutine();
        dco.playerHealth = stats.playerHealth;
        dco.playerDrive = stats.playerDrive;
        dco.overdriveLocked = stats.overdriveLocked;
        dco.enemyType = collide.enemyType;
        dco.inDrive = play.isOverdrive;
        dco.sceneName = SceneManager.GetActiveScene().name;
        dco.playerPosY = player.transform.position.y + 0.5f;
        dco.playerPosZ = player.transform.position.z;
        if (move.lookRight)
        {
            dco.playerPosX = player.transform.position.x - 0.5f;
        }
        else
        {
            dco.playerPosX = player.transform.position.x + 0.5f;
        }
        dco.facingRight = move.lookRight;
        dco.isAdvantage = collide.isAdvantage;
        dco.isAmbushed = play.isAmbush;
        dco.playerExp = level.experience;
        dco.playerPoints = level.skillPoints;
        dco.playerLevel = level.level;
        dco.XPCap = level.experienceToNextLevel;
        dco.encounterID = collide.enemyID;

        foreach (Item item in inv.items)
        {
            if (item.name == "Crab Meat S")
            {
                dco.invMeatS = item.amount;
            }
            else if (item.name == "Crab Meat M")
            {
                dco.invMeatM = item.amount;
            }
            else if (item.name == "Crab Meat L")
            {
                dco.invMeatL = item.amount;
            }
            else if (item.name == "Sandwich")
            {
                dco.invMeatXL = item.amount;
            }
            else if (item.name == "Broken Claw")
            {
                dco.invPow = item.amount;
            }
            else if (item.name == "Robo Crab Meat")
            {
                dco.invDef = item.amount;
            }
            else if (item.name == "Crabbit Ears")
            {
                dco.invSpeed = item.amount;
            }
            else if (item.name == "Crab Roll")
            {
                dco.invSpeed2 = item.amount;
            }
            else if (item.name == "Witch's Hat")
            {
                dco.invMagic = item.amount;
            }
            else if (item.name == "Crab Puff Supreme")
            {
                dco.invCheat = item.amount;
            }
        }

        foreach (EnemyBase enemy in enemies)
        {
            if (enemy.isDead)
            {
                dco.enemyDead[enemy.enemyID] = true;
            }
            else
            {
                dco.enemyDead[enemy.enemyID] = false;
            }
        }

        foreach (ItemPickup item in itemPickups)
        {
            if (item.itemID != -1)
            {
                if (item.isPickedUp)
                {
                    dco.itemPickedUp[item.itemID] = true;
                }
                else
                {
                    dco.itemPickedUp[item.itemID] = false;
                }
            }
        }

        dco.isNewLevel = false;
    }

    public void LoadBattleScene(string enemyType)
    {
        StartCoroutine(LoadLevel("BattleScene"));

        stats.EndCoroutine();
        dco.playerHealth = stats.playerHealth;
        dco.playerDrive = stats.playerDrive;
        dco.overdriveLocked = stats.overdriveLocked;
        dco.enemyType = enemyType;
        dco.inDrive = play.isOverdrive;
        dco.sceneName = SceneManager.GetActiveScene().name;
        dco.playerPosY = player.transform.position.y + 0.5f;
        dco.playerPosZ = player.transform.position.z;
        if (move.lookRight)
        {
            dco.playerPosX = player.transform.position.x - 0.5f;
        }
        else
        {
            dco.playerPosX = player.transform.position.x + 0.5f;
        }
        dco.facingRight = move.lookRight;
        dco.isAdvantage = collide.isAdvantage;
        dco.isAmbushed = play.isAmbush;
        dco.playerExp = level.experience;
        dco.playerPoints = level.skillPoints;
        dco.playerLevel = level.level;
        dco.XPCap = level.experienceToNextLevel;
        dco.encounterID = collide.enemyID;

        foreach (Item item in inv.items)
        {
            if (item.name == "Crab Meat S")
            {
                dco.invMeatS = item.amount;
            }
            else if (item.name == "Crab Meat M")
            {
                dco.invMeatM = item.amount;
            }
            else if (item.name == "Crab Meat L")
            {
                dco.invMeatL = item.amount;
            }
            else if (item.name == "Sandwich")
            {
                dco.invMeatXL = item.amount;
            }
            else if (item.name == "Broken Claw")
            {
                dco.invPow = item.amount;
            }
            else if (item.name == "Robo Crab Meat")
            {
                dco.invDef = item.amount;
            }
            else if (item.name == "Crabbit Ears")
            {
                dco.invSpeed = item.amount;
            }
            else if (item.name == "Crab Roll")
            {
                dco.invSpeed2 = item.amount;
            }
            else if (item.name == "Witch's Hat")
            {
                dco.invMagic = item.amount;
            }
            else if (item.name == "Crab Puff Supreme")
            {
                dco.invCheat = item.amount;
            }
        }

        foreach (EnemyBase enemy in enemies)
        {
            if (enemy.isDead)
            {
                dco.enemyDead[enemy.enemyID] = true;
            }
            else
            {
                dco.enemyDead[enemy.enemyID] = false;
            }
        }

        foreach (ItemPickup item in itemPickups)
        {
            if (item.itemID != -1)
            {
                if (item.isPickedUp)
                {
                    dco.itemPickedUp[item.itemID] = true;
                }
                else
                {
                    dco.itemPickedUp[item.itemID] = false;
                }
            }
        }

        dco.isNewLevel = false;
    }

    public void LoadNewLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));

        Time.timeScale = 1;

        if (stats != null)
        {
            stats.EndCoroutine();
        }
        if(dco != null)
        {
            if(SceneManager.GetActiveScene().name != "BattleScene")
            {
                dco.overdriveLocked = stats.overdriveLocked;
                if (dco.overdriveLocked)
                {
                    dco.playerHealth = stats.playerHealth;
                    dco.playerDrive = stats.playerDrive;
                }
                else
                {
                    dco.playerHealth = stats.playerMaxHealth;
                    dco.playerDrive = stats.playerMaxDrive;
                }
                dco.playerExp = level.experience;
                dco.playerPoints = level.skillPoints;
                dco.playerLevel = level.level;

                foreach (Item item in inv.items)
                {
                    if (item.name == "Crab Meat S")
                    {
                        dco.invMeatS = item.amount;
                    }
                    else if (item.name == "Crab Meat M")
                    {
                        dco.invMeatM = item.amount;
                    }
                    else if (item.name == "Crab Meat L")
                    {
                        dco.invMeatL = item.amount;
                    }
                    else if (item.name == "Sandwich")
                    {
                        dco.invMeatXL = item.amount;
                    }
                    else if (item.name == "Broken Claw")
                    {
                        dco.invPow = item.amount;
                    }
                    else if (item.name == "Robo Crab Meat")
                    {
                        dco.invDef = item.amount;
                    }
                    else if (item.name == "Crabbit Ears")
                    {
                        dco.invSpeed = item.amount;
                    }
                    else if (item.name == "Crab Roll")
                    {
                        dco.invSpeed2 = item.amount;
                    }
                    else if (item.name == "Witch's Hat")
                    {
                        dco.invMagic = item.amount;
                    }
                    else if (item.name == "Crab Puff Supreme")
                    {
                        dco.invCheat = item.amount;
                    }
                }

                //foreach (EnemyBase enemy in enemies)
                //{
                //    if (enemy.isDead)
                //    {
                //        dco.enemyDead[enemy.enemyID] = true;
                //    }
                //    else
                //    {
                //        dco.enemyDead[enemy.enemyID] = false;
                //    }
                //}

                //foreach (ItemPickup item in itemPickups)
                //{
                //    if (item.isPickedUp)
                //    {
                //        dco.itemPickedUp[item.itemID] = true;
                //    }
                //    else
                //    {
                //        dco.itemPickedUp[item.itemID] = false;
                //    }
                //}

                dco.isNewLevel = true;
            }
        }
    }

    IEnumerator LoadLevel(string levelName)
    {
        battleTransition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        if (levelName == "Crapsolute0Fight" || levelName == "Level8" || levelName == "Level12" || levelName == "Level16")
        {
            sm.musicPlayer.Stop();
        }
        else if (levelName != "BattleScene" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            sm.musicPlayer.clip = sm.musicLevel;
            sm.musicPlayer.Play();
        }
        SceneManager.LoadScene(levelName);
    }

    public bool GetIsNewLevel()
    {
        return isNewLevel;
    }
}
