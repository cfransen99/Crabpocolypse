using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconMove : MonoBehaviour
{
    public GameObject damageNum;
    public bool isPlayerIcon, isCasting;
    public string forecastName;
    public TurnBasedManager tbm;
    public GameObject connectedUnit;
    public Transform healthBar, healthBarBack, moneyBar, moneyBarBack;
    public TextMesh healthText, moneyText;
    public RewardCalculator rc;

    private bool canMove;
    private float castSpeed;
    private string action;
    private int guardID;

    private EnemyStats cStats;
    public float speed, maxHealth, power, health, maxMoney, money;
    private string[] abilityList, speedList;

    private DataCarryOver dco;
    private SoundManager sm;

    // Start is called before the first frame update
    void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.gameObject.GetComponent<SoundManager>();
        if (connectedUnit.name != "Player")
        {
            cStats = connectedUnit.GetComponent<EnemyStats>();
            speed = cStats.speed + (dco.playerLevel / 5 * 0.1f);
            maxHealth = cStats.maxHealth + (dco.playerLevel / 5 * 3);
            power = cStats.power + (dco.playerLevel / 5 * 2);
            abilityList = cStats.abilityList;
            speedList = cStats.speedList;
        }

        canMove = true;
        isCasting = false;
        castSpeed = 5.0f;
        health = maxHealth;
        UpdateMeter();
    }

    // Update is called once per frame
    void Update()
    {
        if (tbm.freezeTimeline || (tbm.isPlayersTurn && connectedUnit.name != "Player"))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        if (canMove)
        {
            if (isCasting)
            {
                if (transform.localPosition.x - castSpeed <= -850.0f)
                {
                    transform.localPosition = new Vector3(-850.0f, transform.localPosition.y, transform.localPosition.z);
                    isCasting = false;
                    SendAction(action);
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x - castSpeed, transform.localPosition.y, transform.localPosition.z);
                }
            }
            else
            {
                if (transform.localPosition.x - speed <= -425.0f && !isCasting)
                {
                    transform.localPosition = new Vector3(-425.0f, transform.localPosition.y, transform.localPosition.z);
                    if (!isPlayerIcon)
                    {
                        ChooseAction();
                        isCasting = true;
                    }
                    else
                    {
                        tbm.isPlayersTurn = true;
                    }
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x - speed, transform.localPosition.y, transform.localPosition.z);
                }
            }
        }
    }

    public void SetCastSpeed(string tier)
    {
        if (tier == "very slow")
        {
            castSpeed = 1.0f;
        }
        else if (tier == "slow")
        {
            castSpeed = 3.0f;
        }
        else if (tier == "medium")
        {
            castSpeed = 5.0f;
        }
        else if (tier == "fast")
        {
            castSpeed = 7.0f;
        }
        else if (tier == "very fast")
        {
            castSpeed = 9.0f;
        }
        else if (tier == "instant")
        {
            castSpeed = 427.0f;
        }
        if (tbm.isOverdrive && isPlayerIcon)
        {
            HastenCast();
        }
        if (tbm.turnsSpeedPlayer > 0 && isPlayerIcon)
        {
            HastenCast();
        }
        //25% chance to hasten cast again if playerSpeed high enough
        if (dco.playerSpeed >= 3 && isPlayerIcon)
        {
            int boost = Random.Range(0, 4);
            if (boost == 0)
            {
                HastenCast();
            }
        }
        isCasting = true;
    }

    public void HastenCast()
    {
        castSpeed += 2.0f;
    }

    public void SlowCast()
    {
        if ((castSpeed - 2.0f) > 0.0f)
        {
            castSpeed -= 2.0f;
        }
    }

    //Checks the the connected unit knows how to Guard
    private bool knowsGuard()
    {
        guardID = -1;
        bool knows = false;
        for (int i = 0; i < abilityList.Length; i++)
        {
            if (abilityList[i] == "Guard")
            {
                knows = true;
                guardID = i;
                i = abilityList.Length;
            }
        }
        return knows;
    }

    //Version for enemies; currently chooses randomly from available actions - change this later
    public void ChooseAction()
    {
        //Robo-Crab AI
        if (tbm.iconPlayer.transform.position.x < transform.position.x && (forecastName == "Robo-Crab A" || forecastName == "Robo-Crab B"))
        {
            int smart = Random.Range(0, 2);
            //50% chance to actively choose advantageous option
            if (smart == 1)
            {
                //Guards if player is ahead on timeline
                if (tbm.iconPlayer.transform.position.x <= transform.position.x)
                {
                    action = abilityList[guardID];
                    SetCastSpeed(speedList[guardID]);
                }
                //Attacks if player is behind
                else
                {
                    action = abilityList[0];
                    SetCastSpeed(speedList[0]);
                }
            }
            //50% chance to choose random action
            else if (smart == 0)
            {
                int chosen = Random.Range(0, abilityList.Length);
                action = abilityList[chosen];
                SetCastSpeed(speedList[chosen]);
            }
        }
        //Crabsolute Zero AI can only use basic attacks when "broken"; otherwise random
        else if (forecastName == "Crabsolute Zero")
        {
            if (tbm.bossBroken)
            {
                action = abilityList[0];
                SetCastSpeed(speedList[0]);
            }
            else
            {
                int chosen = Random.Range(0, abilityList.Length);
                action = abilityList[chosen];
                SetCastSpeed(speedList[chosen]);
            }
        }
        //Crabtain Clawdius warns the player whenever Special 2 is selected; can only use Attack when enraged
        else if (forecastName == "Crabtain Clawdius")
        {
            if (tbm.bossEnraged)
            {
                action = abilityList[0];
                SetCastSpeed(speedList[0]);
            }
            else
            {
                int chosen = Random.Range(0, abilityList.Length);
                action = abilityList[chosen];
                SetCastSpeed(speedList[chosen]);
                if (forecastName == "Crabtain Clawdius" && action == "Special2")
                {
                    string forecast = "Crabtain Clawdius eyes a sandwich";
                    if (tbm.numBossInterrupt == 2)
                    {
                        forecast += " menacingly...";
                    }
                    else if (tbm.numBossInterrupt == 1)
                    {
                        forecast += ", annoyed...";
                    }
                    else
                    {
                        forecast += " greedily...";
                    }
                    tbm.SendForecast(forecast);
                }
            }
        }
        //Crustacé Moustache can guard at the right times instead of randomly and "asks a question" when selecting Special2
        else if (forecastName == "Crustacé Moustache")
        {
            if (tbm.bossBroken)
            {
                action = abilityList[0];
                SetCastSpeed(speedList[0]);
            }
            else
            {
                bool choseGuard = false;
                //Checks if player is already casting and if Moustache doesn't have the armored buff
                if (tbm.iconPlayer.transform.position.x < transform.position.x && tbm.turnsDefE1 <= 0)
                {
                    //If so, 50% chance to use the Guard action
                    int smart = Random.Range(0, 2);
                    if (smart == 1)
                    {
                        action = "Guard";
                        SetCastSpeed("instant");
                        choseGuard = true;
                    }
                }
                //Otherwise chooses random ability
                if (!choseGuard)
                {
                    int chosen = Random.Range(0, abilityList.Length);
                    action = abilityList[chosen];
                    SetCastSpeed(speedList[chosen]);
                    if (action == "Special2")
                    {
                        int move = Random.Range(0, 3);
                        tbm.moustacheQuiz = move;
                        string forecast = "\"Quiz time! Show me your best ";
                        switch (move)
                        {
                            case 0:
                                forecast += "basic attack...\"";
                                break;
                            case 1:
                                forecast += "defensive stance...\"";
                                break;
                            case 2:
                                forecast += "special ability...\"";
                                break;
                            default:
                                break;
                        }
                        tbm.SendForecast(forecast);
                        tbm.MoustacheLinger();
                    }
                }
            }
        }
        //Jimmy will escape if he's the only remaining enemy
        else if (forecastName == "Jimmy")
        {
            if (tbm.numEnemies == 1)
            {
                action = "Escape";
                SetCastSpeed("slow");
            }
            else
            {
                int chosen = Random.Range(0, abilityList.Length);
                action = abilityList[chosen];
                SetCastSpeed(speedList[chosen]);
            }
        }
        //Crabraham Lincrab can guard at the right times instead of randomly
        else if (forecastName == "Crabraham Lincrab")
        {
            if (tbm.bossBroken)
            {
                action = abilityList[0];
                SetCastSpeed(speedList[0]);
            }
            else
            {
                bool choseGuard = false;
                //Checks if player is already casting and if Moustache doesn't have the armored/evasive buff
                if (tbm.iconPlayer.transform.position.x < transform.position.x && tbm.turnsDefE1 <= 0 && tbm.turnsEvadeE1 <= 0)
                {
                    //If so, 50% chance to use the Guard action
                    int smart = Random.Range(0, 2);
                    if (smart == 1)
                    {
                        action = "Guard";
                        SetCastSpeed("instant");
                        choseGuard = true;
                    }
                }
                //Otherwise chooses random ability
                if (!choseGuard)
                {
                    int chosen = Random.Range(0, abilityList.Length);
                    action = abilityList[chosen];
                    SetCastSpeed(speedList[chosen]);
                }
            }
        }
        //Basic AI chooses moves randomly
        else
        {
            int chosen = Random.Range(0, abilityList.Length);
            action = abilityList[chosen];
            SetCastSpeed(speedList[chosen]);
        }

        //Adjusts cast speed if Hastened
        if (connectedUnit.name == "Enemy1")
        {
            if (tbm.turnsSpeedE1 > 0)
            {
                HastenCast();
            }
            if (tbm.E1Guarding)
            {
                speed /= 2.0f;
                tbm.E1Guarding = false;
                tbm.animE1.SetBool("isGuarding", tbm.E1Guarding);
            }
        }
        else if (connectedUnit.name == "Enemy2")
        {
            if (tbm.turnsSpeedE2 > 0)
            {
                HastenCast();
            }
            if (tbm.E2Guarding)
            {
                speed /= 2.0f;
                tbm.E2Guarding = false;
                tbm.animE2.SetBool("isGuarding", tbm.E2Guarding);
            }
        }
    }

    //Version for player
    public void ChooseAction(string act)
    {
        action = act;
    }

    public void SendAction(string act)
    {
        tbm.CastAction(act, connectedUnit, power);
    }

    public void GetHeal(float amount)
    {
        if (health + amount >= maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += amount;
        }
        GameObject tempNum = Instantiate(damageNum, connectedUnit.transform.position, connectedUnit.transform.rotation);
        tempNum.GetComponent<DamageText>().SetDamage((int)amount);
        UpdateMeter();
    }

    public void GetHurt(float amount)
    {
        //Doubles BASE damage if interrupted
        //UNLESS target is Jimmy and attack is Moustache Special1
        if (isCasting)
        {
            if (forecastName == "Jimmy" && tbm.proteccJimmy)
            {
                amount *= 1;
            }
            else
            {
                amount *= 2;
            }
        }
        //Reduces non-interruption damage to Crabsolute Zero's claw to 1
        else if (forecastName == "Giant Claw")
        {
            amount = 1;
        }

        bool willEvade = false;
        bool isArmored = false;
        //Applies evade and defense buffs
        if (connectedUnit.name == "Enemy1" && dco.enemyType != "Shovel")
        {
            if (tbm.turnsEvadeE1 > 0)
            {
                int temp = Random.Range(0, 2);
                if (temp == 0)
                {
                    willEvade = true;
                    amount = 0.0f;
                    StartCoroutine("EvadeMessage");
                    sm.sfxPlayer.PlayOneShot(sm.soundDash);
                }
            }
            if (tbm.turnsDefE1 > 0 && !willEvade)
            {
                isArmored = true;
                int newDamage = (int)amount;
                newDamage /= 2;
                amount = newDamage;
                StartCoroutine("ArmorMessage");
                sm.sfxPlayer.PlayOneShot(sm.soundGuard);
            }
            if (tbm.E1Guarding && !willEvade)
            {
                int newDamage = (int)amount;
                newDamage /= 2;
                amount = newDamage;
                if (tbm.turnsDefE1 <= 0)
                {
                    StartCoroutine("GuardMessage");
                    sm.sfxPlayer.PlayOneShot(sm.soundGuard);
                }
            }
            if (!tbm.E1Guarding && !willEvade && tbm.turnsDefE1 <= 0)
            {
                sm.sfxPlayer.PlayOneShot(sm.soundHurt);
            }
        }
        else if (connectedUnit.name == "Enemy2")
        {
            if (tbm.turnsEvadeE2 > 0)
            {
                int temp = Random.Range(0, 2);
                if (temp == 0)
                {
                    willEvade = true;
                    amount = 0.0f;
                    StartCoroutine("EvadeMessage");
                    sm.sfxPlayer.PlayOneShot(sm.soundDash);
                }
            }
            if (tbm.turnsDefE2 > 0 && !willEvade)
            {
                isArmored = true;
                int newDamage = (int)amount;
                newDamage /= 2;
                amount = newDamage;
                StartCoroutine("ArmorMessage");
                sm.sfxPlayer.PlayOneShot(sm.soundGuard);
            }
            if (tbm.E2Guarding && !willEvade)
            {
                int newDamage = (int)amount;
                newDamage /= 2;
                amount = newDamage;
                if (tbm.turnsDefE2 <= 0)
                {
                    StartCoroutine("GuardMessage");
                    sm.sfxPlayer.PlayOneShot(sm.soundGuard);
                }
            }
            if (!tbm.E2Guarding && !willEvade && tbm.turnsDefE2 <= 0 && forecastName != "Giant Claw")
            {
                sm.sfxPlayer.PlayOneShot(sm.soundHurt);
            }
        }

        if (!willEvade)
        {
            if (health - amount <= 0.0f)
            {
                if (connectedUnit.name == "Enemy1")
                {
                    tbm.turnsPowE1 = 0;
                    tbm.turnsDefE1 = 0;
                    tbm.turnsSpeedE1 = 0;
                    tbm.turnsEvadeE1 = 0;
                    tbm.auraE1.StopGenerate("all");
                }
                else if (connectedUnit.name == "Enemy2")
                {
                    tbm.turnsPowE2 = 0;
                    tbm.turnsDefE2 = 0;
                    tbm.turnsSpeedE2 = 0;
                    tbm.turnsEvadeE2 = 0;
                    tbm.auraE2.StopGenerate("all");
                }
                if (forecastName == "Crabsolute Zero" && tbm.iconE2.gameObject.activeSelf)
                {
                    tbm.turnsPowE2 = 0;
                    tbm.turnsDefE2 = 0;
                    tbm.turnsSpeedE2 = 0;
                    tbm.turnsEvadeE2 = 0;
                    tbm.auraE2.StopGenerate("all");
                    tbm.iconE2.gameObject.SetActive(false);
                    tbm.enemy2.gameObject.SetActive(false);
                    tbm.numEnemies--;
                }
                else if (forecastName == "Giant Claw")
                {
                    sm.sfxPlayer.PlayOneShot(sm.soundHurt);
                    tbm.SendForecast("Giant Claw destroyed! Crabsolute Zero weakened!");
                    tbm.bossBroken = true;
                }
                else if (forecastName == "Jimmy")
                {
                    tbm.SendForecast("Without Jimmy, Crustacé Moustache flew into a frenzy!");
                    tbm.bossBroken = true;
                    tbm.animE1.SetBool("isEnraged", tbm.bossBroken);
                    tbm.iconE1.speed *= 2;
                }
                SendRewards();
                health = 0.0f;
                tbm.numEnemies--;
                if (tbm.numEnemies == 0)
                {
                    rc.PushRewards();
                    tbm.DoWin();
                }
                connectedUnit.SetActive(false);
                gameObject.SetActive(false);
            }
            else
            {
                health -= amount;
                if (forecastName == "Crabsolute Zero")
                {
                    tbm.animE1.SetTrigger("isHurting");
                }
                if (forecastName == "Crabtain Clawdius")
                {
                    tbm.animE1.SetTrigger("doHurt");
                }
                if (forecastName == "Crustacé Moustache")
                {
                    tbm.animE1.SetTrigger("doHurt");
                    tbm.animE1.SetBool("isEnraged", tbm.bossBroken);
                }
                if (forecastName == "Giant Claw" && !isCasting)
                {
                    sm.sfxPlayer.PlayOneShot(sm.soundGuard);
                }
            }
            if (amount > 0.0f && isCasting && !isArmored)
            {
                if (forecastName == "Giant Claw" && health > 0)
                {
                    sm.sfxPlayer.PlayOneShot(sm.soundHurt);
                    tbm.SendForecast("Giant Claw took massive damage and returned.");
                    tbm.animE1.SetTrigger("doRegen");
                    isCasting = false;
                    dco.bossAidHP = (int)health;
                    tbm.turnsPowE2 = 0;
                    tbm.turnsDefE2 = 0;
                    tbm.turnsSpeedE2 = 0;
                    tbm.turnsEvadeE2 = 0;
                    tbm.auraE2.StopGenerate("all");
                    tbm.enemy2.gameObject.SetActive(false);
                    tbm.iconE2.gameObject.SetActive(false);
                    tbm.numEnemies--;
                    if (connectedUnit.activeSelf)
                    {
                        StartCoroutine("InterruptMessage");
                    }
                    tbm.pendingTut3 = true;
                }
                else if (forecastName == "Crabsolute Zero" && health > 0 && !tbm.bossBroken)
                {
                    tbm.SendForecast("Crabsolute Zero grew hastened in retaliation.");
                    isCasting = false;
                    transform.localPosition = new Vector3(215.0f, transform.localPosition.y, transform.localPosition.z);
                    tbm.SpeedBuff("Enemy1", 1);
                    if (connectedUnit.activeSelf)
                    {
                        StartCoroutine("InterruptMessage");
                    }
                    tbm.pendingTut3 = true;
                    tbm.pendingTut8 = true;
                }
                else if (forecastName == "Crabtain Clawdius" && health > 0 && !tbm.bossEnraged)
                {
                    tbm.SendForecast("Crabtain Clawdius grew empowered in retaliation.");
                    isCasting = false;
                    transform.localPosition = new Vector3(215.0f, transform.localPosition.y, transform.localPosition.z);
                    tbm.AttackBuff("Enemy1", 1);
                    if (connectedUnit.activeSelf)
                    {
                        StartCoroutine("InterruptMessage");
                    }
                    tbm.pendingTut3 = true;
                    tbm.pendingTut8 = true;
                    if (action == "Special2")
                    {
                        tbm.numBossInterrupt++;
                        if (tbm.numBossInterrupt == 2)
                        {
                            tbm.clawdiusWarning = true;
                        }
                        else if (tbm.numBossInterrupt == 3)
                        {
                            tbm.SendForecast("Crabtain Clawdius grew enraged.");
                            tbm.numBossInterrupt = 0;
                            tbm.bossEnraged = true;
                            speed *= 2;
                            tbm.turnsEnragedE1 = 5;
                            tbm.animE1.SetBool("isEnraged", tbm.bossEnraged);
                        }
                    }
                }
                else if (forecastName == "Crustacé Moustache" && health > 0 && !tbm.bossBroken)
                {
                    tbm.SendForecast("Crustacé Moustache grew armored in retaliation.");
                    isCasting = false;
                    transform.localPosition = new Vector3(215.0f, transform.localPosition.y, transform.localPosition.z);
                    tbm.DefenseBuff("Enemy1", 1);
                    if (connectedUnit.activeSelf)
                    {
                        StartCoroutine("InterruptMessage");
                    }
                    tbm.pendingTut3 = true;
                    tbm.pendingTut8 = true;
                }
                else if (forecastName == "Crabraham Lincrab" && health > 0 && !tbm.bossBroken)
                {
                    tbm.SendForecast("Crabraham Lincrab grew evasive in retaliation.");
                    isCasting = false;
                    transform.localPosition = new Vector3(215.0f, transform.localPosition.y, transform.localPosition.z);
                    tbm.EvadeBuff("Enemy1", 1);
                    if (connectedUnit.activeSelf)
                    {
                        StartCoroutine("InterruptMessage");
                    }
                    tbm.pendingTut3 = true;
                    tbm.pendingTut8 = true;
                }
                else
                {
                    isCasting = false;
                    transform.localPosition = new Vector3(215.0f, transform.localPosition.y, transform.localPosition.z);
                    if (connectedUnit.activeSelf)
                    {
                        StartCoroutine("InterruptMessage");
                    }
                    tbm.pendingTut3 = true;
                }
            }
        }
        GameObject tempNum = Instantiate(damageNum, connectedUnit.transform.position, connectedUnit.transform.rotation);
        tempNum.GetComponent<DamageText>().SetDamage((int)-amount);
        UpdateMeter();
    }

    private void SendRewards()
    {
        //Crab Witches and Holy Crabs called in final boss fight do not drop rewards
        if (dco.enemyType != "Lincrab" || forecastName == "Crabraham Lincrab")
        {
            for (int i = 0; i < cStats.dropList.Length; i++)
            {
                rc.GenerateRewards(cStats.dropList[i], cStats.dropChance[i], cStats.dropNum[i]);
            }
        }
    }

    public void UpdateMeter()
    {
        if (!isPlayerIcon)
        {
            healthBar.localScale = new Vector3(health / maxHealth, healthBar.localScale.y, healthBar.localScale.z);
            healthBar.localPosition = new Vector3((healthBar.localScale.x - 1.0f) / 2.0f, healthBar.localPosition.y, healthBar.localPosition.z);
            healthText.text = health + "/" + maxHealth;
            if (forecastName == "Crabraham Lincrab")
            {
                moneyBar.localScale = new Vector3(money / maxMoney, moneyBar.localScale.y, moneyBar.localScale.z);
                moneyBar.localPosition = new Vector3((moneyBar.localScale.x - 1.0f) / 2.0f, moneyBar.localPosition.y, moneyBar.localPosition.z);
                moneyText.text = money + "/" + maxMoney;
            }
        }
    }

    public void RefreshEncounter()
    {
        StartCoroutine("Refresh");
    }

    public void RefreshBossEncounter()
    {
        StartCoroutine("RefreshBoss");
    }

    //Only on Crabraham Lincrab
    public void ChangeMoney(int amount)
    {
        if (money + amount >= maxMoney)
        {
            money = maxMoney;
        }
        else if (money + amount <= 0)
        {
            money = 0;
        }
        else
        {
            money += amount;
        }
    }

    /* Never called i think?
    public void ScaleStats(int amount)
    {
        maxHealth += amount * 2;
        health = maxHealth;
        power += amount;
        speed += amount;
        UpdateMeter();
    }*/
    
    IEnumerator InterruptMessage()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempNum = Instantiate(damageNum, connectedUnit.transform.position, connectedUnit.transform.rotation);
        tempNum.GetComponent<DamageText>().SetMessage("Interrupted!");
    }

    IEnumerator ArmorMessage()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempNum = Instantiate(damageNum, connectedUnit.transform.position, connectedUnit.transform.rotation);
        tempNum.GetComponent<DamageText>().SetMessage("Armored!");
    }

    IEnumerator EvadeMessage()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempNum = Instantiate(damageNum, connectedUnit.transform.position, connectedUnit.transform.rotation);
        tempNum.GetComponent<DamageText>().SetMessage("Evaded!");
    }

    IEnumerator GuardMessage()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempNum = Instantiate(damageNum, connectedUnit.transform.position, connectedUnit.transform.rotation);
        tempNum.GetComponent<DamageText>().SetMessage("Guarded!");
    }

    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(0.01f);
        cStats = connectedUnit.GetComponent<EnemyStats>();
        speed = cStats.speed + (dco.playerLevel / 5 * 0.1f);
        maxHealth = cStats.maxHealth + (dco.playerLevel / 5 * 2);
        power = cStats.power + (dco.playerLevel / 5);
        abilityList = cStats.abilityList;
        speedList = cStats.speedList;
        health = maxHealth;
        UpdateMeter();
    }

    IEnumerator RefreshBoss()
    {
        yield return new WaitForSeconds(0.01f);
        cStats = connectedUnit.GetComponent<EnemyStats>();
        speed = cStats.speed + (dco.playerLevel / 5 * 0.1f);
        maxHealth = cStats.maxHealth + (dco.playerLevel / 5 * 2);
        power = cStats.power + (dco.playerLevel / 5);
        abilityList = cStats.abilityList;
        speedList = cStats.speedList;
        //Just omits the CURRENT health portion, as this can change between re-encounters and is handled elsewhere
        UpdateMeter();
    }
}
