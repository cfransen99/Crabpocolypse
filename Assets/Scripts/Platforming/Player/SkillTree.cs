using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public int destroyerLevel = 0;
    public int slipstreamLevel = 0;
    public int garrisonLevel = 0;

    private PlayerStats stats;
    private Player player;
    private PlayerLevel playerLevel;

    public Text destroyerText;
    public Text slipstreamText;
    public Text garrisonText;

    /*[SerializeField] private GameObject chooseOnePanel;
    [SerializeField] private Text chooseOtherText;

    [SerializeField] private GameObject newAbilityPanel;
    [SerializeField] private Text newAbilityText;*/
    
    [SerializeField] private Text skillPointsText;
    /*[SerializeField] private Text notEnoughPointsText;

    [SerializeField] private Text attackPowerText;
    [SerializeField] private Text speedText;
    [SerializeField] private Text defenseText;*/

    private int tempAttack, tempSpeed, tempDefense;
    [SerializeField] private GameObject confirmCancel;
    public Text infoText;
    public GameObject pausePanel;

    public Animator[] meterParts;

    //public GameObject skillTreePanel;
    private DataCarryOver dco;
    private SoundManager sm;
    private SaveHandler saveHandler;

    private void Awake()
    {
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.gameObject.GetComponent<SoundManager>();

        if (!saveHandler.GetDidLoadSave())
        {
            destroyerLevel = dco.pointsPow;
            slipstreamLevel = dco.pointsSpeed;
            garrisonLevel = dco.pointsDef;
            destroyerText.text = destroyerLevel.ToString();
            slipstreamText.text = slipstreamLevel.ToString();
            garrisonText.text = garrisonLevel.ToString();
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        stats = player.gameObject.GetComponent<PlayerStats>();
        playerLevel = player.gameObject.GetComponent<PlayerLevel>();

    }

    private void OnEnable()
    {
        SetPoints();
    }

    private void Update()
    {
        skillPointsText.text = "Skill Points: " + playerLevel.skillPoints;
    }

    public void SetPoints()
    {
        int tenOrDestroy = 10;
        int tenOrGarrison = 10;
        int tenOrSlip = 10;

        if(destroyerLevel < tenOrDestroy)
        {
            tenOrDestroy = destroyerLevel;
        }
        if (slipstreamLevel < tenOrSlip)
        {
            tenOrSlip = slipstreamLevel;
        }
        if (garrisonLevel < tenOrGarrison)
        {
            tenOrGarrison = garrisonLevel;
        }
        

        for (int i = 0; i < tenOrDestroy; i++)
        {
            meterParts[i].SetTrigger("goFull");
        }
        for (int i = 0; i < tenOrSlip; i++)
        {
            meterParts[10 + i].SetTrigger("goFull");
        }
        for (int i = 0; i < tenOrGarrison; i++)
        {
            meterParts[20 + i].SetTrigger("goFull");
        }
    }

    //Controls temporary point allocation
    public void TempUpAttack()
    {
        if (playerLevel.skillPoints > 0)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tempAttack++;
            playerLevel.skillPoints--;
            if (destroyerLevel + tempAttack - 1 <= 9)
            {
                meterParts[destroyerLevel + tempAttack - 1].SetTrigger("goHalf");
            }
            destroyerText.text = destroyerLevel + " + " + tempAttack;
            confirmCancel.SetActive(true);
        }
        else
        {
            sm.sfxPlayer.PlayOneShot(sm.soundGuard);
        }
    }

    public void TempDownAttack()
    {
        if (tempAttack > 0)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tempAttack--;
            playerLevel.skillPoints++;
            if (destroyerLevel + tempAttack <= 9)
            {
                meterParts[destroyerLevel + tempAttack].SetTrigger("goOff");
            }
            if (tempAttack == 0)
            {
                destroyerText.text = "" + destroyerLevel;
            }
            else
            {
                destroyerText.text = destroyerLevel + " + " + tempAttack;
            }
            if (tempAttack == 0 && tempSpeed == 0 && tempDefense == 0)
            {
                confirmCancel.SetActive(false);
            }
        }
        else
        {
            sm.sfxPlayer.PlayOneShot(sm.soundGuard);
        }
    }

    public void TempUpSpeed()
    {
        if (playerLevel.skillPoints > 0)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tempSpeed++;
            playerLevel.skillPoints--;
            if (9 + slipstreamLevel + tempSpeed <= 19)
            {
                meterParts[9 + slipstreamLevel + tempSpeed].SetTrigger("goHalf");
            }
            slipstreamText.text = slipstreamLevel + " + " + tempSpeed;
            confirmCancel.SetActive(true);
        }
        else
        {
            sm.sfxPlayer.PlayOneShot(sm.soundGuard);
        }
    }

    public void TempDownSpeed()
    {
        if (tempSpeed > 0)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tempSpeed--;
            playerLevel.skillPoints++;
            if (10 + slipstreamLevel + tempSpeed <= 19)
                meterParts[10 + slipstreamLevel + tempSpeed].SetTrigger("goOff");
            if (tempSpeed == 0)
            {
                slipstreamText.text = "" + slipstreamLevel;
            }
            else
            {
                slipstreamText.text = slipstreamLevel + " + " + tempSpeed;
            }
            if (tempAttack == 0 && tempSpeed == 0 && tempDefense == 0)
            {
                confirmCancel.SetActive(false);
            }
        }
        else
        {
            sm.sfxPlayer.PlayOneShot(sm.soundGuard);
        }
    }

    public void TempUpDefense()
    {
        if (playerLevel.skillPoints > 0 && garrisonLevel + tempDefense + 1 <= 60)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tempDefense++;
            playerLevel.skillPoints--;
            if (19 + garrisonLevel + tempDefense <= 29)
            {
                meterParts[19 + garrisonLevel + tempDefense].SetTrigger("goHalf");
            }
            garrisonText.text = garrisonLevel + " + " + tempDefense;
            confirmCancel.SetActive(true);
        }
        else
        {
            sm.sfxPlayer.PlayOneShot(sm.soundGuard);
        }
    }

    public void TempDownDefense()
    {
        if (tempDefense > 0)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tempDefense--;
            playerLevel.skillPoints++;
            if (20 + garrisonLevel + tempDefense <= 29)
            {
                meterParts[20 + garrisonLevel + tempDefense].SetTrigger("goOff");
            }
            if (tempDefense == 0)
            {
                garrisonText.text = "" + garrisonLevel;
            }
            else
            {
                garrisonText.text = garrisonLevel + " + " + tempDefense;
            }
            if (tempAttack == 0 && tempSpeed == 0 && tempDefense == 0)
            {
                confirmCancel.SetActive(false);
            }

        }
        else
        {
            sm.sfxPlayer.PlayOneShot(sm.soundGuard);
        }
    }

    //Controls permanent point allocation
    public void ConfirmPoints()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundKaching);
        for (int i = 0; i < tempAttack; i++)
        {
            destroyerLevel++;
            if (destroyerLevel <= 10)
            {
                meterParts[destroyerLevel - 1].SetTrigger("goFull");
            }
            if (stats.playerAttack < 20)
            {
                stats.playerAttack += 2;
            }
            else
            {
                stats.playerAttack++;
            }
            dco.pointsPow = destroyerLevel;
        }
        tempAttack = 0;
        for (int i = 0; i < tempSpeed; i++)
        {
            slipstreamLevel++;
            if (slipstreamLevel <= 10)
            {
                meterParts[9 + slipstreamLevel].SetTrigger("goFull");
            }
            if (stats.playerSpeed < 20)
            {
                stats.playerSpeed += 2;
            }
            else
            {
                stats.playerSpeed++;
            }
            dco.pointsSpeed = slipstreamLevel;
        }
        tempSpeed = 0;
        for (int i = 0; i < tempDefense; i++)
        {
            garrisonLevel++;
            if (garrisonLevel <= 10)
            {
                meterParts[19 + garrisonLevel].SetTrigger("goFull");
            }
            if (stats.initPlayerDefense < 20)
            {
                stats.initPlayerDefense += 2;
            }
            else
            {
                stats.initPlayerDefense++;
            }
            dco.pointsDef = garrisonLevel;
        }
        tempDefense = 0;
        destroyerText.text = "" + destroyerLevel;
        slipstreamText.text = "" + slipstreamLevel;
        garrisonText.text = "" + garrisonLevel;
        dco.playerPow = stats.playerAttack;
        dco.playerSpeed = stats.playerSpeed;
        dco.playerDef = stats.initPlayerDefense;
        stats.playerDefense = stats.initPlayerDefense;
        confirmCancel.SetActive(false);
    }

    public void CancelPoints()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        playerLevel.skillPoints += tempAttack + tempSpeed + tempDefense;
        for (int i = destroyerLevel; i < destroyerLevel + tempAttack; i++)
        {
            meterParts[i].SetTrigger("goOff");
        }
        for (int i = slipstreamLevel; i < slipstreamLevel + tempSpeed; i++)
        {
            meterParts[i].SetTrigger("goOff");
        }
        for (int i = garrisonLevel; i < garrisonLevel + tempDefense; i++)
        {
            meterParts[i].SetTrigger("goOff");
        }
        tempAttack = 0;
        tempSpeed = 0;
        tempDefense = 0;
        destroyerText.text = "" + destroyerLevel;
        slipstreamText.text = "" + slipstreamLevel;
        garrisonText.text = "" + garrisonLevel;
        confirmCancel.SetActive(false);
    }

    //Controls mouse-over text
    public void InfoDefault()
    {
        infoText.text = "Hover over a skill to get more detailed information about it and any sub-skills you've unlocked.";
    }

    public void InfoDestroyerA()
    {
        infoText.text = "DESTROYER\n\nThis skill increases your attack power during battle.\n\nCurrent Boost:\n";
        infoText.text += "+" + (dco.playerPow / 2) + " damage\n\nSub-Skills:\n";
        if (destroyerLevel >= 10)
        {
            infoText.text += "Strike Momentum\nPowerful Retaliation\nMighty Aura";
        }
        else if (destroyerLevel >= 6)
        {
            infoText.text += "Strike Momentum\nPowerful Retaliation";
        }
        else if (destroyerLevel >= 3)
        {
            infoText.text += "Strike Momentum";
        }
        else
        {
            infoText.text += "None";
        }
    }

    public void InfoDestroyerB()
    {
        if (destroyerLevel >= 3)
        {
            infoText.text = "STRIKE MOMENTUM\n\n25% chance to grow Empowered (1 turn) after a basic attack.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (3 - destroyerLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void InfoDestroyerC()
    {
        if (destroyerLevel >= 6)
        {
            infoText.text = "POWERFUL RETALIATION\n\nGrow Empowered (1 turn) after getting interrupted.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (6 - destroyerLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void InfoDestroyerD()
    {
        if (destroyerLevel >= 10)
        {
            infoText.text = "MIGHTY AURA\n\n+1 turn duration for all Empowered buffs gained.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (10 - destroyerLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void InfoSlipstreamA()
    {
        infoText.text = "SLIPSTREAM\n\nThis skill increases your timeline speed during battle.\n\nCurrent Boost:\n";
        infoText.text += "+" + (dco.playerSpeed * 2) + "% speed\n\nSub-Skills:\n";
        if (slipstreamLevel >= 10)
        {
            infoText.text += "Fast Cast\nHasty Retaliation\nLightened Aura";
        }
        else if (slipstreamLevel >= 6)
        {
            infoText.text += "Fast Cast\nHasty Retaliation";
        }
        else if (slipstreamLevel >= 3)
        {
            infoText.text += "Fast Cast";
        }
        else
        {
            infoText.text += "None";
        }
    }

    public void InfoSlipstreamB()
    {
        if (slipstreamLevel >= 3)
        {
            infoText.text = "FAST CAST\n\n25% chance to boost casting speed (1 stage) of any action.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (3 - slipstreamLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void InfoSlipstreamC()
    {
        if (slipstreamLevel >= 6)
        {
            infoText.text = "HASTY RETALIATION\n\nGrow Hastened (1 turn) after getting interrupted.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (6 - slipstreamLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void InfoSlipstreamD()
    {
        if (slipstreamLevel >= 10)
        {
            infoText.text = "LIGHTENED AURA\n\n+1 turn duration for all Hastened/Evasive buffs gained.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (10 - slipstreamLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void InfoGarrisonA()
    {
        infoText.text = "GARRISON\n\nThis skill increases your resistance to damage during battle.\n\nCurrent Boost:\n";
        infoText.text += "-" + dco.playerDef + "% damage taken\n\nSub-Skills:\n";
        if (slipstreamLevel >= 10)
        {
            infoText.text += "Immovable\nShielded Retaliation\nHearty Aura";
        }
        else if (slipstreamLevel >= 6)
        {
            infoText.text += "Immovable\nShielded Retaliation";
        }
        else if (slipstreamLevel >= 3)
        {
            infoText.text += "Immovable";
        }
        else
        {
            infoText.text += "None";
        }
    }

    public void InfoGarrisonB()
    {
        if (garrisonLevel >= 3)
        {
            infoText.text = "IMMOVABLE\n\n25% chance to grow Armored (1 turn) after guarding.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (3 - garrisonLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void InfoGarrisonC()
    {
        if (garrisonLevel >= 6)
        {
            infoText.text = "SHIELDED RETALIATION\n\nGrow Armored (1 turn) after getting interrupted.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (6 - garrisonLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void InfoGarrisonD()
    {
        if (garrisonLevel >= 10)
        {
            infoText.text = "HEARTY AURA\n\n+1 turn duration for all Armored buffs gained.";
        }
        else
        {
            infoText.text = "UNKNOWN\n\nSpend " + (10 - garrisonLevel) + " more skill points to unlock this sub-skill.";
        }
    }

    public void SkillBack()
    {
        CancelPoints();
        pausePanel.SetActive(true);
        gameObject.SetActive(false);
    }
    //CUTOFF
    /*
    public void LevelUpDestroyer()
    {
        if(playerLevel.skillPoints > 0)
        {
            notEnoughPointsText.gameObject.SetActive(false);
            destroyerLevel++;
            dco.pointsPow++;
            destroyerText.text = destroyerLevel.ToString();
            if (destroyerLevel <= 10)
            {
                stats.playerAttack += 2;
                attackPowerText.text = "Attack: " + stats.playerAttack.ToString();
                if (destroyerLevel == 3)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown mightier! " +
                    "25% chance to grow Empowered (1 turn) after a basic attack.";
                }
                else if (destroyerLevel == 6)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown mightier! " +
                    "Retaliation: Grow Empowered (1 turn) after getting interrupted.";
                }
                else if (destroyerLevel == 10)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown mightier! " +
                    "+1 turn duration for all Empowered buffs gained.";
                }
            }
            else
            {
                stats.playerAttack++;
                attackPowerText.text = "Attack: " + stats.playerAttack.ToString();
            }
            dco.playerPow = stats.playerAttack;
            playerLevel.skillPoints--;
        }
        else
        {
            notEnoughPointsText.gameObject.SetActive(true);
        }
    }
    public void LevelUpSlipstream()
    {
        if (playerLevel.skillPoints > 0)
        {
            notEnoughPointsText.gameObject.SetActive(false);
            slipstreamLevel++;
            dco.pointsSpeed++;
            slipstreamText.text = slipstreamLevel.ToString();
            if (slipstreamLevel <= 10)
            {
                stats.playerSpeed += 2;
                speedText.text = "Speed: " + stats.playerSpeed.ToString();
                if (slipstreamLevel == 3)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown more nimble! " +
                    "25% chance to boost casting speed (1 stage) of any action.";
                }
                else if (slipstreamLevel == 6)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown more nimble! " +
                    "Retaliation: Grow Hastened (1 turn) after getting interrupted.";
                }
                else if (slipstreamLevel == 10)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown more nimble! " +
                    "+1 turn duration for all Hastened/Evasive buffs gained.";
                }
            }
            else
            {
                stats.playerSpeed++;
                speedText.text = "Speed: " + stats.playerSpeed.ToString();
            }
            dco.playerSpeed = stats.playerSpeed;
            playerLevel.skillPoints--;
        }
        else
        {
            notEnoughPointsText.gameObject.SetActive(true);
        }
    }
    public void LevelUpGarrison()
    {
        if (playerLevel.skillPoints > 0 && garrisonLevel < 60)
        {
            notEnoughPointsText.gameObject.SetActive(false);
            garrisonLevel++;
            dco.pointsDef++;
            garrisonText.text = garrisonLevel.ToString();
            if (garrisonLevel <= 10)
            {
                stats.initPlayerDefense += 5;
                defenseText.text = "Defense: " + stats.initPlayerDefense.ToString();
                if (garrisonLevel == 3)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown fortified! " +
                    "25% chance to grow Armored (1 turn) after guarding.";
                }
                else if (garrisonLevel == 6)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown fortified! " +
                    "Retaliation: Grow Armored (1 turn) after getting interrupted.";
                }
                else if (garrisonLevel == 10)
                {
                    newAbilityPanel.SetActive(true);
                    newAbilityText.text = "You have grown fortified! " +
                    "+1 turn duration for all Armored buffs gained.";
                }
            }
            else
            {
                stats.initPlayerDefense++;
                defenseText.text = "Defense: " + stats.playerDefense.ToString();
            }
            dco.playerDef = stats.initPlayerDefense;
            playerLevel.skillPoints--;
            stats.playerDefense = stats.initPlayerDefense;
        }
        else if (playerLevel.skillPoints <= 0)
        {
            notEnoughPointsText.gameObject.SetActive(true);
        }
    }

    //public void ChooseDash()
    //{
    //    chooseOtherText.gameObject.SetActive(false);
    //    //if (!player.learnDash)
    //    //{
    //    //    chooseOnePanel.SetActive(false);

    //    //    newAbilityPanel.SetActive(true);
    //    //    newAbilityText.text = "You have unlocked the Dash ability! " +
    //    //        "You can now have a burst of speed on the ground that will allow to reach previously unreachable locations!";

    //    //    player.learnDash = true;
    //    //    dco.learnDash = true;
    //    //}
    //    else
    //    {
    //        chooseOtherText.gameObject.SetActive(true);
    //    }
    //}

    //public void ChooseAirDash()
    //{
    //    chooseOtherText.gameObject.SetActive(false);
    //    //if (!player.learnAir)
    //    //{
    //    //    chooseOnePanel.SetActive(false);

    //    //    newAbilityPanel.SetActive(true);
    //    //    newAbilityText.text = "You have unlocked the Air Dash ability! " +
    //    //        "You can now have a burst of speed that will propel yourself skyward!";

    //    //    player.learnAir = true;
    //    //    dco.learnAir = true;
    //    //}
    //    else
    //    {
    //        chooseOtherText.gameObject.SetActive(true);
    //    }
    //}

    public void CloseNewAbilityPanel()
    {
        newAbilityPanel.SetActive(false);
    }

    public void ExitPanel()
    {
        player.uIController.ResetPanels();
    }*/
}
