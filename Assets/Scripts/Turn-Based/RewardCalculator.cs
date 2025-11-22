using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardCalculator : MonoBehaviour
{
    public Text expMonText, rewardText;
    private int rewardXP, rewardMoney, rewardMeatS, rewardMeatM, rewardMeatL, rewardMeatXL, rewardPow, rewardDef, rewardSpeed, rewardSpeed2, rewardMagic, rewardCheat;
    private DataCarryOver dco;

    // Start is called before the first frame update
    void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        rewardXP = 0;
        rewardMoney = 0;
        rewardMeatS = 0;
        rewardMeatM = 0;
        rewardMeatL = 0;
        rewardMeatXL = 0;
        rewardPow = 0;
        rewardDef = 0;
        rewardSpeed = 0;
        rewardSpeed2 = 0;
        rewardMagic = 0;
        rewardCheat = 0;
    }

    //Adds drops from defeating specific enemies to potential rewards list
    public void GenerateRewards(string type, float chance, int num)
    {
        if (type == "XP")
        {
            rewardXP += num;
        }
        else if (type == "Money")
        {
            rewardMoney += num;
        }
        else
        {
            int trueNum = 0;
            for (int i = 0; i < num; i++)
            {
                if (Random.Range(0.0f, 1.0f) <= chance)
                {
                    trueNum++;
                }
            }
            switch(type)
            {
                case "MeatS": rewardMeatS += trueNum; break;
                case "MeatM": rewardMeatM += trueNum; break;
                case "MeatL": rewardMeatL += trueNum; break;
                case "MeatXL": rewardMeatXL += trueNum; break;
                case "Pow": rewardPow += trueNum; break;
                case "Def": rewardDef += trueNum; break;
                case "Speed": rewardSpeed += trueNum; break;
                case "Speed2": rewardSpeed2 += trueNum; break;
                case "Magic": rewardMagic += trueNum; break;
                case "Cheat": rewardCheat += trueNum; break;
                default: break;
            }
        }
    }

    //Puts rewards list into DataCarryOver/Inventory; not called if player runs away/is defeated
    public void PushRewards()
    {
        expMonText.text = "+" + rewardXP + " Exp.\n+" + rewardMoney + " Gold";
        dco.playerExp += rewardXP;
        dco.playerMoney += rewardMoney;

        int numRewards = 0;
        rewardText.text = "Rewards:";
        if (rewardCheat > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardCheat + "x Crab Puff Supreme";
            }
            numRewards++;
            dco.invCheat += rewardCheat;
        }
        if (rewardMagic > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardMagic + "x Witch's Hat";
            }
            numRewards++;
            dco.invMagic += rewardMagic;
        }
        if (rewardSpeed2 > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardSpeed2 + "x Crab Roll";
            }
            numRewards++;
            dco.invSpeed2 += rewardSpeed2;
        }
        if (rewardSpeed > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardSpeed + "x Crabbit Ears";
            }
            numRewards++;
            dco.invSpeed += rewardSpeed;
        }
        if (rewardDef > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardDef + "x Robo Crab Meat";
            }
            numRewards++;
            dco.invDef += rewardDef;
        }
        if (rewardPow > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardPow + "x Broken Claw";
            }
            numRewards++;
            dco.invPow += rewardPow;
        }
        if (rewardMeatXL > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardMeatXL + "x Sandwich";
            }
            numRewards++;
            dco.invMeatXL += rewardMeatXL;
        }
        if (rewardMeatL > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardMeatL + "x Crab Meat L";
            }
            numRewards++;
            dco.invMeatL += rewardMeatL;
        }
        if (rewardMeatM > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardMeatM + "x Crab Meat M";
            }
            numRewards++;
            dco.invMeatM += rewardMeatM;
        }
        if (rewardMeatS > 0)
        {
            if (numRewards < 4)
            {
                rewardText.text += "\n" + rewardMeatS + "x Crab Meat S";
            }
            numRewards++;
            dco.invMeatS += rewardMeatS;
        }
        if (numRewards >= 4)
        {
            rewardText.text += "\n+" + (numRewards - 3) + " More...";
        }
        if (numRewards == 0)
        {
            rewardText.text += "\nNone";
        }
    }
}
