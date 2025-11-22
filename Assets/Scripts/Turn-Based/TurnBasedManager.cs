using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TurnBasedManager : MonoBehaviour
{
    public bool devMode, clawdiusWarning;
    public GameObject[] devModeCrabs;
    public GameObject damageNum, spellCircle, maxBurst, overBurst, holyBurst, clawdiusProj, lincrabBurst;
    public Material skyMat1, skyMat2;
    public int numEnemies, numBossInterrupt;
    public Animator animPlayer, animIcon, animE1, animE2, animClaw, animDoor, sceneTrans;
    public IconMove iconPlayer, iconE1, iconE2;
    public GameObject panelMain, panelTarget, panelPlayerDat, panelTimeline, panelWin, panelLose, panelAbility, panelItem, panelIRecover, panelIStatus, panelIOther, panelEscape, panelDesc, panelForecast, panelDisable, panelPushed, buttonDev, buttonFun, buttonPushOut, sceneryFactory, sceneryBeach, sceneryShip, sceneryRoof, sceneryShack, sceneryOffice, scenerySpace, sceneryFinal, regMOver, altMOver;
    public Transform playerPos, enemy1Pos, enemy2Pos, player, enemy1, enemy2;
    public Image playerHealthBar, playerDriveBar;
    public Text healthText, driveText, descText, forecastText;
    public Text invTexMeatS, invTexMeatM, invTexMeatL, invTexMeatXL, invTexPow, invTexDef, invTexSpeed, invTexSpeed2, invTexMagic, invTexCheat;
    public Text abiDash, abiAir, abiJump, abiBarrier;
    public Transform targetA, targetB, targetSelected, targetPlayer;
    public AuraGenerator auraPlayer, auraE1, auraE2;
    public EnemyChoose ec;
    public UIDescriptions ui;
    public TutorialHub th;
    public bool isOverdrive = false;
    public bool isPlayersTurn = false;
    public bool freezeTimeline = false;
    public bool inTutorialHub = false;

    private float camTimer = 0.0f;
    private float driveTimer = 0.0f;
    private float runTimer = 0.0f;
    private float forecastTimer = 0.0f;
    private string forecastTarget, refreshBuff, camTarget, lincrabAlly;
    private float playerHealth, playerDrive, playerMaxHealth, playerMaxDrive, enemyStrength;
    private bool isTransforming, isGuarding, isRunning, willAttack, willEscape, isWeak, e1Running, e2Running, sentDamage, isCheating, forecastUp, camJumping, soundTrans, bossBeaten;
    public bool E1Guarding, E2Guarding, pendingTut2, pendingTut3, pendingTut4, pendingTut5, pendingTut6, pendingTut7, pendingTut8, battleOver, bossBroken, bossEnraged, proteccJimmy;
    public int turnsPowPlayer, turnsDefPlayer, turnsSpeedPlayer, turnsEvadePlayer, turnsPowE1, turnsDefE1, turnsSpeedE1, turnsEvadeE1, turnsPowE2, turnsDefE2, turnsSpeedE2, turnsEvadeE2, turnsEnragedE1, moustacheQuiz;
    private int refundDrive, delayedDamage;
    private Vector3 defaultCamPos, defaultPlayerPos, defaultE1Pos, defaultE2Pos, camStart, camEnd;
    private DataCarryOver dco;
    private SoundManager sm;

    // Start is called before the first frame update
    void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.gameObject.GetComponent<SoundManager>();
        defaultCamPos = transform.position;
        defaultPlayerPos = player.position;
        defaultE1Pos = enemy1.position;
        defaultE2Pos = enemy2.position;
        playerMaxHealth = 100;
        playerMaxDrive = 100;
        playerHealth = dco.playerHealth;
        playerDrive = dco.playerDrive;
        isTransforming = false;
        isGuarding = false;
        isRunning = false;
        willAttack = false;
        willEscape = false;
        isWeak = false;
        e1Running = false;
        e2Running = false;
        sentDamage = false;
        battleOver = false;
        isCheating = dco.cheating;
        UpdateBars();
        UpdateAbilities();
        camTarget = "default";

        //Changes scenery to match level
        if (dco.sceneName == "Level5" || dco.sceneName == "Level6" || dco.sceneName == "Level7")
        {
            RenderSettings.skybox = skyMat1;
            sceneryFactory.SetActive(false);
            sceneryBeach.SetActive(true);
            sceneryShip.SetActive(false);
            sceneryRoof.SetActive(false);
            sceneryShack.SetActive(false);
            sceneryOffice.SetActive(false);
            scenerySpace.SetActive(false);
            sceneryFinal.SetActive(false);
        }
        else if (dco.sceneName == "Level8")
        {
            RenderSettings.skybox = skyMat1;
            sceneryFactory.SetActive(false);
            sceneryBeach.SetActive(false);
            sceneryShip.SetActive(true);
            sceneryRoof.SetActive(false);
            sceneryShack.SetActive(false);
            sceneryOffice.SetActive(false);
            scenerySpace.SetActive(false);
            sceneryFinal.SetActive(false);
        }
        else if (dco.sceneName == "Level9")
        {
            RenderSettings.skybox = skyMat1;
            sceneryFactory.SetActive(false);
            sceneryBeach.SetActive(false);
            sceneryShip.SetActive(false);
            sceneryRoof.SetActive(true);
            sceneryShack.SetActive(false);
            sceneryOffice.SetActive(false);
            scenerySpace.SetActive(false);
            sceneryFinal.SetActive(false);
        }
        else if (dco.sceneName == "Level10" || dco.sceneName == "Level11")
        {
            RenderSettings.skybox = skyMat1;
            sceneryFactory.SetActive(false);
            sceneryBeach.SetActive(false);
            sceneryShip.SetActive(false);
            sceneryRoof.SetActive(false);
            sceneryShack.SetActive(true);
            sceneryOffice.SetActive(false);
            scenerySpace.SetActive(false);
            sceneryFinal.SetActive(false);
        }
        else if (dco.sceneName == "Level12")
        {
            RenderSettings.skybox = skyMat1;
            regMOver.SetActive(false);
            altMOver.SetActive(true);
            sceneryFactory.SetActive(false);
            sceneryBeach.SetActive(false);
            sceneryShip.SetActive(false);
            sceneryRoof.SetActive(false);
            sceneryShack.SetActive(false);
            sceneryOffice.SetActive(true);
            scenerySpace.SetActive(false);
            sceneryFinal.SetActive(false);
        }
        else if (dco.sceneName == "Level13" || dco.sceneName == "Level14" || dco.sceneName == "Level15")
        {
            RenderSettings.skybox = skyMat2;
            sceneryFactory.SetActive(false);
            sceneryBeach.SetActive(false);
            sceneryShip.SetActive(false);
            sceneryRoof.SetActive(false);
            sceneryShack.SetActive(false);
            sceneryOffice.SetActive(false);
            scenerySpace.SetActive(true);
            sceneryFinal.SetActive(false);
        }
        else if (dco.sceneName == "Level16")
        {
            RenderSettings.skybox = skyMat2;
            if (dco.enemyType == "Lincrab")
            {
                regMOver.SetActive(false);
                altMOver.SetActive(true);
            }
            sceneryFactory.SetActive(false);
            sceneryBeach.SetActive(false);
            sceneryShip.SetActive(false);
            sceneryRoof.SetActive(false);
            sceneryShack.SetActive(false);
            sceneryOffice.SetActive(false);
            scenerySpace.SetActive(false);
            sceneryFinal.SetActive(true);
        }
        else
        {
            RenderSettings.skybox = skyMat1;
            sceneryFactory.SetActive(true);
            sceneryBeach.SetActive(false);
            sceneryShip.SetActive(false);
            sceneryRoof.SetActive(false);
            sceneryShack.SetActive(false);
            sceneryOffice.SetActive(false);
            scenerySpace.SetActive(false);
            sceneryFinal.SetActive(false);
        }

        if (playerDrive == 0 || dco.overdriveLocked)
        {
            panelDisable.SetActive(true);
        }

        iconPlayer.speed += (dco.playerSpeed * 0.1f);

        if (dco.enemyType == "Lincrab" && !dco.returningBoss)
        {
            StartCoroutine("LincrabSteal");
        }

        //Adjusts the health of the boss/helpers to previous levels if re-encountering the boss
        if (dco.returningBoss && (dco.enemyType == "Zero" || dco.enemyType == "Clawdius" || dco.enemyType == "Moustache" || dco.enemyType == "Shovel" || dco.enemyType == "Lincrab"))
        {
            StartCoroutine("ReturnToBoss");
        }

        if (dco.isAdvantage)
        {
            iconPlayer.transform.localPosition = new Vector3(-350.0f, 0.0f, 0.0f);
            StartCoroutine("AdvantageDamage");
            SendForecast("Max made a preemptive strike!");
            dco.isAdvantage = false;
            pendingTut4 = true;
        }
        else if (dco.isAmbushed)
        {
            iconPlayer.transform.localPosition = new Vector3(850.0f, 0.0f, 0.0f);
            iconE1.transform.localPosition = new Vector3(350.0f, 0.0f, 0.0f);
            if (numEnemies == 2)
            {
                iconE2.transform.localPosition = new Vector3(550.0f, 0.0f, 0.0f);
            }
            SendForecast("Max was ambushed!");
            dco.isAmbushed = false;
            pendingTut4 = true;
        }

        isOverdrive = dco.inDrive;
        if (isOverdrive)
        {
            //Triggers transformation flag to Update function; should probably be reworked later to use events instead
            isTransforming = true;
            iconPlayer.speed *= 2.0f;

            animPlayer.SetBool("isOverdrive", isOverdrive);
            animIcon.SetBool("isOverdrive", isOverdrive);
        }

        //Checks for Items tutorial flags
        UpdateInventory();
        //Checks for Overdrive tutorial flags
        if (!dco.overdriveLocked)
        {
            pendingTut7 = true;
        }

        if (dco.enemyType != "Shovel" && dco.enemyType != "Lincrab")
        {
            CheckForTutorials();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Ensures that the timeline freezes whenever an action isn't occuring
        if (isPlayersTurn && !panelMain.activeSelf && !panelTarget.activeSelf && !panelAbility.activeSelf && !panelItem.activeSelf && !panelIRecover.activeSelf && !panelIStatus.activeSelf && !panelIOther.activeSelf && !inTutorialHub)
        {
            panelMain.SetActive(true);
            freezeTimeline = true;
            if (isGuarding)
            {
                isGuarding = false;
                animPlayer.SetBool("isGuarding", isGuarding);
                iconPlayer.speed /= 2.0f;
            }
            //I couldn't find any other way to make the hurt animation play out of running when escaping
            //So that's why this is here... I mean it works...
            willEscape = false;
            animPlayer.SetBool("willEscape", willEscape);
        }

        //Timing stuff for transformation animation's camera movements; can probably be reworked with events
        if (isTransforming)
        {
            freezeTimeline = true;
            camTimer += Time.deltaTime;
            //Takes a tenth of a second to move to/zoom in on the player as they transform
            if (camTimer < 1.0f)
            {
                transform.position = Vector3.Lerp(defaultCamPos, playerPos.position, camTimer * 10.0f);
                if (camTimer >= 0.7f && !soundTrans)
                {
                    sm.sfxPlayer.PlayOneShot(sm.soundKaching);
                    soundTrans = true;
                }
            }
            //Takes another tenth of a second to move back to its original position/zoom after the transformation has passed
            else if (camTimer >= 1.0f && camTimer < 1.1f)
            {
                transform.position = Vector3.Lerp(playerPos.position, defaultCamPos, (camTimer - 1.0f) * 10.0f);
            }
            //Ends the function and resets the timer
            else
            {
                transform.position = defaultCamPos;
                if (!isPlayersTurn)
                {
                    freezeTimeline = false;
                }
                soundTrans = false;
                isTransforming = false;
                camTimer = 0.0f;
            }
        }

        //Used for CameraJump()
        if (camJumping)
        {
            camTimer += Time.deltaTime;
            if (camTarget == "player")
            {
                camEnd = playerPos.position;
            }
            else if (camTarget == "enemy1")
            {
                camEnd = enemy1Pos.position;
            }
            else if (camTarget == "enemy2")
            {
                camEnd = enemy2Pos.position;
            }

            if (camTimer < 0.2f)
            {
                transform.position = Vector3.Lerp(camStart, camEnd, camTimer * 5.0f);
            }
            else
            {
                transform.position = camEnd;
                camJumping = false;
                camTimer = 0.0f;
            }
        }
        else if (camTarget != "default")
        {
            if (camTarget == "player")
            {
                transform.position = playerPos.position;
            }
            else if (camTarget == "enemy1")
            {
                transform.position = enemy1Pos.position;
            }
            else if (camTarget == "enemy2")
            {
                transform.position = enemy2Pos.position;
            }
        }

        //Ticks down the player's drive meter while Overdrive is active
        if (isOverdrive && !freezeTimeline && playerDrive > 0.0f && !isCheating)
        {
            driveTimer += Time.deltaTime;
            if (driveTimer >= 0.2f && playerDrive > 0.0f)
            {
                //Sets to 0 if it would be less than 0
                if (playerDrive - 1.0f < 0.0f)
                {
                    playerDrive = 0.0f;
                }
                else
                {
                    playerDrive -= 1.0f;
                    driveTimer = 0.0f;
                }
                //Reverts Overdrive if drive reaches 0
                if (playerDrive == 0.0f)
                {
                    ToggleOverdrive();
                    panelDisable.SetActive(true);
                }
                UpdateBars();
            }
        }

        //Timing stuff for basic attack's running animation; can probably be reworked with events
        if (isRunning && willAttack)
        {
            runTimer += Time.deltaTime;
            //Takes half a second to run to the target
            if (runTimer < 0.5f)
            {
                player.position = Vector3.Lerp(defaultPlayerPos, targetSelected.position, runTimer * 2.0f);
            }
            //Resets timer and moves on to next step
            else
            {
                player.position = targetSelected.position;
                isRunning = false;
                animPlayer.SetBool("isRunning", isRunning);
                runTimer = 0.0f;
            }
        }
        //Same as above
        else if (willAttack)
        {
            runTimer += Time.deltaTime;
            //Spends a quarter second in the basic attack animation before next run sequence
            if (runTimer >= 0.1f && !sentDamage)
            {
                sentDamage = true;
                int damageDealt = 5 + (dco.playerPow / 2);
                if (isOverdrive)
                {
                    damageDealt *= 2;
                }
                if (turnsPowPlayer > 0)
                {
                    float temp = damageDealt;
                    temp *= 1.5f;
                    damageDealt = (int)temp;
                }

                if (targetSelected == targetA)
                {
                    iconE1.GetHurt(damageDealt);
                }
                else if (targetSelected == targetB)
                {
                    iconE2.GetHurt(damageDealt);
                }

                //25% chance to give 1 turn attack buff if enough playerPow
                if (dco.playerPow >= 3)
                {
                    int boost = Random.Range(0, 4);
                    if (boost == 0)
                    {
                        AttackBuff("Player", 1);
                        refreshBuff = "pow";
                    }
                }
            }
            else if (runTimer >= 0.25f)
            {
                sentDamage = false;
                player.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                willAttack = false;
                isRunning = true;
                animPlayer.SetBool("isRunning", isRunning);
                animPlayer.SetBool("willAttack", willAttack);
                runTimer = 0.0f;
            }
        }
        //Same as above
        else if (isRunning && !willEscape)
        {
            runTimer += Time.deltaTime;
            //Takes half a second to run back to original position
            if (runTimer < 0.5f)
            {
                player.position = Vector3.Lerp(targetSelected.position, defaultPlayerPos, runTimer * 2.0f);
            }
            //Resets all used values during basic attack animation; again, I should rework this later
            else
            {
                CountDownBuffs("Player");
                iconPlayer.transform.localPosition = new Vector3(850.0f, transform.localPosition.y, transform.localPosition.z);
                player.position = defaultPlayerPos;
                player.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                willAttack = false;
                isRunning = false;
                animPlayer.SetBool("isRunning", isRunning);
                animPlayer.SetBool("willAttack", willAttack);
                runTimer = 0.0f;
                freezeTimeline = false;
                if (playerDrive > 0 && !dco.overdriveLocked)
                {
                    panelDisable.SetActive(false);
                }
                CameraJump("default");
                animPlayer.applyRootMotion = false;
                if (clawdiusWarning)
                {
                    clawdiusWarning = false;
                    StartCoroutine("ClawdiusWarning");
                }
                else
                {
                    CheckForTutorials();
                }
            }
        }

        //Timing stuff for enemy 1's basic attack animation; can probably be reworked with events
        if (e1Running)
        {
            runTimer += Time.deltaTime;
            //Takes half a second to run to the target
            if (runTimer < 0.5f)
            {
                enemy1.position = Vector3.Lerp(defaultE1Pos, targetPlayer.position, runTimer * 2.0f);
            }
            //Takes half a second to run back; triggers player's hurt animation
            else if (runTimer >= 0.5f && runTimer < 1.0f)
            {
                if (!sentDamage)
                {
                    sentDamage = true;
                    float damage = enemyStrength;
                    if (turnsPowE1 > 0)
                    {
                        damage *= 1.5f;
                    }
                    GetHurt((int)damage);
                }
                else
                {
                    animPlayer.ResetTrigger("isHurting");
                }
                enemy1.position = Vector3.Lerp(targetPlayer.position, defaultE1Pos, (runTimer - 0.5f) * 2.0f);
            }
            //Resets timer and moves on to next step
            else
            {
                CountDownBuffs("Enemy1");
                iconE1.transform.localPosition = new Vector3(850.0f, transform.localPosition.y, transform.localPosition.z);
                enemy1.position = defaultE1Pos;
                sentDamage = false;
                e1Running = false;
                animE1.SetBool("isRunning", e1Running);
                runTimer = 0.0f;
                freezeTimeline = false;
                if (playerDrive > 0 && !dco.overdriveLocked)
                {
                    panelDisable.SetActive(false);
                }
                CameraJump("default");
                CheckForTutorials();
            }
        }
        //Same as above for enemy 2
        if (e2Running)
        {
            runTimer += Time.deltaTime;
            //Takes half a second to run to the target
            if (runTimer < 0.5f)
            {
                enemy2.position = Vector3.Lerp(defaultE2Pos, targetPlayer.position, runTimer * 2.0f);
            }
            //Takes half a second to run back; triggers player's hurt animation
            else if (runTimer >= 0.5f && runTimer < 1.0f)
            {
                if (!sentDamage)
                {
                    sentDamage = true;
                    float damage = enemyStrength;
                    if (turnsPowE1 > 0)
                    {
                        damage *= 1.5f;
                    }
                    GetHurt((int)damage);
                }
                else
                {
                    animPlayer.ResetTrigger("isHurting");
                }
                enemy2.position = Vector3.Lerp(targetPlayer.position, defaultE2Pos, (runTimer - 0.5f) * 2.0f);
            }
            //Resets timer and moves on to next step
            else
            {
                CountDownBuffs("Enemy2");
                iconE2.transform.localPosition = new Vector3(850.0f, transform.localPosition.y, transform.localPosition.z);
                enemy2.position = defaultE2Pos;
                sentDamage = false;
                e2Running = false;
                animE2.SetBool("isRunning", e2Running);
                runTimer = 0.0f;
                freezeTimeline = false;
                if (playerDrive > 0 && !dco.overdriveLocked)
                {
                    panelDisable.SetActive(false);
                }
                CameraJump("default");
                CheckForTutorials();
            }
        }

        //Ensures that the battle forecast always stays for full length and then disappears
        if (forecastUp)
        {
            forecastTimer += Time.deltaTime;
            if (forecastTimer > 1.5f)
            {
                forecastTimer = 0.0f;
                forecastUp = false;
                panelForecast.SetActive(false);
            }
        }

        //Opens Tutorial Hub
        if (Input.GetKeyDown(KeyCode.Escape) && !(freezeTimeline && !isPlayersTurn) && !inTutorialHub)
        {
            th.gameObject.SetActive(true);
            th.OpenHub();
        }
        
        //Continue Update function stuffs here

        //Dev-Mode enable
        if (Input.GetKeyDown(KeyCode.T))
        {
            devMode = true;
        }
        if (devMode && !battleOver)
        {
            buttonDev.SetActive(true);
            buttonFun.SetActive(true);
            buttonPushOut.SetActive(true);

        }
        else
        {
            buttonDev.SetActive(false);
            buttonFun.SetActive(false);
            buttonPushOut.SetActive(false);
        }
    }

    //Unlocks and opens 
    public void CheckForTutorials()
    {
        //Basic Combat
        if (!dco.tutorialsUnlocked[0])
        {
            dco.tutorialsUnlocked[0] = true;
            th.tutorialPanel[0].SetActive(true);
            panelPlayerDat.SetActive(false);
            panelForecast.SetActive(false);
            panelDisable.SetActive(false);
            freezeTimeline = true;
            isPlayersTurn = false;
        }
        //Basic Defense
        else if (!dco.tutorialsUnlocked[1] && pendingTut2)
        {
            dco.tutorialsUnlocked[1] = true;
            th.tutorialPanel[1].SetActive(true);
            panelPlayerDat.SetActive(false);
            panelForecast.SetActive(false);
            panelDisable.SetActive(false);
            freezeTimeline = true;
            isPlayersTurn = false;
        }
        //Advanced Combat
        else if (!dco.tutorialsUnlocked[2] && pendingTut3)
        {
            dco.tutorialsUnlocked[2] = true;
            th.tutorialPanel[2].SetActive(true);
            panelPlayerDat.SetActive(false);
            panelForecast.SetActive(false);
            panelDisable.SetActive(false);
            freezeTimeline = true;
            isPlayersTurn = false;
        }
        //Advantage & Ambush
        else if (!dco.tutorialsUnlocked[3] && pendingTut4)
        {
            dco.tutorialsUnlocked[3] = true;
            th.tutorialPanel[3].SetActive(true);
            panelPlayerDat.SetActive(false);
            panelForecast.SetActive(false);
            panelDisable.SetActive(false);
            freezeTimeline = true;
            isPlayersTurn = false;
        }
        //Items
        else if (!dco.tutorialsUnlocked[4] && pendingTut5)
        {
            dco.tutorialsUnlocked[4] = true;
            th.tutorialPanel[4].SetActive(true);
            panelPlayerDat.SetActive(false);
            panelForecast.SetActive(false);
            panelDisable.SetActive(false);
            freezeTimeline = true;
            isPlayersTurn = false;
        }
        //Status Effects
        else if (!dco.tutorialsUnlocked[5] && pendingTut6)
        {
            dco.tutorialsUnlocked[5] = true;
            th.tutorialPanel[5].SetActive(true);
            panelPlayerDat.SetActive(false);
            panelForecast.SetActive(false);
            panelDisable.SetActive(false);
            freezeTimeline = true;
            isPlayersTurn = false;
        }
        //Overdrive
        else if (!dco.tutorialsUnlocked[6] && pendingTut7)
        {
            dco.tutorialsUnlocked[6] = true;
            th.tutorialPanel[6].SetActive(true);
            panelPlayerDat.SetActive(false);
            panelForecast.SetActive(false);
            panelDisable.SetActive(false);
            freezeTimeline = true;
            isPlayersTurn = false;
        }
        //Retaliation
        else if (!dco.tutorialsUnlocked[7] && pendingTut8)
        {
            dco.tutorialsUnlocked[7] = true;
            th.tutorialPanel[7].SetActive(true);
            panelPlayerDat.SetActive(false);
            panelForecast.SetActive(false);
            panelDisable.SetActive(false);
            freezeTimeline = true;
            isPlayersTurn = false;
        }
    }

    //Lowers player's health and spawns a damage number
    public void GetHurt(float amount)
    {
        //Doubles BASE damage if interrupted (before defense calculated)
        if (iconPlayer.isCasting)
        {
            amount *= 2;
        }

        //Cast to int to avoid decimals; should probably replace all health stuff with ints instead of floats...
        int damage = (int)(amount * (1 - dco.playerDef / 100.0f));
        if (damage < 0)
        {
            damage = 0;
        }

        bool willEvade = false;
        //Rolls for evade if under evasion buff
        if (turnsEvadePlayer > 0)
        {
            int temp = Random.Range(0, 2);
            if (temp == 0)
            {
                willEvade = true;
                damage = 0;
                StartCoroutine("EvadeMessage");
                sm.sfxPlayer.PlayOneShot(sm.soundDash);
            }
        }

        if (!willEvade)
        {
            //Half damage if guarding
            if (isGuarding)
            {
                damage /= 2;
                if (turnsDefPlayer <= 0)
                {
                    StartCoroutine("GuardMessage");
                    sm.sfxPlayer.PlayOneShot(sm.soundGuard);
                }
            }
            //Also half it if in Overdrive
            if (isOverdrive)
            {
                damage /= 2;
            }
            //Also half it if defense buffed
            if (turnsDefPlayer > 0)
            {
                damage /= 2;
                StartCoroutine("ArmorMessage");
                sm.sfxPlayer.PlayOneShot(sm.soundGuard);
            }
            if (!isGuarding && turnsDefPlayer <= 0)
            {
                sm.sfxPlayer.PlayOneShot(sm.soundHurt);
            }

            if (playerHealth - damage <= 0.0f)
            {
                playerHealth = 0.0f;
                DoLose();
            }
            else
            {
                playerHealth -= damage;
                //Reduces drive meter by amount equal to damage received when guarding in Overdrive
                //Otherwise quartering damage basically for free would be crazy
                if (isOverdrive && isGuarding)
                {
                    if (playerDrive - damage >= 1.0f)
                    {
                        playerDrive -= damage;
                    }
                    else
                    {
                        playerDrive = 1.0f;
                    }
                }
            }

            //If health after damage is below a third of max, switches to weakened idle animation
            if (playerHealth <= (playerMaxHealth / 3.0f))
            {
                isWeak = true;
                animPlayer.SetBool("isWeak", isWeak);
            }

            //If damage taken gets reduced to 0 somehow, hurt animation doesn't play and casting isn't interrupted
            if (damage > 0.0f && turnsDefPlayer <= 0)
            {
                //Interrupt Escape animation if in progress
                if (isRunning)
                {
                    isRunning = false;
                    animPlayer.SetBool("isRunning", isRunning);
                    player.gameObject.GetComponent<SpriteRenderer>().flipX = false;
                }
                if (!isGuarding)
                {
                    animPlayer.SetTrigger("isHurting");
                }
                if (iconPlayer.isCasting)
                {
                    //Retaliation skills if player stats are high enough; no refreshBuff since the player's turn is elongated
                    if (dco.playerPow >= 6)
                    {
                        AttackBuff("Player", 1);
                    }
                    if (dco.playerDef >= 6)
                    {
                        DefenseBuff("Player", 1);
                    }
                    if (dco.playerSpeed >= 6)
                    {
                        SpeedBuff("Player", 1);
                    }

                    iconPlayer.isCasting = false;
                    iconPlayer.transform.localPosition = new Vector3(215.0f, transform.localPosition.y, transform.localPosition.z);
                    StartCoroutine("InterruptMessage");
                    playerDrive += refundDrive;
                    refundDrive = 0;
                    pendingTut3 = true;
                }
            }
        }
        GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
        tempNum.GetComponent<DamageText>().SetDamage(-damage);
        UpdateBars();
    }

    //Redraws the player's health and drive bars to reflect their current values
    private void UpdateBars()
    {
        playerHealthBar.fillAmount = playerHealth / playerMaxHealth;
        healthText.text = playerHealth + "/" + playerMaxHealth;
        playerDriveBar.fillAmount = playerDrive / playerMaxDrive;
        driveText.text = playerDrive + "/" + playerMaxDrive;
    }

    //Updates text on inventory buttons
    private void UpdateInventory()
    {
        if (dco.invMeatS > 0 || dco.enemyType == "Lincrab")
        {
            if (dco.enemyType == "Lincrab")
            {
                invTexMeatS.text = "Crab Meat S - *";
            }
            else
            {
                invTexMeatS.text = "Crab Meat S - " + dco.invMeatS;
            }
            pendingTut5 = true;
        }
        else
            invTexMeatS.text = "-";

        if (dco.invMeatM > 0 || dco.enemyType == "Lincrab")
        {
            if (dco.enemyType == "Lincrab")
            {
                invTexMeatM.text = "Crab Meat M - *";
            }
            else
            {
                invTexMeatM.text = "Crab Meat M - " + dco.invMeatM;
            }
            pendingTut5 = true;
        }
        else
            invTexMeatM.text = "-";

        if (dco.invMeatL > 0 || dco.enemyType == "Lincrab")
        {
            if (dco.enemyType == "Lincrab")
            {
                invTexMeatL.text = "Crab Meat L - *";
            }
            else
            {
                invTexMeatL.text = "Crab Meat L - " + dco.invMeatL;
            }
            pendingTut5 = true;
        }
        else
            invTexMeatL.text = "-";

        if (dco.invMeatXL > 0 || dco.enemyType == "Lincrab")
        {
            if (dco.enemyType == "Lincrab")
            {
                invTexMeatXL.text = "Sandwich - *";
            }
            else
            {
                invTexMeatXL.text = "Sandwich - " + dco.invMeatXL;
            }
            pendingTut5 = true;
        }
        else
            invTexMeatXL.text = "-";

        if (dco.invPow > 0 || dco.enemyType == "Lincrab")
        {
            if (dco.enemyType == "Lincrab")
            {
                invTexPow.text = "Broken Claw - *";
            }
            else
            {
                invTexPow.text = "Broken Claw - " + dco.invPow;
            }
            pendingTut5 = true;
        }
        else
            invTexPow.text = "-";

        if (dco.invDef > 0 || dco.enemyType == "Lincrab")
        {
            if (dco.enemyType == "Lincrab")
            {
                invTexDef.text = "Robo-Crab Meat - *";
            }
            else
            {
                invTexDef.text = "Robo-Crab Meat - " + dco.invDef;
            }
            pendingTut5 = true;
        }
        else
            invTexDef.text = "-";

        if (dco.invSpeed > 0 || dco.enemyType == "Lincrab")
        {
            if (dco.enemyType == "Lincrab")
            {
                invTexSpeed.text = "Crabbit Ears - *";
            }
            else
            {
                invTexSpeed.text = "Crabbit Ears - " + dco.invSpeed;
            }
            pendingTut5 = true;
        }
        else
            invTexSpeed.text = "-";

        if (dco.invSpeed2 > 0 || dco.enemyType == "Lincrab")
        {
            if (dco.enemyType == "Lincrab")
            {
                invTexSpeed2.text = "Crab Roll - *";
            }
            else
            {
                invTexSpeed2.text = "Crab Roll - " + dco.invSpeed2;
            }
            pendingTut5 = true;
        }
        else
            invTexSpeed2.text = "-";

        if (dco.invMagic > 0)
        {
            invTexMagic.text = "Witch's Hat - " + dco.invMagic;
            pendingTut5 = true;
        }
        else
            invTexMagic.text = "-";

        if (dco.invCheat > 0)
        {
            invTexCheat.text = "Crab Puff Supreme - " + dco.invCheat;
            pendingTut5 = true;
        }
        else
            invTexCheat.text = "-";
    }

    //Updates text on Ability buttons
    private void UpdateAbilities()
    {
        if (dco.learnDash)
        {
            abiDash.text = "Dash";
        }
        else
        {
            abiDash.text = "-";
        }
        if (dco.learnAir)
        {
            abiAir.text = "Air Dash";
        }
        else
        {
            abiAir.text = "-";
        }
        if (dco.learnJump)
        {
            abiJump.text = "Power Jump";
        }
        else
        {
            abiJump.text = "-";
        }
        if (dco.learnBarrier)
        {
            abiBarrier.text = "Barrier";
        }
        else
        {
            abiBarrier.text = "-";
        }
    }

    //Switches the active HUD from main to target-selection
    public void AttackButtonPress()
    {
        panelDesc.SetActive(false);
        if (enemy1.gameObject.activeSelf && enemy2.gameObject.activeSelf)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            panelTarget.SetActive(true);
            panelMain.SetActive(false);
        }
        //If only one enemy, will automatically target the living one
        else if (!enemy1.gameObject.activeSelf)
        {
            panelMain.SetActive(false);
            TargetBPress();
        }
        else if (!enemy2.gameObject.activeSelf)
        {
            panelMain.SetActive(false);
            TargetAPress();
        }
    }

    //Switches the active HUD from target-selection to main
    public void BackButtonPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        if (panelIRecover.activeSelf)
        {
            panelIRecover.SetActive(false);
            panelItem.SetActive(true);
        }
        else if (panelIStatus.activeSelf)
        {
            panelIStatus.SetActive(false);
            panelItem.SetActive(true);
        }
        else if (panelIOther.activeSelf)
        {
            panelIOther.SetActive(false);
            panelItem.SetActive(true);
        }
        else
        {
            panelTarget.SetActive(false);
            panelAbility.SetActive(false);
            panelItem.SetActive(false);
            panelMain.SetActive(true);
        }
    }

    //Performs a basic attack on Target A
    public void TargetAPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        forecastTarget = iconE1.forecastName;
        panelTarget.SetActive(false);
        targetSelected = targetA;
        iconPlayer.SetCastSpeed("medium");
        iconPlayer.ChooseAction("Attack");
        isPlayersTurn = false;
        freezeTimeline = false;
    }

    //Performs a basic attack on Target B
    public void TargetBPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        forecastTarget = iconE2.forecastName;
        panelTarget.SetActive(false);
        targetSelected = targetB;
        iconPlayer.SetCastSpeed("medium");
        iconPlayer.ChooseAction("Attack");
        isPlayersTurn = false;
        freezeTimeline = false;
    }

    //Switches the active HUD from main to ability list
    public void AbilityButtonPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        panelAbility.SetActive(true);
        panelMain.SetActive(false);
    }

    //Attempts to perform a Dash ability
    public void UseDash()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        if (playerDrive < 10.0f || !dco.learnDash)
        {
            descText.text = "Not enough Drive!";
        }
        else
        {
            playerDrive -= 10.0f;
            refundDrive = 5;
            panelDesc.SetActive(false);
            panelAbility.SetActive(false);
            iconPlayer.SetCastSpeed("medium");
            iconPlayer.ChooseAction("Dash");
            isPlayersTurn = false;
            freezeTimeline = false;
            UpdateBars();
            if (playerDrive == 0)
            {
                panelDisable.SetActive(true);
            }
        }
    }

    //Attempts to perform an Air Dash ability
    public void UseAir()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        if (playerDrive < 15.0f || !dco.learnAir)
        {
            descText.text = "Not enough Drive!";
        }
        else
        {
            playerDrive -= 15.0f;
            refundDrive = 7;
            panelDesc.SetActive(false);
            panelAbility.SetActive(false);
            iconPlayer.SetCastSpeed("fast");
            iconPlayer.ChooseAction("Air");
            isPlayersTurn = false;
            freezeTimeline = false;
            UpdateBars();
            if (playerDrive == 0)
            {
                panelDisable.SetActive(true);
            }
        }
    }

    //Attempts to perform a Power Jump ability
    public void UseJump()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        if (playerDrive < 20.0f || !dco.learnJump)
        {
            descText.text = "Not enough Drive!";
        }
        else
        {
            playerDrive -= 20.0f;
            refundDrive = 10;
            panelDesc.SetActive(false);
            panelAbility.SetActive(false);
            iconPlayer.SetCastSpeed("slow");
            iconPlayer.ChooseAction("Jump");
            isPlayersTurn = false;
            freezeTimeline = false;
            UpdateBars();
            if (playerDrive == 0)
            {
                panelDisable.SetActive(true);
            }
        }
    }

    //Attempts to perform a Barrer ability
    public void UseBarrier()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        if (playerDrive < 30.0f || !dco.learnBarrier)
        {
            descText.text = "Not enough Drive!";
        }
        else
        {
            playerDrive -= 30.0f;
            refundDrive = 15;
            panelDesc.SetActive(false);
            panelAbility.SetActive(false);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("Barrier");
            isPlayersTurn = false;
            freezeTimeline = false;
            UpdateBars();
            if (playerDrive == 0)
            {
                panelDisable.SetActive(true);
            }
        }
    }

    //Performs a guard
    public void GuardButtonPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        panelDesc.SetActive(false);
        panelMain.SetActive(false);
        iconPlayer.SetCastSpeed("instant");
        iconPlayer.ChooseAction("Guard");
        isPlayersTurn = false;
        freezeTimeline = false;
    }

    //Switches the active HUD from main to inventory
    public void ItemButtonPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        UpdateInventory();
        panelItem.SetActive(true);
        panelMain.SetActive(false);
    }
    
    //Switches the active HUD from inventory to recover
    public void RecoverButtonPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        panelIRecover.SetActive(true);
        panelItem.SetActive(false);
    }

    //Uses a Crab Meat S if possible
    public void UseMeatS()
    {
        if (dco.invMeatS > 0 || dco.enemyType == "Lincrab")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("MeatS");
            panelIRecover.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Crab Meat M if possible
    public void UseMeatM()
    {
        if (dco.invMeatM > 0 || dco.enemyType == "Lincrab")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("MeatM");
            panelIRecover.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Crab Meat L if possible
    public void UseMeatL()
    {
        if (dco.invMeatL > 0 || dco.enemyType == "Lincrab")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("MeatL");
            panelIRecover.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Sandwich if possible
    public void UseMeatXL()
    {
        if (dco.invMeatXL > 0 || dco.enemyType == "Lincrab")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("MeatXL");
            panelIRecover.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Broken Claw if possible
    public void UsePow()
    {
        if (dco.invPow > 0 || dco.enemyType == "Lincrab")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("Pow");
            panelIStatus.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Robo Crab Meat if possible
    public void UseDef()
    {
        if (dco.invDef > 0 || dco.enemyType == "Lincrab")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("Def");
            panelIStatus.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Crabbit Ears if possible
    public void UseSpeed()
    {
        if (dco.invSpeed > 0 || dco.enemyType == "Lincrab")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("Speed");
            panelIStatus.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Crab Roll if possible
    public void UseSpeed2()
    {
        if (dco.invSpeed2 > 0 || dco.enemyType == "Lincrab")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("very fast");
            iconPlayer.ChooseAction("Speed2");
            panelIStatus.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Witch's Hat if possible
    public void UseMagic()
    {
        if (dco.invMagic > 0)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            SendForecast("Magical energies swarm around the Witch's Hat...");
            animPlayer.SetTrigger("useItem");
            dco.invMagic--;
            UpdateInventory();

            panelIOther.SetActive(false);
            panelDesc.SetActive(false);

            int spell = Random.Range(0, 6);
            switch (spell)
            {
                case 0:
                    iconPlayer.SetCastSpeed("fast");
                    iconPlayer.ChooseAction("Cure");
                    break;
                case 1:
                    iconPlayer.SetCastSpeed("medium");
                    iconPlayer.ChooseAction("Hasten");
                    break;
                case 2:
                    iconPlayer.SetCastSpeed("medium");
                    iconPlayer.ChooseAction("Ward");
                    break;
                case 3:
                    iconPlayer.SetCastSpeed("fast");
                    iconPlayer.ChooseAction("Fireball");
                    break;
                case 4:
                    iconPlayer.SetCastSpeed("slow");
                    iconPlayer.ChooseAction("Frostball");
                    break;
                case 5:
                    iconPlayer.SetCastSpeed("very slow");
                    iconPlayer.ChooseAction("Dispose");
                    break;
                default: break;
            }
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Uses a Crab Puff Supreme if possible
    public void UseCheat()
    {
        if (dco.invCheat > 0)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            iconPlayer.SetCastSpeed("instant");
            iconPlayer.ChooseAction("Cheat");
            panelIOther.SetActive(false);
            panelDesc.SetActive(false);
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Switches the active HUD from inventory to status
    public void StatusButtonPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        panelIStatus.SetActive(true);
        panelItem.SetActive(false);
    }

    //Switches the active HUD from inventory to recover
    public void OtherButtonPress()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        panelIOther.SetActive(true);
        panelItem.SetActive(false);
    }

    //Performs an escape
    public void EscapeButtonPress()
    {
        if (dco.enemyType == "Zero" || dco.enemyType == "Clawdius" || dco.enemyType == "Moustache" || dco.enemyType == "Shovel" || dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(false);
            SendForecast(iconE1.forecastName + " blocked your path.");
        }
        else
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            panelDesc.SetActive(false);
            if (isGuarding)
            {
                isGuarding = false;
                animPlayer.SetBool("isGuarding", isGuarding);
                iconPlayer.speed /= 2.0f;
            }
            panelMain.SetActive(false);
            iconPlayer.SetCastSpeed("slow");
            iconPlayer.ChooseAction("Escape");
            isRunning = true;
            willEscape = true;
            animPlayer.SetBool("isRunning", isRunning);
            animPlayer.SetBool("willEscape", willEscape);
            player.gameObject.GetComponent<SpriteRenderer>().flipX = true;
            isPlayersTurn = false;
            freezeTimeline = false;
        }
    }

    //Checks if panelDisable should be active
    public bool PlayerHasDrive()
    {
        if (playerDrive == 0)
        {
            return false;
        }
        return true;
    }

    //Puts the player into Overdrive if possible, otherwise disables Overdrive
    public void ToggleOverdrive()
    {
        ui.ExitMousePanels();
        //Runs check to see if the player's drive meter isn't empty and there isn't an action currently happening
        if (playerDrive > 0.0f && (isPlayersTurn || !freezeTimeline))
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            //Disables Overdrive and lowers cast speed if an action was being loaded
            if (isOverdrive)
            {
                isOverdrive = false;
                iconPlayer.speed /= 2.0f;
                iconPlayer.SlowCast();
            }
            //Enables Overdrive and raises cast speed if an action was being loaded
            else
            {
                isOverdrive = true;
                //Triggers transformation flag to Update function; should probably be reworked later to use events instead
                isTransforming = true;
                iconPlayer.speed *= 2.0f;
                iconPlayer.HastenCast();
                SendForecast("Max entered Overdrive!");
            }
            animPlayer.SetBool("isOverdrive", isOverdrive);
            animIcon.SetBool("isOverdrive", isOverdrive);
        }
        //Called when the player is forced out of Overdrive (empty drive meter)
        else
        {
            //Reverts Overdrive but delays it as long as there is an ongoing action
            if (isOverdrive && (isPlayersTurn || !freezeTimeline))
            {
                isOverdrive = false;
                iconPlayer.speed /= 2.0f;
                iconPlayer.SlowCast();
                animPlayer.SetBool("isOverdrive", isOverdrive);
                animIcon.SetBool("isOverdrive", isOverdrive);
            }
        }
    }

    //Displays a message about the previous action on screen (especially useful w/o animation done yet)
    public void SendForecast(string forecast)
    {
        forecastText.text = forecast;
        if (forecastUp)
        {
            forecastTimer = 0.0f;
        }
        else
        {
            panelForecast.SetActive(true);
            forecastUp = true;
        }
    }

    //Performs a specific action as passed to it by the corresponding IconMove on the timeline
    public void CastAction(string act, GameObject unit, float power)
    {
        ui.ExitMousePanels();
        panelDisable.SetActive(true);
        if (act == "Attack")
        {
            if (unit.name == "Player")
            {
                SendForecast("Max attacked " + forecastTarget + ".");
                freezeTimeline = true;
                //Triggers timing flags to Update function; rework this later to use events instead
                isRunning = true;
                willAttack = true;
                animPlayer.SetBool("isRunning", isRunning);
                animPlayer.SetBool("willAttack", willAttack);
                CameraJump("player");
                animPlayer.applyRootMotion = true;
                if (moustacheQuiz == 0)
                {
                    moustacheQuiz = -1;
                }
            }
            else if (unit.name == "Enemy1")
            {
                if (iconE1.forecastName == "Crabsolute Zero")
                {
                    SendForecast("Crabsolute Zero attacked Max.");
                    if (turnsPowE1 > 0)
                    {
                        power *= 1.5f;
                    }
                    if (bossBroken)
                    {
                        power /= 2;
                    }
                    delayedDamage = (int)power;
                    StartCoroutine("ZeroAttack");
                }
                else if (iconE1.forecastName == "Crabtain Clawdius")
                {
                    SendForecast("Crabtain Clawdius attacked Max.");
                    if (turnsPowE1 > 0)
                    {
                        power *= 1.5f;
                    }
                    delayedDamage = ((int)power);
                    StartCoroutine("ClawdiusAttack");
                }
                else if (iconE1.forecastName == "Crustacé Moustache")
                {
                    SendForecast("Crustacé Moustache attacked Max.");
                    animE1.SetTrigger("doAttack");
                    if (turnsPowE1 > 0)
                    {
                        power *= 1.5f;
                    }
                    if (bossBroken)
                    {
                        power *= 2;
                    }
                    delayedDamage = (int)power;
                    StartCoroutine("MoustacheAttack");
                }
                else if (iconE1.forecastName == "Crabraham Lincrab")
                {
                    SendForecast("Crabraham Lincrab attacked Max.");
                    animE1.SetTrigger("doAttack");
                    if (turnsPowE1 > 0 || bossBroken)
                    {
                        power *= 1.5f;
                    }
                    delayedDamage = (int)power;
                    StartCoroutine("LincrabAttack");
                }
                else
                {
                    SendForecast(iconE1.forecastName + " attacked Max.");
                    freezeTimeline = true;
                    enemyStrength = power;
                    //Triggers timing flags to Update function; rework this later to use events instead
                    e1Running = true;
                    sentDamage = false;
                    animE1.SetBool("isRunning", e1Running);
                    CameraJump("enemy1");
                }
            }
            else if (unit.name == "Enemy2")
            {
                if (iconE2.forecastName == "Giant Claw")
                {
                    SendForecast("Giant Claw hurtled into Max.");
                    dco.bossAidHP = (int)iconE2.health;
                    iconE2.healthBarBack.gameObject.SetActive(false);
                    iconE2.healthText.gameObject.SetActive(false);
                    if (turnsPowE2 > 0)
                    {
                        power *= 1.5f;
                    }
                    delayedDamage = (int)power;
                    StartCoroutine("ClawAttack");
                }
                else if (iconE2.forecastName == "Cannon")
                {
                    SendForecast("Cannon fired at Max.");
                    Instantiate(clawdiusProj, new Vector3(3.0f, 1.0f, 0.75f), clawdiusProj.transform.rotation);
                    animE2.SetTrigger("doAttack");
                    if (turnsPowE2 > 0)
                    {
                        power *= 1.5f;
                    }
                    delayedDamage = ((int)power);
                    StartCoroutine("CannonAttack");
                }
                else if (iconE2.forecastName == "Jimmy")
                {
                    SendForecast("Jimmy flailed meekly at Max.");
                    if (turnsPowE2 > 0)
                    {
                        power *= 1.5f;
                    }
                    delayedDamage = (int)power;
                    StartCoroutine("JimmyAttack");
                }
                else
                {
                    SendForecast(iconE2.forecastName + " attacked Max.");
                    freezeTimeline = true;
                    enemyStrength = power;
                    //Triggers timing flags to Update function; rework this later to use events instead
                    e2Running = true;
                    sentDamage = false;
                    animE2.SetBool("isRunning", e2Running);
                    CameraJump("enemy2");
                }
            }
        }
        else if (act == "Dash")
        {
            SendForecast("Max dashed in place and grew hastened.");
            refundDrive = 0;
            if (moustacheQuiz == 2)
            {
                moustacheQuiz = -1;
            }
            StartCoroutine("PlayerDash");
        } //Only usable by player
        else if (act == "Air")
        {
            SendForecast("Max dashed through the air and grew evasive.");
            refundDrive = 0;
            if (moustacheQuiz == 2)
            {
                moustacheQuiz = -1;
            }
            StartCoroutine("PlayerAir");
        } //Only usable by player
        else if (act == "Jump")
        {
            SendForecast("Max leapt into the air and crashed back down.");
            refundDrive = 0;
            if (moustacheQuiz == 2)
            {
                moustacheQuiz = -1;
            }
            int damageDealt = 10 + (dco.playerPow / 2);
            if (isOverdrive)
            {
                damageDealt *= 2;
            }
            if (turnsPowPlayer > 0)
            {
                float temp = damageDealt;
                temp *= 1.5f;
                damageDealt = (int)temp;
            }
            delayedDamage = damageDealt;
            StartCoroutine("PlayerJump");
        } //Only usable by player
        else if (act == "Barrier")
        {
            SendForecast("Max constructed a barrier and grew armored.");
            refundDrive = 0;
            if (moustacheQuiz == 2)
            {
                moustacheQuiz = -1;
            }
            DefenseBuff("Player", 3);
            refreshBuff = "def";
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "Guard")
        {
            pendingTut2 = true;
            sm.sfxPlayer.PlayOneShot(sm.soundGuard);
            if (unit.name == "Player")
            {
                SendForecast("Max took up a defensive stance.");
                iconPlayer.speed *= 2.0f;
                isGuarding = true;
                animPlayer.SetBool("isGuarding", isGuarding);
                if (moustacheQuiz == 1)
                {
                    moustacheQuiz = -1;
                }

                //25% chance to gain buff if enough playerDef
                if (dco.playerDef >= 3)
                {
                    int boost = Random.Range(0, 4);
                    if (boost == 0)
                    {
                        DefenseBuff("Player", 1);
                        refreshBuff = "def";
                    }
                }
                StartCoroutine("PlayerEndPause");
            }
            else if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " took up a defensive stance.");
                iconE1.speed *= 2.0f;
                E1Guarding = true;
                animE1.SetBool("isGuarding", E1Guarding);
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " took up a defensive stance.");
                iconE2.speed *= 2.0f;
                E2Guarding = true;
                animE2.SetBool("isGuarding", E2Guarding);
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Escape")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundDash);
            if (unit.name == "Player")
            {
                SendForecast("Max escaped from the battle and dropped his winnings.");
                turnsPowPlayer = 0;
                turnsDefPlayer = 0;
                turnsSpeedPlayer = 0;
                turnsEvadePlayer = 0;
                auraPlayer.StopGenerate("all");
                freezeTimeline = true;
                panelMain.SetActive(false);
                panelTarget.SetActive(false);
                panelPlayerDat.SetActive(false);
                panelTimeline.SetActive(false);
                StartCoroutine("EscapeScreen");
            }
            else if (unit.name == "Enemy2")
            {
                if (iconE2.forecastName == "Jimmy")
                {
                    SendForecast("Jimmy thanked Max and escaped with his life.");
                    animE2.SetTrigger("doEscape");
                    iconE2.rc.PushRewards();
                    DoWin();
                }
            }
        }
        else if (act == "Startle")
        {
            if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " struck quickly to startle Max.");
                enemyStrength = power / 2.0f;
                GetHurt(enemyStrength);
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " struck quickly to startle Max.");
                enemyStrength = power / 2.0f;
                GetHurt(enemyStrength);
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Help")
        {
            if (unit.name == "Enemy1")
            {
                if (enemy2.gameObject.activeSelf)
                {
                    SendForecast(iconE1.forecastName + " cried out for help... but no one arrived.");
                }
                else
                {
                    ec.SummonCrabbit();
                    SendForecast(iconE1.forecastName + " cried out for help... " + iconE2.forecastName +  " arrived.");
                }
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                if (enemy1.gameObject.activeSelf || dco.enemyType == "Lincrab")
                {
                    SendForecast(iconE2.forecastName + " cried out for help... but no one arrived.");
                }
                else
                {
                    ec.SummonCrabbit();
                    SendForecast(iconE2.forecastName + " cried out for help... " + iconE1.forecastName + " arrived.");
                }
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Crush")
        {
            if (unit.name == "Enemy1")
            {
                if (iconE1.forecastName == "Crabraham Lincrab")
                {
                    SendForecast("Crabraham Lincrab crushed Max in his powerful claw.");
                    animE1.SetTrigger("doAttack");
                    if (turnsPowE1 > 0)
                    {
                        power *= 1.5f;
                    }
                    delayedDamage = (int)(power * 1.5f);
                    StartCoroutine("LincrabAttack");
                }
                else
                {
                    SendForecast(iconE1.forecastName + " crushed Max in its powerful claw.");
                    enemyStrength = (int)(power * 1.5f);
                    GetHurt(enemyStrength);
                    StartCoroutine("E1EndPause");
                }
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " crushed Max in its powerful claw.");
                enemyStrength = (int)(power * 1.5f);
                GetHurt(enemyStrength);
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Camouflage")
        {
            if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " camouflaged itself and grew evasive.");
                EvadeBuff("Enemy1", 3);
                refreshBuff = "evade";
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " camouflaged itself and grew evasive.");
                EvadeBuff("Enemy2", 3);
                refreshBuff = "evade";
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Cure")
        {
            if (unit.name == "Player")
            {
                SendForecast("The Witch's Hat conjured a healing spell.");
                dco.invMagic--;

                GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
                playerHealth += (int)playerMaxHealth / 4;
                if (playerHealth > playerMaxHealth)
                {
                    playerHealth = playerMaxHealth;
                }
                if (playerHealth > (playerMaxHealth / 3.0f))
                {
                    isWeak = false;
                    animPlayer.SetBool("isWeak", isWeak);
                }
                tempNum.GetComponent<DamageText>().SetDamage((int)playerMaxHealth / 4);
                UpdateBars();
                
                StartCoroutine("PlayerEndPause");
            }
            else if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " cast a healing spell.");
                StartCoroutine("E1Cure");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " cast a healing spell.");
                StartCoroutine("E2Cure");
            }
        }
        else if (act == "Hasten")
        {
            if (unit.name == "Player")
            {
                SendForecast("The Witch's Hat conjured a speed spell.");
                dco.invMagic--;
                SpeedBuff("Player", 3);
                refreshBuff = "speed";
                StartCoroutine("PlayerEndPause");
            }
            else if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " cast a speed spell.");
                SpeedBuff("Enemy1", 3);
                refreshBuff = "speed";
                if (enemy2.gameObject.activeSelf)
                {
                    SpeedBuff("Enemy2", 3);
                }
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " cast a speed spell.");
                SpeedBuff("Enemy2", 3);
                refreshBuff = "speed";
                if (enemy1.gameObject.activeSelf)
                {
                    SpeedBuff("Enemy1", 3);
                }
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Ward")
        {
            if (unit.name == "Player")
            {
                SendForecast("The Witch's Hat conjured a warding spell.");
                dco.invMagic--;
                DefenseBuff("Player", 3);
                refreshBuff = "def";
                StartCoroutine("PlayerEndPause");
            }
            else if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " cast a warding spell.");
                StartCoroutine("E1Ward");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " cast a warding spell.");
                StartCoroutine("E2Ward");
            }
        }
        else if (act == "Glare")
        {
            if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " glared at Max with malice.");
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " glared at Max with malice.");
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Dispose")
        {
            if (unit.name == "Player")
            {
                dco.invMagic--;
                int rand = Random.Range(0, 10);
                if (rand <= 2)
                {
                    SendForecast("The Hat conjured a death spell... It worked!");
                    int tar = Random.Range(0, 2);
                    if (tar == 0)
                    {
                        iconE1.GetHurt(666);
                    }
                    else
                    {
                        iconE2.GetHurt(666);
                    }
                }
                else
                {
                    SendForecast("The Hat conjured a death spell... It fizzled.");
                }
                StartCoroutine("PlayerEndPause");
            }
            else if (unit.name == "Enemy1")
            {
                int rand = Random.Range(0, 10);
                if (rand <= 2)
                {
                    SendForecast(iconE1.forecastName + " cast a death spell... It worked!");
                    GetHurt(666);
                }
                else
                {
                    SendForecast(iconE1.forecastName + " cast a death spell... It fizzled.");
                }
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                int rand = Random.Range(0, 10);
                if (rand <= 2)
                {
                    SendForecast(iconE2.forecastName + " cast a death spell... It worked!");
                    GetHurt(666);
                }
                else
                {
                    SendForecast(iconE2.forecastName + " cast a death spell... It fizzled.");
                }
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Fireball")
        {
            if (unit.name == "Player")
            {
                dco.invMagic--;
                int damage = 5 + (dco.playerPow / 2);
                if (isOverdrive)
                {
                    damage *= 2;
                }
                if (turnsPowPlayer > 0)
                {
                    float temp = damage;
                    temp *= 1.5f;
                    damage = (int)temp;
                }
                int tar = Random.Range(0, 2);
                if (tar == 0)
                {
                    SendForecast("The Witch's Hat conjured a fireball at " + iconE1.forecastName + ".");
                    iconE1.GetHurt(damage);
                }
                else
                {
                    SendForecast("The Witch's Hat conjured a fireball at " + iconE2.forecastName + ".");
                    iconE2.GetHurt(damage);
                }
                StartCoroutine("PlayerEndPause");
            }
            else if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " cast a fireball at Max.");
                enemyStrength = power;
                GetHurt(enemyStrength);
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " cast a fireball at Max.");
                enemyStrength = power;
                GetHurt(enemyStrength);
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "Frostball")
        {
            if (unit.name == "Player")
            {
                dco.invMagic--;
                int damage = 10 + (dco.playerPow / 2);
                if (isOverdrive)
                {
                    damage *= 2;
                }
                if (turnsPowPlayer > 0)
                {
                    float temp = damage;
                    temp *= 1.5f;
                    damage = (int)temp;
                }
                int tar = Random.Range(0, 2);
                if (tar == 0)
                {
                    SendForecast("The Witch's Hat conjured a frostball at " + iconE1.forecastName + ".");
                    iconE1.GetHurt(damage);
                }
                else
                {
                    SendForecast("The Witch's Hat conjured a frostball at " + iconE2.forecastName + ".");
                    iconE2.GetHurt(damage);
                }
                StartCoroutine("PlayerEndPause");
            }
            else if (unit.name == "Enemy1")
            {
                SendForecast(iconE1.forecastName + " cast a frostball at Max.");
                enemyStrength = power * 2;
                GetHurt(enemyStrength);
                StartCoroutine("E1EndPause");
            }
            else if (unit.name == "Enemy2")
            {
                SendForecast(iconE2.forecastName + " cast a frostball at Max.");
                enemyStrength = power * 2;
                GetHurt(enemyStrength);
                StartCoroutine("E2EndPause");
            }
        }
        else if (act == "MeatS")
        {
            SendForecast("Max restored 15 HP and 10 DP from the Crab Meat S.");
            animPlayer.SetTrigger("useItem");
            if (dco.enemyType != "Lincrab")
            {
                dco.invMeatS--;
            }
            else
            {
                iconE1.ChangeMoney(5);
                iconE1.UpdateMeter();
            }

            GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
            playerHealth += 15;
            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
            playerDrive += 10;
            if (playerDrive > playerMaxDrive)
            {
                playerDrive = playerMaxDrive;
            }
            if (!dco.overdriveLocked)
            {
                panelDisable.SetActive(false);
            }
            if (playerHealth > (playerMaxHealth / 3.0f))
            {
                isWeak = false;
                animPlayer.SetBool("isWeak", isWeak);
            }
            tempNum.GetComponent<DamageText>().SetDamage(15);
            UpdateBars();
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "MeatM")
        {
            SendForecast("Max restored 30 HP and 30 DP from the Crab Meat M.");
            animPlayer.SetTrigger("useItem");
            if (dco.enemyType != "Lincrab")
            {
                dco.invMeatM--;
            }
            else
            {
                iconE1.ChangeMoney(10);
                iconE1.UpdateMeter();
            }

            GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
            playerHealth += 30;
            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
            playerDrive += 30;
            if (playerDrive > playerMaxDrive)
            {
                playerDrive = playerMaxDrive;
            }
            if (!dco.overdriveLocked)
            {
                panelDisable.SetActive(false);
            }
            if (playerHealth > (playerMaxHealth / 3.0f))
            {
                isWeak = false;
                animPlayer.SetBool("isWeak", isWeak);
            }
            tempNum.GetComponent<DamageText>().SetDamage(30);
            UpdateBars();
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "MeatL")
        {
            SendForecast("Max restored 50 HP and 50 DP from the Crab Meat L.");
            animPlayer.SetTrigger("useItem");
            if (dco.enemyType != "Lincrab")
            {
                dco.invMeatL--;
            }
            else
            {
                iconE1.ChangeMoney(20);
                iconE1.UpdateMeter();
            }

            GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
            playerHealth += 50;
            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
            playerDrive += 50;
            if (playerDrive > playerMaxDrive)
            {
                playerDrive = playerMaxDrive;
            }
            if (!dco.overdriveLocked)
            {
                panelDisable.SetActive(false);
            }
            if (playerHealth > (playerMaxHealth / 3.0f))
            {
                isWeak = false;
                animPlayer.SetBool("isWeak", isWeak);
            }
            tempNum.GetComponent<DamageText>().SetDamage(50);
            UpdateBars();
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "MeatXL")
        {
            SendForecast("Max fully restored HP and DP from the Sandwich.");
            animPlayer.SetTrigger("useItem");
            if (dco.enemyType != "Lincrab")
            {
                dco.invMeatXL--;
            }
            else
            {
                iconE1.ChangeMoney(50);
                iconE1.UpdateMeter();
            }

            GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
            playerHealth = playerMaxHealth;
            playerDrive = playerMaxDrive;
            if (!dco.overdriveLocked)
            {
                panelDisable.SetActive(false);
            }
            isWeak = false;
            animPlayer.SetBool("isWeak", isWeak);
            tempNum.GetComponent<DamageText>().SetDamage((int)playerMaxHealth);
            UpdateBars();
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "Pow")
        {
            SendForecast("Max grew empowered from the Broken Claw.");
            animPlayer.SetTrigger("useItem");
            if (dco.enemyType != "Lincrab")
            {
                dco.invPow--;
            }
            else
            {
                iconE1.ChangeMoney(30);
                iconE1.UpdateMeter();
            }
            AttackBuff("Player", 3);
            refreshBuff = "pow";
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "Def")
        {
            SendForecast("Max grew healthy and armored from the Robo-Crab Meat.");
            animPlayer.SetTrigger("useItem");
            if (dco.enemyType != "Lincrab")
            {
                dco.invDef--;
            }
            else
            {
                iconE1.ChangeMoney(40);
                iconE1.UpdateMeter();
            }
            DefenseBuff("Player", 3);
            refreshBuff = "def";

            GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
            playerHealth += 30;
            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
            if (!dco.overdriveLocked)
            {
                panelDisable.SetActive(false);
                playerDrive += 30;
                if (playerDrive > playerMaxDrive)
                {
                    playerDrive = playerMaxDrive;
                }
            }
            if (playerHealth > (playerMaxHealth / 3.0f))
            {
                isWeak = false;
                animPlayer.SetBool("isWeak", isWeak);
            }
            tempNum.GetComponent<DamageText>().SetDamage(30);
            UpdateBars();
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "Speed")
        {
            SendForecast("Max grew hastened from the Crabbit Ears.");
            animPlayer.SetTrigger("useItem");
            if (dco.enemyType != "Lincrab")
            {
                dco.invSpeed--;
            }
            else
            {
                iconE1.ChangeMoney(30);
                iconE1.UpdateMeter();
            }
            SpeedBuff("Player", 3);
            refreshBuff = "speed";
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "Speed2")
        {
            SendForecast("Max grew healthy and hastened from the Crab Roll.");
            animPlayer.SetTrigger("useItem");
            if (dco.enemyType != "Lincrab")
            {
                dco.invSpeed2--;
            }
            else
            {
                iconE1.ChangeMoney(70);
                iconE1.UpdateMeter();
            }
            SpeedBuff("Player", 3);
            refreshBuff = "speed";

            GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
            playerHealth += 50;
            if (playerHealth > playerMaxHealth)
            {
                playerHealth = playerMaxHealth;
            }
            if (!dco.overdriveLocked)
            {
                panelDisable.SetActive(false);
                playerDrive += 50;
                if (playerDrive > playerMaxDrive)
                {
                    playerDrive = playerMaxDrive;
                }
            }
            if (playerHealth > (playerMaxHealth / 3.0f))
            {
                isWeak = false;
                animPlayer.SetBool("isWeak", isWeak);
            }
            tempNum.GetComponent<DamageText>().SetDamage(50);
            UpdateBars();
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "Cheat")
        {
            SendForecast("Max felt a rush of energy from the Crab Puff Supreme.");
            animPlayer.SetTrigger("useItem");
            playerDrive = playerMaxDrive;
            if (!dco.overdriveLocked)
            {
                panelDisable.SetActive(false);
            }
            if (!isCheating)
            {
                isCheating = true;
            }
            UpdateBars();
            StartCoroutine("PlayerEndPause");
        } //Only usable by player
        else if (act == "Special1")
        {
            if (unit.name == "Enemy1")
            {
                if (iconE1.forecastName == "Crabsolute Zero")
                {
                    SendForecast("Crabsolute Zero launched one of his claws skyward.");
                    StartCoroutine("ZeroSpecial1");
                }
                else if (iconE1.forecastName == "Crabtain Clawdius")
                {
                    if (numEnemies == 2 && iconE2.forecastName == "Cannon")
                    {
                        SendForecast("Crabtain Clawdius took aim with the cannon...");
                        power *= 1.5f;
                        if (turnsPowE1 > 0)
                        {
                            power *= 1.5f;
                        }
                        delayedDamage = (int)power;
                        StartCoroutine("ClawdiusSpecial1");
                    }
                    else if (numEnemies == 1)
                    {
                        SendForecast("Crabtain Clawdius issued a call to claws... Crab reported.");
                        StartCoroutine("ClawdiusCall1");
                    }
                    else if (numEnemies == 2 && iconE2.forecastName == "Crab")
                    {
                        SendForecast("Crabtain Clawdius tossed a morsel to Crab to restore HP.");
                        StartCoroutine("ClawdiusCall2");
                    }
                }
                else if (iconE1.forecastName == "Crustacé Moustache")
                {
                    if (!bossBroken)
                    {
                        proteccJimmy = true;
                        SendForecast("Crustacé Moustache launched Jimmy needlessly hard at Max.");
                        power *= 2.5f;
                        if (turnsPowE1 > 0)
                        {
                            power *= 1.5f;
                        }
                        delayedDamage = (int)power;
                        StartCoroutine("MoustacheSpecial1");
                    }
                    else
                    {
                        SendForecast("Crustacé Moustache couldn't collect his thoughts...");
                        StartCoroutine("E1EndPause");
                    }
                }
                else if (iconE1.forecastName == "Shovel Crab")
                {
                    SendForecast("Shovel Crab conjures the powers of the ancients...");
                    StartCoroutine("ShovelSpecial1");
                }
                else if (iconE1.forecastName == "Crabraham Lincrab")
                {
                    if (enemy2.gameObject.activeSelf)
                    {
                        //Acts as wasted turn if already has ally
                        SendForecast("Crabraham Lincrab carefully adjusted his top hat.");
                        StartCoroutine("E1EndPause");
                    }
                    else
                    {
                        int allyRoll = Random.Range(0, 4);

                        //25% chance summon a Holy Crab
                        if (allyRoll == 0)
                        {
                            SendForecast("Crabraham Lincrab issued a call to claws... Holy Crab arrived!");
                            lincrabAlly = "Holy Crab";
                        }
                        //25% chance summon a Crab Witch
                        else if (allyRoll == 1)
                        {
                            SendForecast("Crabraham Lincrab issued a call to claws... Crab Witch arrived!");
                            lincrabAlly = "Crab Witch";
                        }
                        //50% chance summon a Crabbit
                        else
                        {
                            SendForecast("Crabraham Lincrab issued a call to claws... Crabbit arrived!");
                            lincrabAlly = "Crabbit";
                        }
                        StartCoroutine("LincrabSpecial1");
                    }
                }
            }
            else if (unit.name == "Enemy2")
            {
                if (iconE2.forecastName == "Jimmy")
                {
                    int phrase = Random.Range(0, 6);
                    if (phrase == 0)
                    {
                        SendForecast("Jimmy hopes Max isn't looking at him.");
                    }
                    else if (phrase == 1)
                    {
                        SendForecast("Jimmy contemplates the oil pit's layout.");
                    }
                    else if (phrase == 2)
                    {
                        SendForecast("Jimmy is busy being Jimmy.");
                    }
                    else if (phrase == 3)
                    {
                        SendForecast("Jimmy counts the minutes until his shift ends.");
                    }
                    else if (phrase == 4)
                    {
                        SendForecast("Jimmy does his best to look busy.");
                    }
                    else if (phrase == 5)
                    {
                        SendForecast("Jimmy proofreads quizzes with a sigh.");
                    }
                    StartCoroutine("JimmySpecial1");
                }
            }
        }
        else if (act == "Special2")
        {
            if (unit.name == "Enemy1")
            {
                if (iconE1.forecastName == "Crabsolute Zero")
                {
                    SendForecast("Crabsolute Zero tackled Max at full throtle.");
                    //Deals more damage than basic attack
                    power *= 1.5f;
                    if (turnsPowE1 > 0)
                    {
                        power *= 1.5f;
                    }
                    delayedDamage = (int)power;
                    StartCoroutine("ZeroSpecial2");
                }
                else if (iconE1.forecastName == "Crabtain Clawdius")
                {
                    SendForecast("Crabtain Clawdius indulged in a snack to restore 75 HP.");
                    numBossInterrupt = 0;
                    StartCoroutine("ClawdiusSpecial2");
                }
                else if (iconE1.forecastName == "Crustacé Moustache")
                {
                    if (!bossBroken)
                    {
                        if (moustacheQuiz == -1)
                        {
                            SendForecast("\"Hmm, not bad, human... Not bad...\"");
                            bool temp = false;
                            animE1.SetBool("isEnraged", temp);
                            StartCoroutine("E1EndPause");
                        }
                        else
                        {
                            SendForecast("\"WRONG, FOOLISH HUMAN! INTO THE PIT!\"");
                            StartCoroutine("MoustacheSpecial2");
                        }
                    }
                    else
                    {
                        SendForecast("Crustacé Moustache couldn't collect his thoughts...");
                        StartCoroutine("E1EndPause");
                    }
                }
                else if (iconE1.forecastName == "Crabraham Lincrab")
                {
                    if (iconE1.health <= (iconE1.maxHealth / 2))
                    {
                        SendForecast("Crabsolute Zero buys and uses a Crab Meat M from his store.");
                        lincrabAlly = "MeatM";
                    }
                    else if (turnsSpeedE1 <= 0)
                    {
                        SendForecast("Crabsolute Zero buys and uses a Crabbit Ear from his store.");
                        lincrabAlly = "Speed";
                    }
                    else if (turnsPowE1 <= 0)
                    {
                        SendForecast("Crabsolute Zero buys and uses a Broken Claw from his store.");
                        lincrabAlly = "Pow";
                    }
                    else
                    {
                        SendForecast("Crabsolute Zero buys and uses a Crab Meat S from his store.");
                        lincrabAlly = "MeatS";
                    }
                    StartCoroutine("LincrabSpecial2");
                }
            }
            else if (unit.name == "Enemy2")
            {
                if (iconE2.forecastName == "Jimmy")
                {
                    SendForecast("Jimmy munches a crab apple on his lunch break.");
                    StartCoroutine("JimmySpecial2");
                }
            }
        }
        else if (act == "Special3")
        {
            if (unit.name == "Enemy1")
            {
                if (iconE1.forecastName == "Crabraham Lincrab")
                {
                    SendForecast("Crabraham Lincrab released a shockwave of pure crabnergy!");
                    //Deals more damage than basic attack
                    power *= 1.5f;
                    if (turnsPowE1 > 0)
                    {
                        power *= 1.5f;
                    }
                    delayedDamage = (int)power;
                    StartCoroutine("LincrabSpecial3");
                }
            }
        }
        else if (act == "DevKill")
        {
            SendForecast("Max gained supernatural power.");
            delayedDamage = 1000;
            StartCoroutine("PlayerJump");
        }
    }

    //Applies an attack buff to someone
    public void AttackBuff(string forWho, int turns)
    {
        pendingTut6 = true;
        if (forWho == "Player")
        {
            if (dco.playerPow >= 10)
            {
                turns++;
            }
            turnsPowPlayer = turns;
            auraPlayer.StartGenerate("pow");
        }
        else if (forWho == "Enemy1")
        {
            turnsPowE1 = turns;
            auraE1.StartGenerate("pow");
        }
        else if (forWho == "Enemy2")
        {
            turnsPowE2 = turns;
            auraE2.StartGenerate("pow");
        }
    }

    //Applies a defense buff to someone
    public void DefenseBuff(string forWho, int turns)
    {
        pendingTut6 = true;
        if (forWho == "Player")
        {
            if (dco.playerDef >= 10)
            {
                turns++;
            }
            turnsDefPlayer = turns;
            auraPlayer.StartGenerate("def");
        }
        else if (forWho == "Enemy1")
        {
            turnsDefE1 = turns;
            auraE1.StartGenerate("def");
        }
        else if (forWho == "Enemy2")
        {
            turnsDefE2 = turns;
            auraE2.StartGenerate("def");
        }
    }

    //Applies a speed buff to someone
    public void SpeedBuff(string forWho, int turns)
    {
        pendingTut6 = true;
        if (forWho == "Player")
        {
            if (dco.playerSpeed >= 10)
            {
                turns++;
            }
            if (turnsSpeedPlayer == 0)
            {
                iconPlayer.speed *= 1.5f;
            }
            turnsSpeedPlayer = turns;
            auraPlayer.StartGenerate("speed");
        }
        else if (forWho == "Enemy1")
        {
            if (turnsSpeedE1 == 0)
            {
                iconE1.speed *= 1.5f;
            }
            turnsSpeedE1 = turns;
            auraE1.StartGenerate("speed");
        }
        else if (forWho == "Enemy2")
        {
            if (turnsSpeedE2 == 0)
            {
                iconE2.speed *= 1.5f;
            }
            turnsSpeedE2 = turns;
            auraE2.StartGenerate("speed");
        }
    }

    //Applies an evasion buff to someone
    public void EvadeBuff(string forWho, int turns)
    {
        pendingTut6 = true;
        if (forWho == "Player")
        {
            if (dco.playerSpeed >= 10)
            {
                turns++;
            }
            turnsEvadePlayer = turns;
            auraPlayer.StartGenerate("evade");
        }
        else if (forWho == "Enemy1")
        {
            turnsEvadeE1 = turns;
            auraE1.StartGenerate("evade");
        }
        else if (forWho == "Enemy2")
        {
            turnsEvadeE2 = turns;
            auraE2.StartGenerate("evade");
        }
    }

    //Decreases all buff turns by 1 and removes buff if reaches 0; doesn't subtract if buff gained on Act this turn
    private void CountDownBuffs(string forWho)
    {
        if (forWho == "Player")
        {
            if (turnsPowPlayer > 0 && refreshBuff != "pow")
            {
                turnsPowPlayer--;
                if (turnsPowPlayer == 0)
                {
                    auraPlayer.StopGenerate("pow");
                }
            }
            if (turnsDefPlayer > 0 && refreshBuff != "def")
            {
                turnsDefPlayer--;
                if (turnsDefPlayer == 0)
                {
                    auraPlayer.StopGenerate("def");
                }
            }
            if (turnsSpeedPlayer > 0 && refreshBuff != "speed")
            {
                turnsSpeedPlayer--;
                if (turnsSpeedPlayer == 0)
                {
                    iconPlayer.speed /= 1.5f;
                    auraPlayer.StopGenerate("speed");
                }
            }
            if (turnsEvadePlayer > 0 && refreshBuff != "evade")
            {
                turnsEvadePlayer--;
                if (turnsEvadePlayer == 0)
                {
                    auraPlayer.StopGenerate("evade");
                }
            }
        }
        else if (forWho == "Enemy1")
        {
            if (turnsPowE1 > 0 && refreshBuff != "pow")
            {
                turnsPowE1--;
                if (turnsPowE1 == 0)
                {
                    auraE1.StopGenerate("pow");
                }
            }
            if (turnsDefE1 > 0 && refreshBuff != "def")
            {
                turnsDefE1--;
                if (turnsDefE1 == 0)
                {
                    auraE1.StopGenerate("def");
                }
            }
            if (turnsSpeedE1 > 0 && refreshBuff != "speed")
            {
                turnsSpeedE1--;
                if (turnsSpeedE1 == 0)
                {
                    iconE1.speed /= 1.5f;
                    auraE1.StopGenerate("speed");
                }
            }
            if (turnsEvadeE1 > 0 && refreshBuff != "evade")
            {
                turnsEvadeE1--;
                if (turnsEvadeE1 == 0)
                {
                    auraE1.StopGenerate("evade");
                }
            }
        }
        else if (forWho == "Enemy2")
        {
            if (turnsPowE2 > 0 && refreshBuff != "pow")
            {
                turnsPowE2--;
                if (turnsPowE2 == 0)
                {
                    auraE2.StopGenerate("pow");
                }
            }
            if (turnsDefE2 > 0 && refreshBuff != "def")
            {
                turnsDefE2--;
                if (turnsDefE2 == 0)
                {
                    auraE2.StopGenerate("def");
                }
            }
            if (turnsSpeedE2 > 0 && refreshBuff != "speed")
            {
                turnsSpeedE2--;
                if (turnsSpeedE2 == 0)
                {
                    iconE2.speed /= 1.5f;
                    auraE2.StopGenerate("speed");
                }
            }
            if (turnsEvadeE2 > 0 && refreshBuff != "evade")
            {
                turnsEvadeE2--;
                if (turnsEvadeE2 == 0)
                {
                    auraE2.StopGenerate("evade");
                }
            }
        }
        refreshBuff = "";
    }

    public void DoWin()
    {
        if (dco.enemyType == "Zero")
        {
            dco.beatZero = true;
            isCheating = true;
            dco.playerDrive = 100;
        }
        else if (dco.enemyType == "Clawdius")
        {
            dco.beatClawdius = true;
        }
        else if (dco.enemyType == "Moustache")
        {
            dco.beatMoustache = true;
        }
        else if (dco.enemyType == "Shovel")
        {
            dco.beatShovel = true;
        }
        else if (dco.enemyType == "Lincrab")
        {
            dco.beatLincrab = true;
        }
        //Leaving this here for now, but MAYBE unnecessary?
        if (dco.enemyType == "Zero" || dco.enemyType == "Clawdius" || dco.enemyType == "Moustache" || dco.enemyType == "Shovel" || dco.enemyType == "Lincrab")
        {
            bossBeaten = true;
        }
        if (dco.enemyType == "Shovel" || dco.enemyType == "Lincrab")
        {
            dco.bossAidHP = 1;
        }
        else
        {
            dco.bossAidHP = 0;
        }
        //End of MAYBE removable section
        sm.musicPlayer.clip = sm.musicVictory;
        sm.musicPlayer.Play();
        turnsPowPlayer = 0;
        turnsDefPlayer = 0;
        turnsSpeedPlayer = 0;
        turnsEvadePlayer = 0;
        auraPlayer.StopGenerate("all");
        freezeTimeline = true;
        panelMain.SetActive(false);
        panelTarget.SetActive(false);
        panelPlayerDat.SetActive(false);
        panelTimeline.SetActive(false);
        StartCoroutine("WinScreen");
        dco.returningBoss = false;
        dco.enemyDead[dco.encounterID] = true;
    }

    private void DoLose()
    {
        sm.musicPlayer.clip = sm.musicDefeat;
        sm.musicPlayer.Play();
        turnsPowPlayer = 0;
        turnsDefPlayer = 0;
        turnsSpeedPlayer = 0;
        turnsEvadePlayer = 0;
        auraPlayer.StopGenerate("all");
        freezeTimeline = true;
        panelMain.SetActive(false);
        panelTarget.SetActive(false);
        panelPlayerDat.SetActive(false);
        panelTimeline.SetActive(false);
        StartCoroutine("LoseScreen");

        //For testing purposes, health will reset to max if loading back into the level after defeat
        playerHealth = playerMaxHealth;
    }

    //Only called during boss fights; so music isn't touched
    private void DoPushOut()
    {
        turnsPowPlayer = 0;
        turnsDefPlayer = 0;
        turnsSpeedPlayer = 0;
        turnsEvadePlayer = 0;
        auraPlayer.StopGenerate("all");
        freezeTimeline = true;
        panelMain.SetActive(false);
        panelTarget.SetActive(false);
        panelPlayerDat.SetActive(false);
        panelTimeline.SetActive(false);
        StartCoroutine("PushedScreen");
    }

    public void LeaveBattleScene()
    {
        StartCoroutine(Leave());

        dco.playerHealth = playerHealth;
        dco.playerDrive = playerDrive;
        dco.inDrive = isOverdrive;
        if ((dco.enemyType == "Zero" || dco.enemyType == "Clawdius" || dco.enemyType == "Moustache" || dco.enemyType == "Lincrab") && !bossBeaten)
        {
            dco.bossHP = (int)iconE1.health;
            dco.bossAidHP = (int)iconE2.health;
            dco.bossMoney = (int)iconE1.money;
            dco.returningBoss = true;
        }
        else if (dco.enemyType == "Shovel" || dco.enemyType == "Lincrab")
        {
            dco.bossAidHP = 1;
        }
        else
        {
            dco.bossHP = 0;
            dco.bossAidHP = 0;
            //dco.returningBoss = false;
        }
    }

    //Moves the camera to a specified position
    private void CameraJump(string target)
    {
        if (target == "player" || target == "enemy1" || target == "enemy2")
        {
            camStart = transform.position;
            camTarget = target;
            camTimer = 0.0f;
            camJumping = true;
        }
        else if (target == "playerD")
        {
            camStart = transform.position;
            camEnd = new Vector3(defaultPlayerPos.x, defaultPlayerPos.y + 0.05f, defaultPlayerPos.z - 4.0f);
            camTarget = "playerD";
            camTimer = 0.0f;
            camJumping = true;
        }
        else
        {
            camStart = transform.position;
            camEnd = defaultCamPos;
            camTarget = "default";
            camTimer = 0.0f;
            camJumping = true;
        }
    }

    //Moves iconPlayer to the start and counts down its active buffs
    private void EndPlayerTurn()
    {
        iconPlayer.transform.localPosition = new Vector3(850.0f, transform.localPosition.y, transform.localPosition.z);
        CountDownBuffs("Player");
        freezeTimeline = false;
        if (playerDrive > 0 && !dco.overdriveLocked)
        {
            panelDisable.SetActive(false);
        }
        if (clawdiusWarning)
        {
            clawdiusWarning = false;
            StartCoroutine("ClawdiusWarning");
        }
        else
        {
            CheckForTutorials();
        }
    }

    //Moves iconPlayer to the start and counts down its active buffs
    private void EndE1Turn()
    {
        iconE1.transform.localPosition = new Vector3(850.0f, transform.localPosition.y, transform.localPosition.z);
        CountDownBuffs("Enemy1");
        proteccJimmy = false;

        if (dco.enemyType == "Lincrab" && iconE1.money <= 0 && !bossBroken)
        {
            SendForecast("With his coffers emptied, Crabraham Lincrab flew into a frenzy!");
            bossBroken = true;
        }
        else if (dco.enemyType == "Lincrab" && iconE1.money > 0 && bossBroken)
        {
            SendForecast("With his coffers replenished, Crabraham Lincrab calmed down.");
            bossBroken = false;
        }

        if (turnsEnragedE1 > 0)
        {
            turnsEnragedE1--;
            if (turnsEnragedE1 == 0)
            {
                iconE1.speed /= 2;
                SendForecast(iconE1.forecastName + "' rage finally subsided.");
                bossEnraged = false;
                animE1.SetBool("isEnraged", bossEnraged);
            }
        }
        freezeTimeline = false;
        if (playerDrive > 0 && !dco.overdriveLocked)
        {
            panelDisable.SetActive(false);
        }
        CheckForTutorials();
    }

    //Moves iconPlayer to the start and counts down its active buffs
    private void EndE2Turn()
    {
        iconE2.transform.localPosition = new Vector3(850.0f, transform.localPosition.y, transform.localPosition.z);
        CountDownBuffs("Enemy2");
        freezeTimeline = false;
        if (playerDrive > 0 && !dco.overdriveLocked)
        {
            panelDisable.SetActive(false);
        }
        CheckForTutorials();
    }

    public void MoustacheLinger()
    {
        bool temp = true;
        animE1.SetBool("isEnraged", temp);
        StartCoroutine("MoustacheAsks");
    }

    //Deals 500 damage to all foes (dev tool)
    public void DevKill()
    {
        if ((!freezeTimeline || isPlayersTurn) && !battleOver)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            panelMain.SetActive(false);
            panelAbility.SetActive(false);
            panelItem.SetActive(false);
            panelIRecover.SetActive(false);
            panelIStatus.SetActive(false);
            panelIOther.SetActive(false);
            iconPlayer.SetCastSpeed("instant");
            iconPlayer.ChooseAction("DevKill");
            isPlayersTurn = false;
            freezeTimeline = false;
            devMode = false;

        }
    }

    //Forces the boss to use their "push out" move
    public void DevPushOut()
    {
        if (!freezeTimeline && !battleOver && (dco.enemyType == "Zero" || dco.enemyType == "Clawdius" || dco.enemyType == "Moustache" || dco.enemyType == "Lincrab"))
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            panelMain.SetActive(false);
            panelAbility.SetActive(false);
            panelItem.SetActive(false);
            panelIRecover.SetActive(false);
            panelIStatus.SetActive(false);
            panelIOther.SetActive(false);
            iconE1.SetCastSpeed("instant");
            if (dco.enemyType == "Zero" || dco.enemyType == "Moustache")
            {
                iconE1.ChooseAction("Special2");
            }
            else if (dco.enemyType == "Clawdius")
            {
                iconE1.ChooseAction("Special1");
            }
            else
            {
                iconE1.ChooseAction("Special3");
            }
            isPlayersTurn = false;
            freezeTimeline = false;
            devMode = false;

        }
    }

    //Spawns the other crab types that run across the screen;
    public void DevFun()
    {
        StartCoroutine("DevFunTime");
    }

    IEnumerator Leave()
    {
        sceneTrans.SetTrigger("Start");

        yield return new WaitForSeconds(1.0f);

        if (dco.enemyType != "Zero" && dco.enemyType != "Clawdius" && dco.enemyType != "Moustache" && dco.enemyType != "Shovel" && dco.enemyType != "Lincrab")
        {
            sm.musicPlayer.clip = sm.musicLevel;
            sm.musicPlayer.Play();
        }
        else if ((dco.enemyType == "Zero" || dco.enemyType == "Clawdius" || dco.enemyType == "Moustache" || dco.enemyType == "Shovel" || dco.enemyType == "Lincrab") && bossBeaten)
        {
            sm.musicPlayer.Stop();
        }
        SceneManager.LoadScene(dco.sceneName);
    }
    
    IEnumerator PlayerDash()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        CameraJump("playerD");
        freezeTimeline = true;
        animPlayer.SetTrigger("doDash");
        yield return new WaitForSeconds(0.25f);
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        yield return new WaitForSeconds(0.25f);
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        yield return new WaitForSeconds(0.5f);
        sm.sfxPlayer.PlayOneShot(sm.soundKaching);
        yield return new WaitForSeconds(0.167f);
        CameraJump("default");
        SpeedBuff("Player", 3);
        refreshBuff = "speed";
        EndPlayerTurn();
    }
    
    IEnumerator PlayerAir()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        CameraJump("playerD");
        freezeTimeline = true;
        animPlayer.SetTrigger("doAir");
        yield return new WaitForSeconds(0.25f);
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        yield return new WaitForSeconds(0.25f);
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        yield return new WaitForSeconds(0.5f);
        sm.sfxPlayer.PlayOneShot(sm.soundKaching);
        yield return new WaitForSeconds(0.167f);
        CameraJump("default");
        EvadeBuff("Player", 3);
        refreshBuff = "evade";
        EndPlayerTurn();
    }

    IEnumerator PlayerJump()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundPJump);
        freezeTimeline = true;
        animPlayer.SetTrigger("doJump");
        yield return new WaitForSeconds(0.7f);
        sm.sfxPlayer.PlayOneShot(sm.soundPLand);
        if (isOverdrive)
        {
            Instantiate(overBurst, new Vector3(player.position.x, player.position.y - 0.58f, player.position.z), overBurst.transform.rotation);
        }
        else
        {
            Instantiate(maxBurst, new Vector3(player.position.x, player.position.y - 0.58f, player.position.z), overBurst.transform.rotation);
        }
        if (enemy1.gameObject.activeSelf)
        {
            iconE1.GetHurt(delayedDamage);
        }
        if (enemy2.gameObject.activeSelf)
        {
            iconE2.GetHurt(delayedDamage);
        }
        yield return new WaitForSeconds(0.634f);
        EndPlayerTurn();
    }

    IEnumerator E1Cure()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundSpellCharge);
        CameraJump("enemy1");
        freezeTimeline = true;
        animE1.SetTrigger("doCast");
        Instantiate(spellCircle, new Vector3(enemy1.position.x, 0.05f, enemy1.position.z), spellCircle.transform.rotation);

        yield return new WaitForSeconds(1.0f);

        sm.sfxPlayer.PlayOneShot(sm.soundSpellUse);
        iconE1.GetHeal((int)(iconE1.maxHealth / 4.0f));
        if (enemy2.gameObject.activeSelf)
        {
            iconE2.GetHeal((int)(iconE2.maxHealth / 4.0f));
        }
        Instantiate(holyBurst, new Vector3(enemy1.position.x, 0.6f, enemy1.position.z), holyBurst.transform.rotation);

        yield return new WaitForSeconds(0.34f);
        CameraJump("default");
        EndE1Turn();
    }

    IEnumerator E2Cure()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundSpellCharge);
        CameraJump("enemy2");
        freezeTimeline = true;
        animE2.SetTrigger("doCast");
        Instantiate(spellCircle, new Vector3(enemy2.position.x, 0.05f, enemy2.position.z), spellCircle.transform.rotation);

        yield return new WaitForSeconds(1.0f);

        sm.sfxPlayer.PlayOneShot(sm.soundSpellUse);
        iconE2.GetHeal((int)(iconE2.maxHealth / 4.0f));
        if (enemy1.gameObject.activeSelf)
        {
            iconE1.GetHeal((int)(iconE1.maxHealth / 4.0f));
        }
        Instantiate(holyBurst, new Vector3(enemy2.position.x, 0.6f, enemy2.position.z), holyBurst.transform.rotation);

        yield return new WaitForSeconds(0.34f);
        CameraJump("default");
        EndE2Turn();
    }

    IEnumerator E1Ward()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundSpellCharge);
        CameraJump("enemy1");
        freezeTimeline = true;
        animE1.SetTrigger("doCast");
        Instantiate(spellCircle, new Vector3(enemy1.position.x, 0.05f, enemy1.position.z), spellCircle.transform.rotation);

        yield return new WaitForSeconds(1.0f);

        sm.sfxPlayer.PlayOneShot(sm.soundSpellUse);
        DefenseBuff("Enemy1", 3);
        refreshBuff = "def";
        if (enemy2.gameObject.activeSelf)
        {
            DefenseBuff("Enemy2", 3);
        }
        Instantiate(holyBurst, new Vector3(enemy1.position.x, 0.6f, enemy1.position.z), holyBurst.transform.rotation);

        yield return new WaitForSeconds(0.34f);
        CameraJump("default");
        EndE1Turn();
    }

    IEnumerator E2Ward()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundSpellCharge);
        CameraJump("enemy2");
        freezeTimeline = true;
        animE2.SetTrigger("doCast");
        Instantiate(spellCircle, new Vector3(enemy2.position.x, 0.05f, enemy2.position.z), spellCircle.transform.rotation);

        yield return new WaitForSeconds(1.0f);

        sm.sfxPlayer.PlayOneShot(sm.soundSpellUse);
        DefenseBuff("Enemy2", 3);
        refreshBuff = "def";
        if (enemy1.gameObject.activeSelf)
        {
            DefenseBuff("Enemy1", 3);
        }
        Instantiate(holyBurst, new Vector3(enemy2.position.x, 0.6f, enemy2.position.z), holyBurst.transform.rotation);

        yield return new WaitForSeconds(0.34f);
        CameraJump("default");
        EndE2Turn();
    }

    IEnumerator ZeroAttack()
    {
        CameraJump("enemy1");
        freezeTimeline = true;
        animE1.SetTrigger("doAttack");
        yield return new WaitForSeconds(0.43f);
        GetHurt(delayedDamage);
        yield return new WaitForSeconds(0.57f);
        CameraJump("default");
        EndE1Turn();
    }

    IEnumerator ZeroSpecial1()
    {
        CameraJump("enemy1");
        freezeTimeline = true;
        animE1.SetTrigger("doSpecial1");
        yield return new WaitForSeconds(0.5f);
        ec.SummonZClaw();
        sm.sfxPlayer.PlayOneShot(sm.soundLandHeavy);
        bool temp = true;
        animE1.SetBool("isBroken", temp);
        yield return new WaitForSeconds(0.5f);
        CameraJump("default");
        yield return new WaitForSeconds(0.5f);
        EndE1Turn();
    }

    IEnumerator ZeroSpecial2()
    {
        bool doPush = false;
        freezeTimeline = true;
        animE1.SetTrigger("doSpecial2");
        yield return new WaitForSeconds(0.25f);
        GetHurt(delayedDamage);
        if (playerHealth > 0)
        {
            animPlayer.SetTrigger("getPushed");
            doPush = true;
        }
        animE1.SetBool("pushedOut", doPush);
        yield return new WaitForSeconds(0.75f);
        if (doPush)
        {
            DoPushOut();
        }
    }

    IEnumerator ClawdiusAttack()
    {
        CameraJump("enemy1");
        freezeTimeline = true;
        animE1.SetTrigger("doAttack");
        yield return new WaitForSeconds(0.5f);
        GetHurt(delayedDamage);
        yield return new WaitForSeconds(0.5f);
        CameraJump("default");
        EndE1Turn();
    }

    IEnumerator ClawdiusSpecial1()
    {
        bool doPush = false;
        freezeTimeline = true;
        animE1.SetTrigger("doSpecial1");
        animE2.SetTrigger("doSpecial1");
        yield return new WaitForSeconds(0.58f);
        Instantiate(clawdiusProj, new Vector3(3.0f, 1.0f, 0.75f), clawdiusProj.transform.rotation);
        yield return new WaitForSeconds(0.25f);
        GetHurt(delayedDamage);
        if (playerHealth > 0)
        {
            animPlayer.SetTrigger("getPushed");
            doPush = true;
        }
        yield return new WaitForSeconds(0.17f);
        if (doPush)
        {
            DoPushOut();
        }
    }

    IEnumerator ClawdiusSpecial2()
    {
        freezeTimeline = true;
        animE1.SetTrigger("doSpecial2");
        yield return new WaitForSeconds(0.58f);
        iconE1.GetHeal(75);
        yield return new WaitForSeconds(0.42f);
        freezeTimeline = false;
        EndE1Turn();
    }

    IEnumerator ClawdiusCall1()
    {
        freezeTimeline = true;
        animE1.SetTrigger("doCall");
        yield return new WaitForSeconds(0.5f);
        ec.SummonCrab();
        yield return new WaitForSeconds(0.5f);
        freezeTimeline = false;
        EndE1Turn();
    }

    IEnumerator ClawdiusCall2()
    {
        freezeTimeline = true;
        animE1.SetTrigger("doCall");
        yield return new WaitForSeconds(0.5f);
        iconE2.GetHeal(75);
        yield return new WaitForSeconds(0.5f);
        freezeTimeline = false;
        EndE1Turn();
    }

    IEnumerator CannonAttack()
    {
        freezeTimeline = true;
        yield return new WaitForSeconds(0.25f);
        GetHurt(delayedDamage);
        yield return new WaitForSeconds(0.75f);
        freezeTimeline = false;
        EndE2Turn();
    }

    IEnumerator ClawAttack()
    {
        freezeTimeline = true;
        animE2.SetTrigger("doAttack");
        yield return new WaitForSeconds(0.3f);
        animE1.SetTrigger("doRegen");
        yield return new WaitForSeconds(0.06f);
        GetHurt(delayedDamage);
        yield return new WaitForSeconds(0.94f);
        turnsPowE2 = 0;
        turnsDefE2 = 0;
        turnsSpeedE2 = 0;
        turnsEvadeE2 = 0;
        auraE2.StopGenerate("all");
        enemy2.gameObject.SetActive(false);
        iconE2.gameObject.SetActive(false);
        numEnemies--;
        EndE2Turn();
    }

    IEnumerator MoustacheAttack()
    {
        freezeTimeline = true;
        animE1.SetTrigger("doAttack");
        yield return new WaitForSeconds(0.50f);
        Instantiate(clawdiusProj, new Vector3(0.5f, 0.55f, 0.75f), clawdiusProj.transform.rotation);
        yield return new WaitForSeconds(0.25f);
        GetHurt(delayedDamage);
        yield return new WaitForSeconds(0.25f);
        freezeTimeline = false;
        EndE1Turn();
    }

    IEnumerator MoustacheSpecial1()
    {
        freezeTimeline = true;
        animE1.SetTrigger("doSpecial");
        animE2.SetTrigger("doSpecial");
        animClaw.SetTrigger("doSpecial");
        yield return new WaitForSeconds(1.4f);
        GetHurt(delayedDamage);
        yield return new WaitForSeconds(0.6f);
        iconE2.GetHurt(delayedDamage);
        freezeTimeline = false;
        EndE1Turn();
    }

    IEnumerator MoustacheSpecial2()
    {
        freezeTimeline = true;
        animPlayer.SetTrigger("doFall");
        animDoor.SetTrigger("doOpen");
        yield return new WaitForSeconds(1.0f);
        DoPushOut();
    }

    IEnumerator JimmyAttack()
    {
        CameraJump("enemy2");
        freezeTimeline = true;
        animE2.SetTrigger("doAttack");
        yield return new WaitForSeconds(0.62f);
        GetHurt(delayedDamage);
        yield return new WaitForSeconds(0.38f);
        CameraJump("default");
        EndE2Turn();
    }

    IEnumerator JimmySpecial1()
    {
        CameraJump("enemy2");
        freezeTimeline = true;
        animE2.SetBool("isGuarding", freezeTimeline);
        yield return new WaitForSeconds(1.0f);
        freezeTimeline = false;
        animE2.SetBool("isGuarding", freezeTimeline);
        CameraJump("default");
        EndE2Turn();
    }

    IEnumerator JimmySpecial2()
    {
        CameraJump("enemy2");
        freezeTimeline = true;
        animE2.SetBool("isGuarding", freezeTimeline);
        iconE2.GetHeal(10);
        yield return new WaitForSeconds(1.0f);
        freezeTimeline = false;
        animE2.SetBool("isGuarding", freezeTimeline);
        CameraJump("default");
        EndE2Turn();
    }

    IEnumerator ShovelSpecial1()
    {
        CameraJump("enemy1");
        freezeTimeline = true;
        animE1.SetTrigger("doSpecial");
        yield return new WaitForSeconds(2.0f);
        AttackBuff("Enemy1", 3);
        sm.sfxPlayer.PlayOneShot(sm.soundKaching);
        yield return new WaitForSeconds(0.5f);
        DefenseBuff("Enemy1", 3);
        sm.sfxPlayer.PlayOneShot(sm.soundKaching);
        yield return new WaitForSeconds(0.5f);
        SpeedBuff("Enemy1", 3);
        sm.sfxPlayer.PlayOneShot(sm.soundKaching);
        yield return new WaitForSeconds(0.5f);
        EvadeBuff("Enemy1", 3);
        sm.sfxPlayer.PlayOneShot(sm.soundKaching);
        SendForecast("\"You've come far... But this is where your road ends!\"");
        yield return new WaitForSeconds(1.5f);
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        yield return new WaitForSeconds(0.83f);
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        yield return new WaitForSeconds(0.5f);
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        yield return new WaitForSeconds(0.5f);
        sm.sfxPlayer.PlayOneShot(sm.soundDash);
        SendForecast("\"BEGONE, HUMAN!!\"");
        yield return new WaitForSeconds(1.10f);
        sm.sfxPlayer.PlayOneShot(sm.soundLandHeavy);
        CameraJump("default");
        yield return new WaitForSeconds(2.07f);
        sm.sfxPlayer.PlayOneShot(sm.soundHurt);
        iconE1.GetHurt(99999);
        EndE1Turn();
    }

    IEnumerator LincrabAttack()
    {
        CameraJump("enemy1");
        freezeTimeline = true;
        animE1.SetTrigger("doAttack");
        yield return new WaitForSeconds(0.5f);
        GetHurt(delayedDamage);
        yield return new WaitForSeconds(0.5f);
        CameraJump("default");
        EndE1Turn();
    }

    IEnumerator LincrabSpecial1()
    {
        if (lincrabAlly == "Holy Crab")
        {
            CameraJump("enemy1");
            freezeTimeline = true;
            animE1.SetTrigger("doSpecial1");
            yield return new WaitForSeconds(0.5f);
            ec.SummonHoly();
            sm.sfxPlayer.PlayOneShot(sm.soundClang);
            yield return new WaitForSeconds(0.5f);
            CameraJump("default");
            EndE1Turn();
        }
        else if (lincrabAlly == "Crab Witch")
        {
            CameraJump("enemy1");
            freezeTimeline = true;
            animE1.SetTrigger("doSpecial1");
            yield return new WaitForSeconds(0.5f);
            ec.SummonWitch();
            sm.sfxPlayer.PlayOneShot(sm.soundClang);
            yield return new WaitForSeconds(0.5f);
            CameraJump("default");
            EndE1Turn();
        }
        else if (lincrabAlly == "Crabbit")
        {
            CameraJump("enemy1");
            freezeTimeline = true;
            animE1.SetTrigger("doSpecial1");
            yield return new WaitForSeconds(0.5f);
            ec.SummonCrabbit();
            sm.sfxPlayer.PlayOneShot(sm.soundClang);
            yield return new WaitForSeconds(0.5f);
            CameraJump("default");
            EndE1Turn();
        }
    }

    IEnumerator LincrabSpecial2()
    {
        if (lincrabAlly == "MeatS")
        {
            CameraJump("enemy1");
            freezeTimeline = true;
            animE1.SetTrigger("doSpecial2");
            yield return new WaitForSeconds(0.83f);
            iconE1.GetHeal(15);
            sm.sfxPlayer.PlayOneShot(sm.soundItem);
            iconE1.ChangeMoney(-10);
            iconE1.UpdateMeter();
            yield return new WaitForSeconds(0.17f);
            CameraJump("default");
            EndE1Turn();
        }
        if (lincrabAlly == "MeatM")
        {
            CameraJump("enemy1");
            freezeTimeline = true;
            animE1.SetTrigger("doSpecial2");
            yield return new WaitForSeconds(0.83f);
            iconE1.GetHeal(30);
            sm.sfxPlayer.PlayOneShot(sm.soundItem);
            iconE1.ChangeMoney(-20);
            iconE1.UpdateMeter();
            yield return new WaitForSeconds(0.17f);
            CameraJump("default");
            EndE1Turn();
        }
        if (lincrabAlly == "Pow")
        {
            CameraJump("enemy1");
            freezeTimeline = true;
            animE1.SetTrigger("doSpecial2");
            yield return new WaitForSeconds(0.83f);
            AttackBuff("Enemy1", 3);
            refreshBuff = "pow";
            sm.sfxPlayer.PlayOneShot(sm.soundItem);
            iconE1.ChangeMoney(-60);
            iconE1.UpdateMeter();
            yield return new WaitForSeconds(0.17f);
            CameraJump("default");
            EndE1Turn();
        }
        if (lincrabAlly == "Speed")
        {
            CameraJump("enemy1");
            freezeTimeline = true;
            animE1.SetTrigger("doSpecial2");
            yield return new WaitForSeconds(0.83f);
            SpeedBuff("Enemy1", 3);
            refreshBuff = "speed";
            sm.sfxPlayer.PlayOneShot(sm.soundItem);
            iconE1.ChangeMoney(-60);
            iconE1.UpdateMeter();
            yield return new WaitForSeconds(0.17f);
            CameraJump("default");
            EndE1Turn();
        }
    }

    IEnumerator LincrabSpecial3()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundSpellCharge);
        CameraJump("enemy1");
        freezeTimeline = true;
        animE1.SetTrigger("doSpecial3");
        yield return new WaitForSeconds(0.4f);
        CameraJump("default");
        yield return new WaitForSeconds(0.3f);
        sm.sfxPlayer.PlayOneShot(sm.soundSpellUse);
        Instantiate(lincrabBurst, enemy1.position, lincrabBurst.transform.rotation);
        bool doPush = false;
        GetHurt(delayedDamage);
        if (iconE2.gameObject.activeSelf)
        {
            iconE2.GetHurt(delayedDamage);
        }
        if (playerHealth > 0)
        {
            animPlayer.SetTrigger("getPushed");
            doPush = true;
        }
        //animE1.SetBool("pushedOut", doPush);
        if (doPush)
        {
            DoPushOut();
        }
    }

    //Warning message if the player interrupts Clawdius' eating twice consecutively
    IEnumerator ClawdiusWarning()
    {
        CameraJump("enemy1");
        freezeTimeline = true;
        SendForecast("\"Can't ye let a crab eat in peace? Do it again!\"");
        yield return new WaitForSeconds(1.6f);
        CameraJump("default");
        freezeTimeline = false;
        CheckForTutorials();
    }

    //Pauses so that the player gets a couple seconds to read his "quiz"
    IEnumerator MoustacheAsks()
    {
        CameraJump("enemy1");
        freezeTimeline = true;
        yield return new WaitForSeconds(1.6f);
        CameraJump("default");
        freezeTimeline = false;
    }

    //Pre-fight item theft by Crabraham Lincrab
    IEnumerator LincrabSteal()
    {
        freezeTimeline = true;
        yield return new WaitForSeconds(1.0f);
        int numItems = 0;
        numItems += dco.invMeatS;
        numItems += dco.invMeatM;
        numItems += dco.invMeatL;
        numItems += dco.invMeatXL;
        numItems += dco.invPow;
        numItems += dco.invDef;
        numItems += dco.invSpeed;
        numItems += dco.invSpeed2;
        if (numItems > 100)
        {
            numItems = 100;
        }

        dco.invMeatS = 0;
        dco.invMeatM = 0;
        dco.invMeatL = 0;
        dco.invMeatXL = 0;
        dco.invPow = 0;
        dco.invDef = 0;
        dco.invSpeed = 0;
        dco.invSpeed2 = 0;
        
        if (numItems == 0)
        {
            SendForecast("\"No good items? Hah, buy yourself something nice then!\"");
            animE1.SetTrigger("doPity");
            yield return new WaitForSeconds(0.08f);
            sm.sfxPlayer.PlayOneShot(sm.soundItem);
            iconE1.ChangeMoney(-150);
            iconE1.UpdateMeter();
            yield return new WaitForSeconds(1.92f);
        }
        else
        {
            SendForecast("\"Nice collection of items... I'll take your entire stock!\"");
            animE1.SetTrigger("doSteal");
            animPlayer.SetTrigger("isHurting");
            sm.sfxPlayer.PlayOneShot(sm.soundHurt);
            yield return new WaitForSeconds(0.41f);
            iconE1.ChangeMoney(-1 * (50 + numItems));
            iconE1.UpdateMeter();
            yield return new WaitForSeconds(1.59f);
        }
        SendForecast("\"No worries, my store's always open for an old friend...\"");
        yield return new WaitForSeconds(2.0f);
        freezeTimeline = false;
    }

    //Pauses the timeline for 0.6 seconds, then moves iconPlayer to the start and counts down its active buffs (placeholder)
    IEnumerator PlayerEndPause()
    {
        CameraJump("player");
        freezeTimeline = true;
        yield return new WaitForSeconds(0.6f);
        CameraJump("default");
        EndPlayerTurn();
    }
    
    //Pauses the timeline for 0.6 seconds, then moves iconE1 to the start and counts down its active buffs (placeholder)
    IEnumerator E1EndPause()
    {
        CameraJump("enemy1");
        freezeTimeline = true;
        yield return new WaitForSeconds(0.6f);
        CameraJump("default");
        EndE1Turn();
    }

    //Pauses the timeline for 0.6 seconds, then moves iconE2 to the start and counts down its active buffs (placeholder)
    IEnumerator E2EndPause()
    {
        CameraJump("enemy2");
        freezeTimeline = true;
        yield return new WaitForSeconds(0.6f);
        CameraJump("default");
        EndE2Turn();
    }

    //Start() runs too early (IconMove Start() hasn't happened yet), so Unity throws an error if put directly there
    IEnumerator AdvantageDamage()
    {
        yield return new WaitForSeconds(0.1f);
        iconE1.GetHurt(5.0f);
        if (numEnemies == 2)
        {
            iconE2.GetHurt(5.0f);
        }
    }

    //Start() runs too early (IconMove Start() hasn't happened yet), so Unity throws an error if put directly there
    IEnumerator ReturnToBoss()
    {
        yield return new WaitForSeconds(0.01f);
        iconE1.health = dco.bossHP;
        iconE1.money = dco.bossMoney;
        iconE1.RefreshBossEncounter();
        if (dco.enemyType == "Moustache")
        {
            iconE2.health = dco.bossAidHP;
            iconE2.RefreshBossEncounter();
        }
    }

    //Does a thing
    IEnumerator DevFunTime()
    {
        for (int i = 0; i < devModeCrabs.Length; i++)
        {
            yield return new WaitForSeconds(0.2f);
            Instantiate(devModeCrabs[i], new Vector3(0.0f, 1.2f, 0.0f), devModeCrabs[i].transform.rotation);
        }
    }

    //Delays win/lose/escape screens so player can read most recent forecast
    IEnumerator WinScreen()
    {
        yield return new WaitForSeconds(0.5f);
        battleOver = true;
        animPlayer.SetBool("battleOver", battleOver);
        yield return new WaitForSeconds(0.5f);
        panelWin.SetActive(true);
    }
    IEnumerator LoseScreen()
    {
        yield return new WaitForSeconds(0.5f);
        battleOver = true;
        animPlayer.SetBool("battleOver", battleOver);
        yield return new WaitForSeconds(0.5f);
        panelLose.SetActive(true);
    }
    IEnumerator EscapeScreen()
    {
        yield return new WaitForSeconds(1.0f);
        battleOver = true;
        animPlayer.SetBool("battleOver", battleOver);
        panelEscape.SetActive(true);
    }
    IEnumerator PushedScreen()
    {
        yield return new WaitForSeconds(1.0f);
        battleOver = true;
        animPlayer.SetBool("battleOver", battleOver);
        panelPushed.SetActive(true);
    }

    //Series of delayed messages for extra info in battles
    IEnumerator InterruptMessage()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
        tempNum.GetComponent<DamageText>().SetMessage("Interrupted!");
    }
    IEnumerator ArmorMessage()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
        tempNum.GetComponent<DamageText>().SetMessage("Armored!");
    }
    IEnumerator EvadeMessage()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
        tempNum.GetComponent<DamageText>().SetMessage("Evaded!");
    }
    IEnumerator GuardMessage()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject tempNum = Instantiate(damageNum, player.position, player.rotation);
        tempNum.GetComponent<DamageText>().SetMessage("Guarded!");
    }
}
