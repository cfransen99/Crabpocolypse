using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoxes : MonoBehaviour
{
    public TurnBasedManager tbm;
    public GameObject buttonNext, buttonPrev, buttonClose, panelHub;
    public int tutorialID;
    public GameObject[] pages;

    private int currentPage = 1;
    private SoundManager sm;
    private DataCarryOver dco;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
        dco = sm.gameObject.GetComponent<DataCarryOver>();
    }

    public void NextPage()
    {
        pages[currentPage - 1].SetActive(false);
        currentPage++;
        pages[currentPage - 1].SetActive(true);
        buttonPrev.SetActive(true);
        if (currentPage == pages.Length)
        {
            buttonClose.SetActive(true);
            buttonNext.SetActive(false);
        }
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
    }

    public void PrevPage()
    {
        pages[currentPage - 1].SetActive(false);
        currentPage--;
        pages[currentPage - 1].SetActive(true);
        buttonNext.SetActive(true);
        buttonClose.SetActive(false);
        if (currentPage == 1)
        {
            buttonPrev.SetActive(false);
        }
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
    }

    public void Close()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        pages[currentPage - 1].SetActive(false);
        currentPage = 1;
        pages[currentPage - 1].SetActive(true);
        buttonPrev.SetActive(false);
        buttonClose.SetActive(false);
        buttonNext.SetActive(true);
        if (!tbm.inTutorialHub)
        {
            dco.tutorialsUnlocked[tutorialID] = true;
            if (!tbm.battleOver)
            {
                tbm.freezeTimeline = false;
                tbm.panelPlayerDat.SetActive(true);
                if (!tbm.PlayerHasDrive() || dco.overdriveLocked)
                {
                    tbm.panelDisable.SetActive(true);
                }
            }
            gameObject.SetActive(false);
            tbm.CheckForTutorials();
        }
        else
        {
            panelHub.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
