using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChoose : MonoBehaviour
{
    public GameObject CrabA, CrabB, CrabbitA, CrabbitB, RoboA, RoboB, BackA, BackB, WitchA, WitchB, MimicA, MimicB, HolyA, HolyB, HoleyA, HoleyB, Zero, ZClaw, Clawdius, CCannon, Moustache, MJimmy, Shovel, Lincrab, IconA, IconB, LabelA, LabelB;
    public Sprite iconCrab, iconCrabbit, iconRobo, iconBack, iconWitch, iconMimic, iconHoly, iconHoley, iconZero, iconZClaw, iconClawdius, iconCCannon, iconMoustache, iconMJimmy, iconShovel, iconLincrab;
    public Text targetTextA, targetTextB, letterTextA, letterTextB;
    public TurnBasedManager tbm;
    public int numEnemies;

    private DataCarryOver dco;
    private SoundManager sm;
    private IconMove iconAMove, iconBMove;

    // Start is called before the first frame update
    void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.gameObject.GetComponent<SoundManager>();
        if (dco.enemyType == "Zero" || dco.enemyType == "Clawdius" || dco.enemyType == "Moustache" || dco.enemyType == "Lincrab")
        {
            if (sm.musicPlayer.clip != sm.musicBoss)
            {
                sm.musicPlayer.clip = sm.musicBoss;
                sm.musicPlayer.Play();
            }
        }
        else if (dco.enemyType == "Shovel")
        {
            if (sm.musicPlayer.clip != sm.musicShovel)
            {
                sm.musicPlayer.clip = sm.musicShovel;
                sm.musicPlayer.Play();
            }
        }
        else
        {
            sm.musicPlayer.clip = sm.musicBattle;
            sm.musicPlayer.Play();
        }
        iconAMove = IconA.GetComponent<IconMove>();
        iconBMove = IconB.GetComponent<IconMove>();
        int numEnemies = Random.Range(1,3);
        tbm.numEnemies = numEnemies;

        string type = dco.enemyType;
        switch(type)
        {
            case "Crab":
                CrabA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconCrab;
                tbm.enemy1 = CrabA.transform;
                tbm.targetA = CrabA.transform.GetChild(1);
                tbm.enemy1Pos = CrabA.transform.GetChild(2);
                tbm.animE1 = CrabA.GetComponent<Animator>();
                targetTextA.text = "Crab";
                letterTextA.text = "(-)";
                iconAMove.forecastName = "Crab";
                iconAMove.connectedUnit = CrabA;
                iconAMove.healthBar = CrabA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = CrabA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                LabelA.SetActive(false);
                if (numEnemies == 2 && !dco.overdriveLocked)
                {
                    int ally = Random.Range(1, 4);
                    if (ally == 1) // Rare Ally = Robo-Crab
                    {
                        LabelB.SetActive(false);
                        RoboB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconRobo;
                        tbm.enemy2 = RoboB.transform;
                        tbm.targetB = RoboB.transform.GetChild(1);
                        tbm.enemy2Pos = RoboB.transform.GetChild(2);
                        tbm.animE2 = RoboB.GetComponent<Animator>();
                        targetTextB.text = "Robo-Crab";
                        letterTextB.text = "(-)";
                        iconBMove.forecastName = ("Robo-Crab");
                        iconBMove.connectedUnit = RoboB;
                        iconBMove.healthBar = RoboB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = RoboB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                    else // Common Ally = Crab
                    {
                        LabelA.SetActive(true);
                        letterTextA.text = "(A)";
                        iconAMove.forecastName = "Crab A";
                        CrabB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconCrab;
                        tbm.enemy2 = CrabB.transform;
                        tbm.targetB = CrabB.transform.GetChild(1);
                        tbm.enemy2Pos = CrabB.transform.GetChild(2);
                        tbm.animE2 = CrabB.GetComponent<Animator>();
                        targetTextB.text = "Crab";
                        letterTextB.text = "(B)";
                        iconBMove.forecastName = ("Crab B");
                        iconBMove.connectedUnit = CrabB;
                        iconBMove.healthBar = CrabB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = CrabB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                }
                else if (dco.overdriveLocked)
                {
                    numEnemies = 1;
                    tbm.numEnemies = numEnemies;
                }
                break;
            case "Crabbit":
                CrabbitA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconCrabbit;
                tbm.enemy1 = CrabbitA.transform;
                tbm.targetA = CrabbitA.transform.GetChild(1);
                tbm.enemy1Pos = CrabbitA.transform.GetChild(2);
                tbm.animE1 = CrabbitA.GetComponent<Animator>();
                targetTextA.text = "Crabbit";
                letterTextA.text = "(-)";
                iconAMove.forecastName = "Crabbit";
                iconAMove.connectedUnit = CrabbitA;
                iconAMove.healthBar = CrabbitA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = CrabbitA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                LabelA.SetActive(false);
                if (numEnemies == 2)
                {
                    int ally = Random.Range(1, 4);
                    if (ally == 1) // Rare Ally = Crab
                    {
                        LabelB.SetActive(false);
                        CrabB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconCrab;
                        tbm.enemy2 = CrabB.transform;
                        tbm.targetB = CrabB.transform.GetChild(1);
                        tbm.enemy2Pos = CrabB.transform.GetChild(2);
                        tbm.animE2 = CrabB.GetComponent<Animator>();
                        targetTextB.text = "Crab";
                        letterTextB.text = "(-)";
                        iconBMove.forecastName = ("Crab");
                        iconBMove.connectedUnit = CrabB;
                        iconBMove.healthBar = CrabB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = CrabB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                    else // Common Ally = Crabbit
                    {
                        LabelA.SetActive(true);
                        letterTextA.text = "(A)";
                        iconAMove.forecastName = "Crabbit A";
                        CrabbitB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconCrabbit;
                        tbm.enemy2 = CrabbitB.transform;
                        tbm.targetB = CrabbitB.transform.GetChild(1);
                        tbm.enemy2Pos = CrabbitB.transform.GetChild(2);
                        tbm.animE2 = CrabbitB.GetComponent<Animator>();
                        targetTextB.text = "Crabbit";
                        letterTextB.text = "(B)";
                        iconBMove.forecastName = ("Crabbit B");
                        iconBMove.connectedUnit = CrabbitB;
                        iconBMove.healthBar = CrabbitB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = CrabbitB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                }
                break;
            case "Robo-Crab":
                RoboA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconRobo;
                tbm.enemy1 = RoboA.transform;
                tbm.targetA = RoboA.transform.GetChild(1);
                tbm.enemy1Pos = RoboA.transform.GetChild(2);
                tbm.animE1 = RoboA.GetComponent<Animator>();
                targetTextA.text = "Robo-Crab";
                letterTextA.text = "(-)";
                iconAMove.forecastName = "Robo-Crab";
                iconAMove.connectedUnit = RoboA;
                iconAMove.healthBar = RoboA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = RoboA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                LabelA.SetActive(false);
                if (numEnemies == 2)
                {
                    int ally = Random.Range(1, 4);
                    if (ally == 1) // Rare Ally = Holy Crab
                    {
                        LabelB.SetActive(false);
                        HolyB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconHoly;
                        tbm.enemy2 = HolyB.transform;
                        tbm.targetB = HolyB.transform.GetChild(1);
                        tbm.enemy2Pos = HolyB.transform.GetChild(2);
                        tbm.animE2 = HolyB.GetComponent<Animator>();
                        targetTextB.text = "Holy Crab";
                        letterTextB.text = "(-)";
                        iconBMove.forecastName = "Holy Crab";
                        iconBMove.connectedUnit = HolyB;
                        iconBMove.healthBar = HolyB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = HolyB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                    else // Common Ally = Robo-Crab
                    {
                        LabelA.SetActive(true);
                        letterTextA.text = "(A)";
                        iconAMove.forecastName = "Robo-Crab A";
                        RoboB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconRobo;
                        tbm.enemy2 = RoboB.transform;
                        tbm.targetB = RoboB.transform.GetChild(1);
                        tbm.enemy2Pos = RoboB.transform.GetChild(2);
                        tbm.animE2 = RoboB.GetComponent<Animator>();
                        targetTextB.text = "Robo-Crab";
                        letterTextB.text = "(B)";
                        iconBMove.forecastName = "Robo-Crab B";
                        iconBMove.connectedUnit = RoboB;
                        iconBMove.healthBar = RoboB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = RoboB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                }
                break;
            case "Back-Crab":
                BackA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconBack;
                tbm.enemy1 = BackA.transform;
                tbm.targetA = BackA.transform.GetChild(1);
                tbm.enemy1Pos = BackA.transform.GetChild(2);
                tbm.animE1 = BackA.GetComponent<Animator>();
                targetTextA.text = "Back-Crab";
                letterTextA.text = "(-)";
                iconAMove.forecastName = "Back-Crab";
                iconAMove.connectedUnit = BackA;
                iconAMove.healthBar = BackA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = BackA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                LabelA.SetActive(false);
                if (numEnemies == 2)
                {
                    int ally = Random.Range(1, 4);
                    if (ally == 1) // Rare Ally = Holey Crab
                    {
                        LabelB.SetActive(false);
                        HoleyB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconHoley;
                        tbm.enemy2 = HoleyB.transform;
                        tbm.targetB = HoleyB.transform.GetChild(1);
                        tbm.enemy2Pos = HoleyB.transform.GetChild(2);
                        tbm.animE2 = HoleyB.GetComponent<Animator>();
                        targetTextB.text = "Holey Crab";
                        letterTextB.text = "(-)";
                        iconBMove.forecastName = "Holey Crab";
                        iconBMove.connectedUnit = HoleyB;
                        iconBMove.healthBar = HoleyB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = HoleyB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                    else // Common Ally = Back-Crab
                    {
                        LabelA.SetActive(true);
                        letterTextA.text = "(A)";
                        iconAMove.forecastName = "Back-Crab A";
                        BackB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconBack;
                        tbm.enemy2 = BackB.transform;
                        tbm.targetB = BackB.transform.GetChild(1);
                        tbm.enemy2Pos = BackB.transform.GetChild(2);
                        tbm.animE2 = BackB.GetComponent<Animator>();
                        targetTextB.text = "Back-Crab";
                        letterTextB.text = "(B)";
                        iconBMove.forecastName = "Back-Crab B";
                        iconBMove.connectedUnit = BackB;
                        iconBMove.healthBar = BackB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = BackB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                }
                break;
            case "Crab Witch":
                WitchA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconWitch;
                tbm.enemy1 = WitchA.transform;
                tbm.targetA = WitchA.transform.GetChild(1);
                tbm.enemy1Pos = WitchA.transform.GetChild(2);
                tbm.animE1 = WitchA.GetComponent<Animator>();
                targetTextA.text = "Crab Witch";
                letterTextA.text = "(-)";
                iconAMove.forecastName = "Crab Witch";
                iconAMove.connectedUnit = WitchA;
                iconAMove.healthBar = WitchA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = WitchA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                LabelA.SetActive(false);
                if (numEnemies == 2)
                {
                    int ally = Random.Range(1, 4);
                    if (ally == 1) // Rare Ally = Holy Crab
                    {
                        LabelB.SetActive(false);
                        HolyB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconHoly;
                        tbm.enemy2 = HolyB.transform;
                        tbm.targetB = HolyB.transform.GetChild(1);
                        tbm.enemy2Pos = HolyB.transform.GetChild(2);
                        tbm.animE2 = HolyB.GetComponent<Animator>();
                        targetTextB.text = "Holy Crab";
                        letterTextB.text = "(-)";
                        iconBMove.forecastName = "Holy Crab";
                        iconBMove.connectedUnit = HolyB;
                        iconBMove.healthBar = HolyB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = HolyB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                    else // Common Ally = Crab Witch
                    {
                        LabelA.SetActive(true);
                        letterTextA.text = "(A)";
                        iconAMove.forecastName = "Crab Witch A";
                        WitchB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconWitch;
                        tbm.enemy2 = WitchB.transform;
                        tbm.targetB = WitchB.transform.GetChild(1);
                        tbm.enemy2Pos = WitchB.transform.GetChild(2);
                        tbm.animE2 = WitchB.GetComponent<Animator>();
                        targetTextB.text = "Crab Witch";
                        letterTextB.text = "(B)";
                        iconBMove.forecastName = "Crab Witch B";
                        iconBMove.connectedUnit = WitchB;
                        iconBMove.healthBar = WitchB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = WitchB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                }
                break;
            case "Crabwich":
                MimicA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconMimic;
                tbm.enemy1 = MimicA.transform;
                tbm.targetA = MimicA.transform.GetChild(1);
                tbm.enemy1Pos = MimicA.transform.GetChild(2);
                tbm.animE1 = MimicA.GetComponent<Animator>();
                targetTextA.text = "Crabwich";
                letterTextA.text = "(-)";
                iconAMove.forecastName = "Crabwich";
                iconAMove.connectedUnit = MimicA;
                iconAMove.healthBar = MimicA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = MimicA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                LabelA.SetActive(false);
                if (numEnemies == 2)
                {
                    int ally = Random.Range(1, 4);
                    if (ally == 1) // Rare Ally = Crabbit
                    {
                        LabelB.SetActive(false);
                        CrabbitB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconCrabbit;
                        tbm.enemy2 = CrabbitB.transform;
                        tbm.targetB = CrabbitB.transform.GetChild(1);
                        tbm.enemy2Pos = CrabbitB.transform.GetChild(2);
                        tbm.animE2 = CrabbitB.GetComponent<Animator>();
                        targetTextB.text = "Crabbit";
                        letterTextB.text = "(-)";
                        iconBMove.forecastName = "Crabbit";
                        iconBMove.connectedUnit = CrabbitB;
                        iconBMove.healthBar = CrabbitB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = CrabbitB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                    else // Common Ally = Crabwich
                    {
                        LabelA.SetActive(true);
                        letterTextA.text = "(A)";
                        iconAMove.forecastName = "Crabwich A";
                        MimicB.SetActive(true);
                        IconB.SetActive(true);
                        IconB.GetComponent<Image>().sprite = iconMimic;
                        tbm.enemy2 = MimicB.transform;
                        tbm.targetB = MimicB.transform.GetChild(1);
                        tbm.enemy2Pos = MimicB.transform.GetChild(2);
                        tbm.animE2 = MimicB.GetComponent<Animator>();
                        targetTextB.text = "Crabwich";
                        letterTextB.text = "(B)";
                        iconBMove.forecastName = "Crabwich B";
                        iconBMove.connectedUnit = MimicB;
                        iconBMove.healthBar = MimicB.transform.GetChild(0).GetChild(0);
                        iconBMove.healthText = MimicB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                    }
                }
                break;
            case "Holy Crab": //Only here for debugging purposes; cannot naturally occur
                HolyA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconHoly;
                HolyB.SetActive(true);
                IconB.SetActive(true);
                IconB.GetComponent<Image>().sprite = iconHoly;
                tbm.enemy1 = HolyA.transform;
                tbm.targetA = HolyA.transform.GetChild(1);
                tbm.enemy1Pos = HolyA.transform.GetChild(2);
                tbm.enemy2 = HolyB.transform;
                tbm.targetB = HolyB.transform.GetChild(1);
                tbm.enemy2Pos = HolyB.transform.GetChild(2);
                tbm.animE1 = HolyA.GetComponent<Animator>();
                tbm.animE2 = HolyB.GetComponent<Animator>();
                targetTextA.text = "Holy Crab";
                targetTextB.text = "Holy Crab";
                iconAMove.connectedUnit = HolyA;
                iconAMove.healthBar = HolyA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = HolyA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                iconBMove.connectedUnit = HolyB;
                iconBMove.healthBar = HolyB.transform.GetChild(0).GetChild(0);
                iconBMove.healthText = HolyB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                if (numEnemies == 1)
                {
                    HolyB.SetActive(false);
                    IconB.SetActive(false);
                    LabelA.SetActive(false);
                }
                break;
            case "Holey Crab": //Only here for debugging purposes; cannot naturally occur
                HoleyA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconHoley;
                HoleyB.SetActive(true);
                IconB.SetActive(true);
                IconB.GetComponent<Image>().sprite = iconHoley;
                tbm.enemy1 = HoleyA.transform;
                tbm.targetA = HoleyA.transform.GetChild(1);
                tbm.enemy1Pos = HoleyA.transform.GetChild(2);
                tbm.enemy2 = HoleyB.transform;
                tbm.targetB = HoleyB.transform.GetChild(1);
                tbm.enemy2Pos = HoleyB.transform.GetChild(2);
                tbm.animE1 = HoleyA.GetComponent<Animator>();
                tbm.animE2 = HoleyB.GetComponent<Animator>();
                targetTextA.text = "Holey Crab";
                targetTextB.text = "Holey Crab";
                iconAMove.connectedUnit = HoleyA;
                iconAMove.healthBar = HoleyA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = HoleyA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                iconBMove.connectedUnit = HoleyB;
                iconBMove.healthBar = HoleyB.transform.GetChild(0).GetChild(0);
                iconBMove.healthText = HoleyB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                if (numEnemies == 1)
                {
                    HoleyB.SetActive(false);
                    IconB.SetActive(false);
                    LabelA.SetActive(false);
                }
                break;
            case "Zero":
                Zero.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconZero;
                //ZClaw.SetActive(true);
                //IconB.SetActive(true);
                IconB.GetComponent<Image>().sprite = iconZClaw;
                tbm.enemy1 = Zero.transform;
                tbm.targetA = Zero.transform.GetChild(1);
                tbm.enemy1Pos = Zero.transform.GetChild(2);
                tbm.enemy2 = ZClaw.transform;
                tbm.targetB = ZClaw.transform.GetChild(1);
                tbm.enemy2Pos = ZClaw.transform.GetChild(2);
                tbm.animE1 = Zero.GetComponent<Animator>();
                tbm.animE2 = ZClaw.GetComponent<Animator>();
                targetTextA.text = "Crabsolute Zero";
                iconAMove.forecastName = "Crabsolute Zero";
                targetTextB.text = "Giant Claw";
                iconBMove.forecastName = "Giant Claw";
                iconAMove.connectedUnit = Zero;
                iconAMove.healthBar = Zero.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = Zero.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                iconBMove.connectedUnit = ZClaw;
                iconBMove.healthBar = ZClaw.transform.GetChild(0).GetChild(0);
                iconBMove.healthText = ZClaw.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                numEnemies = 1;
                tbm.numEnemies = numEnemies;
                LabelA.SetActive(false);
                LabelB.SetActive(false);
                letterTextA.text = "(-)";
                letterTextB.text = "(-)";
                iconBMove.healthBarBack = ZClaw.transform.GetChild(0);
                break;
            case "Clawdius":
                Clawdius.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconClawdius;
                CCannon.SetActive(true);
                IconB.SetActive(true);
                IconB.GetComponent<Image>().sprite = iconCCannon;
                tbm.enemy1 = Clawdius.transform;
                tbm.targetA = Clawdius.transform.GetChild(1);
                tbm.enemy1Pos = Clawdius.transform.GetChild(2);
                tbm.enemy2 = CCannon.transform;
                tbm.targetB = CCannon.transform.GetChild(1);
                tbm.enemy2Pos = CCannon.transform.GetChild(2);
                tbm.animE1 = Clawdius.GetComponent<Animator>();
                tbm.animE2 = CCannon.GetComponent<Animator>();
                targetTextA.text = "Crabtain Clawdius";
                iconAMove.forecastName = "Crabtain Clawdius";
                targetTextB.text = "Cannon";
                iconBMove.forecastName = "Cannon";
                iconAMove.connectedUnit = Clawdius;
                iconAMove.healthBar = Clawdius.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = Clawdius.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                iconBMove.connectedUnit = CCannon;
                iconBMove.healthBar = CCannon.transform.GetChild(0).GetChild(0);
                iconBMove.healthText = CCannon.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                numEnemies = 2;
                tbm.numEnemies = numEnemies;
                LabelA.SetActive(false);
                LabelB.SetActive(false);
                letterTextA.text = "(-)";
                letterTextB.text = "(-)";
                iconBMove.healthBarBack = CCannon.transform.GetChild(0);
                break;
            case "Moustache":
                Moustache.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconMoustache;
                MJimmy.SetActive(true);
                IconB.SetActive(true);
                IconB.GetComponent<Image>().sprite = iconMJimmy;
                tbm.enemy1 = Moustache.transform;
                tbm.targetA = Moustache.transform.GetChild(1);
                tbm.enemy1Pos = Moustache.transform.GetChild(2);
                tbm.enemy2 = MJimmy.transform;
                tbm.targetB = MJimmy.transform.GetChild(1);
                tbm.enemy2Pos = MJimmy.transform.GetChild(2);
                tbm.animE1 = Moustache.GetComponent<Animator>();
                tbm.animE2 = MJimmy.GetComponent<Animator>();
                targetTextA.text = "Crustacé Moustache";
                iconAMove.forecastName = "Crustacé Moustache";
                targetTextB.text = "Intern Jimmy";
                iconBMove.forecastName = "Jimmy";
                iconAMove.connectedUnit = Moustache;
                iconAMove.healthBar = Moustache.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = Moustache.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                iconBMove.connectedUnit = MJimmy;
                iconBMove.healthBar = MJimmy.transform.GetChild(0).GetChild(0);
                iconBMove.healthText = MJimmy.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                numEnemies = 2;
                tbm.numEnemies = numEnemies;
                LabelA.SetActive(false);
                LabelB.SetActive(false);
                letterTextA.text = "(-)";
                letterTextB.text = "(-)";
                iconBMove.healthBarBack = MJimmy.transform.GetChild(0);
                break;
            case "Shovel":
                Shovel.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconShovel;
                tbm.enemy1 = Shovel.transform;
                tbm.targetA = Shovel.transform.GetChild(1);
                tbm.enemy1Pos = Shovel.transform.GetChild(2);
                tbm.animE1 = Shovel.GetComponent<Animator>();
                targetTextA.text = "Shovel Crab";
                iconAMove.forecastName = "Shovel Crab";
                iconAMove.connectedUnit = Shovel;
                iconAMove.healthBar = Shovel.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = Shovel.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                numEnemies = 1;
                tbm.numEnemies = numEnemies;
                LabelA.SetActive(false);
                letterTextA.text = "(-)";
                break;
            case "Lincrab":
                Lincrab.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconLincrab;
                tbm.enemy1 = Lincrab.transform;
                tbm.targetA = Lincrab.transform.GetChild(1);
                tbm.enemy1Pos = Lincrab.transform.GetChild(2);
                tbm.animE1 = Lincrab.GetComponent<Animator>();
                targetTextA.text = "Crabraham Lincrab";
                iconAMove.forecastName = "Crabraham Lincrab";
                iconAMove.connectedUnit = Lincrab;
                iconAMove.healthBar = Lincrab.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = Lincrab.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                numEnemies = 1;
                tbm.numEnemies = numEnemies;
                LabelA.SetActive(false);
                letterTextA.text = "(-)";
                break;
            default:
                Debug.Log("Loaded default encounter! What happened?");
                tbm.numEnemies = 2;
                CrabA.SetActive(true);
                IconA.SetActive(true);
                IconA.GetComponent<Image>().sprite = iconCrab;
                CrabB.SetActive(true);
                IconB.SetActive(true);
                IconB.GetComponent<Image>().sprite = iconCrab;
                tbm.enemy1 = CrabA.transform;
                tbm.targetA = CrabA.transform.GetChild(1);
                tbm.enemy1Pos = CrabA.transform.GetChild(2);
                tbm.enemy2 = CrabB.transform;
                tbm.targetB = CrabB.transform.GetChild(1);
                tbm.enemy2Pos = CrabB.transform.GetChild(2);
                tbm.animE1 = CrabA.GetComponent<Animator>();
                tbm.animE2 = CrabB.GetComponent<Animator>();
                targetTextA.text = "Crab";
                targetTextB.text = "Crab";
                iconAMove.connectedUnit = CrabA;
                iconAMove.healthBar = CrabA.transform.GetChild(0).GetChild(0);
                iconAMove.healthText = CrabA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                iconBMove.connectedUnit = CrabB;
                iconBMove.healthBar = CrabB.transform.GetChild(0).GetChild(0);
                iconBMove.healthText = CrabB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
                tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
                tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
                break;
        }
    }
    
    public void SummonCrabbit()
    {
        if (!tbm.enemy1.gameObject.activeSelf)
        {
            CrabbitA.SetActive(true);
            IconA.SetActive(true);
            IconA.GetComponent<Image>().sprite = iconCrabbit;
            IconA.transform.localPosition = new Vector3(650.0f, 0.0f, 0.0f);
            tbm.enemy1 = CrabbitA.transform;
            tbm.targetA = CrabbitA.transform.GetChild(1);
            tbm.enemy1Pos = CrabbitA.transform.GetChild(2);
            tbm.animE1 = CrabbitA.GetComponent<Animator>();
            iconAMove.connectedUnit = CrabbitA;
            iconAMove.healthBar = CrabbitA.transform.GetChild(0).GetChild(0);
            iconAMove.healthText = CrabbitA.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
            tbm.auraE1 = tbm.enemy1.gameObject.GetComponent<AuraGenerator>();
            iconAMove.RefreshEncounter();
            if (iconBMove.forecastName == "Crabbit" || iconBMove.forecastName == "Crabbit B")
            {
                targetTextA.text = "Crabbit";
                letterTextA.text = "(A)";
                iconAMove.forecastName = "Crabbit A";
                LabelA.SetActive(true);
                targetTextB.text = "Crabbit";
                letterTextB.text = "(B)";
                iconBMove.forecastName = "Crabbit B";
                LabelB.SetActive(true);
            }
            else
            {
                targetTextA.text = "Crabbit";
                letterTextA.text = "(-)";
                iconAMove.forecastName = "Crabbit";
                LabelA.SetActive(false);
            }
        }
        else if (!tbm.enemy2.gameObject.activeSelf)
        {
            CrabbitB.SetActive(true);
            IconB.SetActive(true);
            IconB.GetComponent<Image>().sprite = iconCrabbit;
            IconB.transform.localPosition = new Vector3(650.0f, 0.0f, 0.0f);
            tbm.enemy2 = CrabbitB.transform;
            tbm.targetB = CrabbitB.transform.GetChild(1);
            tbm.enemy2Pos = CrabbitB.transform.GetChild(2);
            tbm.animE2 = CrabbitB.GetComponent<Animator>();
            iconBMove.connectedUnit = CrabbitB;
            iconBMove.healthBar = CrabbitB.transform.GetChild(0).GetChild(0);
            iconBMove.healthText = CrabbitB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
            tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
            iconBMove.RefreshEncounter();
            tbm.numEnemies++;
            if (iconAMove.forecastName == "Crabbit" || iconAMove.forecastName == "Crabbit A")
            {
                targetTextB.text = "Crabbit";
                letterTextB.text = "(B)";
                iconBMove.forecastName = "Crabbit B";
                LabelB.SetActive(true);
                targetTextA.text = "Crabbit";
                letterTextA.text = "(A)";
                iconAMove.forecastName = "Crabbit A";
                LabelA.SetActive(true);
            }
            else
            {
                targetTextB.text = "Crabbit";
                letterTextB.text = "(-)";
                iconBMove.forecastName = "Crabbit";
                LabelB.SetActive(false);
            }
        }
    }

    //Only used by Crabsolute Zero
    public void SummonZClaw()
    {
        ZClaw.SetActive(true);
        IconB.SetActive(true);
        IconB.transform.localPosition = new Vector3(700.0f, 0.0f, 0.0f);
        iconBMove.healthBarBack.gameObject.SetActive(true);
        iconBMove.healthText.gameObject.SetActive(true);
        if (dco.returningAid)
        {
            StartCoroutine("AdjustHelperHealth");
        }
        else
        {
            iconBMove.RefreshEncounter();
            dco.returningAid = true;
        }
        tbm.numEnemies++;
    }

    //Only used by Crabtain Clawdius
    public void SummonCrab()
    {
        CrabB.SetActive(true);
        IconB.SetActive(true);
        IconB.GetComponent<Image>().sprite = iconCrab;
        IconB.transform.localPosition = new Vector3(650.0f, 0.0f, 0.0f);
        tbm.enemy2 = CrabB.transform;
        tbm.targetB = CrabB.transform.GetChild(1);
        tbm.enemy2Pos = CrabB.transform.GetChild(2);
        tbm.animE2 = CrabB.GetComponent<Animator>();
        iconBMove.connectedUnit = CrabB;
        iconBMove.healthBar = CrabB.transform.GetChild(0).GetChild(0);
        iconBMove.healthText = CrabB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
        iconBMove.RefreshEncounter();
        tbm.numEnemies++;
        targetTextB.text = "Crab";
        letterTextB.text = "(-)";
        iconBMove.forecastName = "Crab";
        LabelB.SetActive(false);
    }

    //Only used by Crabraham Lincrab
    public void SummonHoly()
    {
        HolyB.SetActive(true);
        IconB.SetActive(true);
        IconB.GetComponent<Image>().sprite = iconHoly;
        IconB.transform.localPosition = new Vector3(650.0f, 0.0f, 0.0f);
        tbm.enemy2 = HolyB.transform;
        tbm.targetB = HolyB.transform.GetChild(1);
        tbm.enemy2Pos = HolyB.transform.GetChild(2);
        tbm.animE2 = HolyB.GetComponent<Animator>();
        iconBMove.connectedUnit = HolyB;
        iconBMove.healthBar = HolyB.transform.GetChild(0).GetChild(0);
        iconBMove.healthText = HolyB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
        iconBMove.RefreshEncounter();
        tbm.numEnemies++;
        targetTextB.text = "Holy Crab";
        letterTextB.text = "(-)";
        iconBMove.forecastName = "Holy Crab";
        LabelB.SetActive(false);
    }

    //Only used by Crabraham Lincrab
    public void SummonWitch()
    {
        WitchB.SetActive(true);
        IconB.SetActive(true);
        IconB.GetComponent<Image>().sprite = iconWitch;
        IconB.transform.localPosition = new Vector3(650.0f, 0.0f, 0.0f);
        tbm.enemy2 = WitchB.transform;
        tbm.targetB = WitchB.transform.GetChild(1);
        tbm.enemy2Pos = WitchB.transform.GetChild(2);
        tbm.animE2 = WitchB.GetComponent<Animator>();
        iconBMove.connectedUnit = WitchB;
        iconBMove.healthBar = WitchB.transform.GetChild(0).GetChild(0);
        iconBMove.healthText = WitchB.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMesh>();
        tbm.auraE2 = tbm.enemy2.gameObject.GetComponent<AuraGenerator>();
        iconBMove.RefreshEncounter();
        tbm.numEnemies++;
        targetTextB.text = "Crab Witch";
        letterTextB.text = "(-)";
        iconBMove.forecastName = "Crab Witch";
        LabelB.SetActive(false);
    }

    IEnumerator AdjustHelperHealth()
    {
        yield return new WaitForSeconds(0.01f);
        iconBMove.health = dco.bossAidHP;
        iconBMove.RefreshBossEncounter();
    }
}
