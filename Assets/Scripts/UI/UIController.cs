using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject[] defaultPanels;
    public GameObject losePanel;
    public GameObject pausePanel;
    public GameObject skillTreePanel;
    public GameObject dialoguePanel;
    public GameObject skillButton;
    public Text skillButtonText;
    public GameObject instructPanel;

    public GameObject devModePanel;

    public bool isPause, useSkillTree, dialogueActive;
    
    private DataCarryOver dco;
    private SoundManager sm;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.gameObject.GetComponent<SoundManager>();
        CheckSkillPoints();
    }

    public void DiePanel()
    {
        foreach(GameObject i in defaultPanels)
        {
            i.SetActive(false);
        }
        losePanel.SetActive(true);
    }

    public void SkillTreePanel()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        pausePanel.SetActive(false);
        foreach (GameObject i in defaultPanels)
        {
            i.SetActive(false);
        }
        skillTreePanel.SetActive(true);
        skillButton.SetActive(false);

        useSkillTree = true;

        Time.timeScale = 0;
    }

    public void PausePanel()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        foreach (GameObject i in defaultPanels)
        {
            i.SetActive(false);
        }
        skillTreePanel.SetActive(false);
        pausePanel.SetActive(true);

        isPause = true;

        Time.timeScale = 0;
    }

    public void DialoguePanel()
    {
        foreach (GameObject i in defaultPanels)
        {
            i.SetActive(false);
        }
        skillTreePanel.SetActive(false);
        dialoguePanel.SetActive(true);

        dialogueActive = true;
    }

    public void ResetPanels()
    {
        if(sm != null)
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
        }
        foreach (GameObject i in defaultPanels)
        {
            if(i.name == "SkillTreePanel" || i.name == "InventoryPanel" || i.name == "ShopPanel" || i.name == "QuitPanel")
            {
                i.SetActive(false);
            }
            else
            {
                i.SetActive(true);
            }
        }
        losePanel.SetActive(false);
        pausePanel.SetActive(false);
        skillTreePanel.SetActive(false);
        dialoguePanel.SetActive(false);
        instructPanel.SetActive(false);
        
        if(dco != null)
        {
            CheckSkillPoints();
        }

        useSkillTree = false;
        isPause = false;

        Time.timeScale = 1;
    }

    public void ActivateDevMode()
    {
        devModePanel.SetActive(true);
    }

    private void CheckSkillPoints()
    {
        if (dco.playerPoints > 0)
        {
            skillButton.SetActive(true);
            if (dco.playerPoints > 1)
            {
                skillButtonText.text = dco.playerPoints + " Unspent Skill Point(s)";
            }
            else
            {
                skillButtonText.text = "1 Unspent Skill Points";
            }
        }
        else
        {
            skillButton.SetActive(false);
        }
    }
}
