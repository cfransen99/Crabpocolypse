using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public int level, skillPoints = 0;
    [SerializeField] public int experience = 0;
    
    public int experienceToNextLevel = 30;
    public int baseExperience = 30;
    public float experienceScaleMultiplier;

    [SerializeField] private Image levelUI;
    [SerializeField] private Text levelText;

    [SerializeField] private int num;

    private DataCarryOver dco;

    private SaveHandler saveHandler;

    private EnemyBase[] enemies;

    private void Awake()
    {
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();

        if(!saveHandler.GetDidLoadSave())
        {
            dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
            if (dco.XPCap != 0)
            {
                experienceToNextLevel = dco.XPCap;
            }
            level = dco.playerLevel;
            skillPoints = dco.playerPoints;
            AddExperience(dco.playerExp);
        }
    }

    private void Update()
    {
        //Fills the XP Bar and displays player level
        levelUI.fillAmount = GetExperience();
        levelText.text = level.ToString();

        /*
        if(Input.GetKeyDown(KeyCode.F))
        {
            AddExperience(num);
        }*/
    }

    //Add XP to the players experience and if the player levels up it calculates what their new XP to level up is
    public void AddExperience(int amount)
    {
        experience += amount;
        while (experience >= experienceToNextLevel)
        {
            level++;
            skillPoints++;
            experience -= experienceToNextLevel;
            experienceToNextLevel = (int)(baseExperience * (Mathf.Pow(experienceScaleMultiplier, level - 1)));

            enemies = FindObjectsOfType<EnemyBase>();

            foreach (EnemyBase enemy in enemies)
            {
                if(enemy != null)
                {
                    enemy.UpdateLevel(level);
                }
            }
        }
    }

    //Gets players experience out of the amount they need to level up
    public float GetExperience()
    {
        return (float)experience / experienceToNextLevel;
    }

    public void UpdateLevel()
    {
        while (experience >= experienceToNextLevel)
        {
            level++;
            skillPoints++;
            experience -= experienceToNextLevel;
            experienceToNextLevel = (int)(baseExperience * (Mathf.Pow(experienceScaleMultiplier, level - 1)));
        }
    }
}
