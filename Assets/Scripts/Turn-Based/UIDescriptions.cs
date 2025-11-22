using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDescriptions : MonoBehaviour
{
    public GameObject panelDesc, panelPlayer, panelE1, panelE2, panelOverdrive, glowPlayer, glowE1, glowE2;
    public Text textDesc, textPlayerNL, textPlayerStatus, textPlayerDesc, textE1NL, textE1Status, textE1Desc, textE2NL, textE2Status, textE2Desc;

    public TurnBasedManager tbm;
    private DataCarryOver dco;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
    }

    public void EnterBasicAttack()
    {
        panelDesc.SetActive(true);
        textDesc.text = "Throw a wild haymaker that deals a small amount of damage.";
        textDesc.text += "\n\nSpeed: Medium                 |                 Cost: None";
    }
    public void EnterBasicGuard()
    {
        panelDesc.SetActive(true);
        textDesc.text = "Reduces all incoming damage and boosts speed until next Cast.";
        textDesc.text += "\n\nSpeed: Instant                 |                 Cost: None";
    }
    public void EnterEscape()
    {
        panelDesc.SetActive(true);
        textDesc.text = "Run away to fight another day, leaving any rewards behind\nNote: There are some battles you cannot run from..";
        textDesc.text += "\n\nSpeed: Very Slow                 |                 Cost: None";
    }
    public void EnterAbiDash()
    {
        if (dco.learnDash)
        {
            panelDesc.SetActive(true);
            textDesc.text = "Dash about to boost your maneuverability, granting yourself the Hastened status for 3 turns.";
            textDesc.text += "\n\nSpeed: Medium                 |                 Cost: 10 DP";
        }
    }
    public void EnterAbiAir()
    {
        if (dco.learnAir)
        {
            panelDesc.SetActive(true);
            textDesc.text = "Propel yourself skyward, granting yourself the Evasive status for 3 turns.";
            textDesc.text += "\n\nSpeed: Fast                 |                 Cost: 15 DP";
        }
    }
    public void EnterAbiJump()
    {
        if (dco.learnJump)
        {
            panelDesc.SetActive(true);
            textDesc.text = "Leap up and come crashing down, dealing heavy damage to all foes.";
            textDesc.text += "\n\nSpeed: Slow                 |                 Cost: 20 DP";
        }
    }
    public void EnterAbiBarrier()
    {
        if (dco.learnBarrier)
        {
            panelDesc.SetActive(true);
            textDesc.text = "Produce a defensive shield, granting yourself the Armored status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 30 DP";
        }
    }
    public void EnterItemMeatS()
    {
        if (dco.invMeatS > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "A small morsel of crab meat. Restores 15 HP and 10 DP.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 1 Item";
        }
        else if (dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(true);
            textDesc.text = "A small morsel of crab meat. Restores 15 HP and 10 DP.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 5 gp";
        }
    }
    public void EnterItemMeatM()
    {
        if (dco.invMeatM > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "A morsel of crab meat. Restores 30 HP and 30 DP.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 1 Item";
        }
        else if (dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(true);
            textDesc.text = "A morsel of crab meat. Restores 30 HP and 30 DP.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 10 gp";
        }
    }
    public void EnterItemMeatL()
    {
        if (dco.invMeatL > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "A lavish morsel of crab meat. Restores 50 HP and 50 DP.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 1 Item";
        }
        else if (dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(true);
            textDesc.text = "A lavish morsel of crab meat. Restores 50 HP and 50 DP.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 20 gp";
        }
    }
    public void EnterItemMeatXL()
    {
        if (dco.invMeatXL > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "A sandwich practically brimming with crab meat. Fully restores HP and DP.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 1 Item";
        }
        else if (dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(true);
            textDesc.text = "A sandwich practically brimming with crab meat. Fully restores HP and DP.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 50 gp";
        }
    }
    public void EnterItemPow()
    {
        if (dco.invPow > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "The shattered claw of a Holey Crab. Grants the Empowered status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 1 Item";
        }
        else if (dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(true);
            textDesc.text = "The shattered claw of a Holey Crab. Grants the Empowered status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 30 gp";
        }
    }
    public void EnterItemDef()
    {
        if (dco.invDef > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "It's got bits of metal and wiring in it... Restores 30 HP and 30 DP and grants the Armored status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 1 Item";
        }
        else if (dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(true);
            textDesc.text = "It's got bits of metal and wiring in it... Restores 30 HP and 30 DP and grants the Armored status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 40 gp";
        }
    }
    public void EnterItemSpeed()
    {
        if (dco.invSpeed > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "A set of lucky Crabbit ears. Grants the Hastened status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 1 Item";
        }
        else if (dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(true);
            textDesc.text = "A set of lucky Crabbit ears. Grants the Hastened status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 30 gp";
        }
    }
    public void EnterItemSpeed2()
    {
        if (dco.invSpeed2 > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "A sushi roll with plenty of succulent crab meat. Restores 50 HP and 50 DP and grants the Hastened status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 1 Item";
        }
        else if (dco.enemyType == "Lincrab")
        {
            panelDesc.SetActive(true);
            textDesc.text = "A sushi roll with plenty of succulent crab meat. Restores 50 HP and 50 DP and grants the Hastened status for 3 turns.";
            textDesc.text += "\n\nSpeed: Very Fast                 |                 Cost: 70 gp";
        }
    }
    public void EnterItemMagic()
    {
        if (dco.invMagic > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "Some hints of magic linger in this discarded Crab Witch hat. Attempts to cast a random Crab Spell.";
            textDesc.text += "\n\nSpeed: Varies                 |                 Cost: 1 Item";
        }
    }
    public void EnterItemCheat()
    {
        if (dco.invCheat > 0)
        {
            panelDesc.SetActive(true);
            textDesc.text = "This flavor... It's overwhelming! This should keep your suit powered almost indefinitely!";
            textDesc.text += "\n\nNot consumed when used.";
            textDesc.text += "\n\nSpeed: Instant                 |                 Cost: None";
        }
    }

    public void ExitAny()
    {
        panelDesc.SetActive(false);
    }

    public void EnterPlayer()
    {
        if (!tbm.freezeTimeline || tbm.isPlayersTurn)
        {
            glowPlayer.SetActive(true);
            bool noBuffs = true;
            bool isFirst = true;
            textPlayerNL.text = "Max - Level " + dco.playerLevel;
            string newStatus = "";
            string newDesc = "\n\n";
            if (tbm.turnsPowPlayer > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Empowered - " + tbm.turnsPowPlayer + " Turns Left";
                newDesc += "+50% outgoing damage";
            }
            if (tbm.turnsDefPlayer > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Armored - " + tbm.turnsDefPlayer + " Turns Left";
                newDesc += "-50% incoming damage + Interruption immunity";
            }
            if (tbm.turnsSpeedPlayer > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Hastened - " + tbm.turnsSpeedPlayer + " Turns Left";
                newDesc += "+50% speed on timeline + boosted cast speed";
            }
            if (tbm.turnsEvadePlayer > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Evasive - " + tbm.turnsEvadePlayer + " Turns Left";
                newDesc += "50% chance to avoid incoming damage";
            }
            if (noBuffs)
            {
                newStatus = "None";
                newDesc = "";
            }
            if (tbm.isOverdrive)
            {
                panelOverdrive.SetActive(true);
            }
            else
            {
                panelOverdrive.SetActive(false);
            }
            textPlayerStatus.text = newStatus;
            textPlayerDesc.text = newDesc;
            panelPlayer.SetActive(true);
        }
    }

    public void EnterE1()
    {
        if ((!tbm.freezeTimeline || tbm.isPlayersTurn) && tbm.enemy1.gameObject.activeSelf)
        {
            glowE1.SetActive(true);
            bool noBuffs = true;
            bool isFirst = true;
            int enLevel;
            if (dco.playerLevel >= 5)
            {
                enLevel = dco.playerLevel / 5 * 5;
            }
            else
            {
                enLevel = 1;
            }
            textE1NL.text = tbm.iconE1.forecastName + " - Level " + enLevel;
            string newStatus = "";
            string newDesc = "\n\n";
            if (tbm.turnsPowE1 > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Empowered - " + tbm.turnsPowE1 + " Turns Left";
                newDesc += "+50% outgoing damage";
            }
            if (tbm.turnsDefE1 > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Armored - " + tbm.turnsDefE1 + " Turns Left";
                newDesc += "-50% incoming damage + Interruption immunity";
            }
            if (tbm.turnsSpeedE1 > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Hastened - " + tbm.turnsSpeedE1 + " Turns Left";
                newDesc += "+50% speed on timeline + boosted cast speed";
            }
            if (tbm.turnsEvadeE1 > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Evasive - " + tbm.turnsEvadeE1 + " Turns Left";
                newDesc += "50% chance to avoid incoming damage";
            }
            if (noBuffs)
            {
                newStatus = "None";
                newDesc = "";
            }
            textE1Status.text = newStatus;
            textE1Desc.text = newDesc;
            panelE1.SetActive(true);
        }
    }

    public void EnterE2()
    {
        if ((!tbm.freezeTimeline || tbm.isPlayersTurn) && tbm.enemy2.gameObject.activeSelf)
        {
            glowE2.SetActive(true);
            bool noBuffs = true;
            bool isFirst = true;
            int enLevel;
            if (dco.playerLevel >= 5)
            {
                enLevel = dco.playerLevel / 5 * 5;
            }
            else
            {
                enLevel = 1;
            }
            textE2NL.text = tbm.iconE2.forecastName + " - Level " + enLevel;
            string newStatus = "";
            string newDesc = "\n\n";
            if (tbm.turnsPowE2 > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Empowered - " + tbm.turnsPowE2 + " Turns Left";
                newDesc += "+50% outgoing damage";
            }
            if (tbm.turnsDefE2 > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Armored - " + tbm.turnsDefE2 + " Turns Left";
                newDesc += "-50% incoming damage + Interruption immunity";
            }
            if (tbm.turnsSpeedE2 > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Hastened - " + tbm.turnsSpeedE2 + " Turns Left";
                newDesc += "+50% speed on timeline + boosted cast speed";
            }
            if (tbm.turnsEvadeE2 > 0)
            {
                noBuffs = false;
                if (!isFirst)
                {
                    newStatus += "\n\n\n";
                    newDesc += "\n\n\n";
                }
                else
                {
                    isFirst = false;
                }
                newStatus += "Evasive - " + tbm.turnsEvadeE2 + " Turns Left";
                newDesc += "50% chance to avoid incoming damage";
            }
            if (noBuffs)
            {
                newStatus = "None";
                newDesc = "";
            }
            textE2Status.text = newStatus;
            textE2Desc.text = newDesc;
            panelE2.SetActive(true);
        }
    }

    public void ExitMousePanels()
    {
        glowPlayer.SetActive(false);
        glowE1.SetActive(false);
        glowE2.SetActive(false);
        panelPlayer.SetActive(false);
        panelE1.SetActive(false);
        panelE2.SetActive(false);
    }
}
