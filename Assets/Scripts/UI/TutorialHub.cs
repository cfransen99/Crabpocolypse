using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHub : MonoBehaviour
{
    public GameObject[] tutorialPanel;
    public TurnBasedManager tbm;
    public Text[] textTutorial;
    public string[] textTutName;

    private GameObject activePanel;
    private DataCarryOver dco;
    private SoundManager sm;

    private void Awake()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.gameObject.GetComponent<SoundManager>();
    }

    public void OpenHub()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        UpdateTutorialText();
        tbm.freezeTimeline = true;
        tbm.inTutorialHub = true;
        tbm.panelPlayerDat.SetActive(false);
        tbm.panelDisable.SetActive(false);
        tbm.panelDesc.SetActive(false);
        tbm.panelForecast.SetActive(false);

        if (tbm.isPlayersTurn)
        {
            if (tbm.panelMain.activeSelf)
            {
                tbm.panelMain.SetActive(false);
                activePanel = tbm.panelMain;
            }
            else if (tbm.panelTarget.activeSelf)
            {
                tbm.panelTarget.SetActive(false);
                activePanel = tbm.panelTarget;
            }
            else if (tbm.panelAbility.activeSelf)
            {
                tbm.panelAbility.SetActive(false);
                activePanel = tbm.panelAbility;
            }
            else if (tbm.panelItem.activeSelf)
            {
                tbm.panelItem.SetActive(false);
                activePanel = tbm.panelItem;
            }
            else if (tbm.panelIRecover.activeSelf)
            {
                tbm.panelIRecover.SetActive(false);
                activePanel = tbm.panelIRecover;
            }
            else if (tbm.panelIStatus.activeSelf)
            {
                tbm.panelIStatus.SetActive(false);
                activePanel = tbm.panelIStatus;
            }
            else if (tbm.panelIOther.activeSelf)
            {
                tbm.panelIOther.SetActive(false);
                activePanel = tbm.panelIOther;
            }
        }
    }

    public void UpdateTutorialText()
    {
        for (int i = 0; i < dco.tutorialsUnlocked.Length; i++)
        {
            if (dco.tutorialsUnlocked[i])
            {
                textTutorial[i].text = textTutName[i];
            }
            else
            {
                textTutorial[i].text = "???";
            }
        }
    }

    public void PressTutorial1()
    {
        if (dco.tutorialsUnlocked[0])
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tutorialPanel[0].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void PressTutorial2()
    {
        if (dco.tutorialsUnlocked[1])
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tutorialPanel[1].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void PressTutorial3()
    {
        if (dco.tutorialsUnlocked[2])
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tutorialPanel[2].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void PressTutorial4()
    {
        if (dco.tutorialsUnlocked[3])
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tutorialPanel[3].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void PressTutorial5()
    {
        if (dco.tutorialsUnlocked[4])
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tutorialPanel[4].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void PressTutorial6()
    {
        if (dco.tutorialsUnlocked[5])
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tutorialPanel[5].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void PressTutorial7()
    {
        if (dco.tutorialsUnlocked[6])
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tutorialPanel[6].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void PressTutorial8()
    {
        if (dco.tutorialsUnlocked[7])
        {
            sm.sfxPlayer.PlayOneShot(sm.soundButton);
            tutorialPanel[7].SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ExitHub()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        tbm.freezeTimeline = false;
        tbm.inTutorialHub = false;
        tbm.panelPlayerDat.SetActive(true);
        if (!tbm.PlayerHasDrive() || dco.overdriveLocked)
        {
            tbm.panelDisable.SetActive(true);
        }

        if (tbm.isPlayersTurn)
        {
            tbm.freezeTimeline = true;
            activePanel.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
