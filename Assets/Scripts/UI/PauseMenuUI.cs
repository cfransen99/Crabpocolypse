using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    public GameObject panelInstruct, panelShop, moneyPanel, panelQuit;
    public Button quitButtonYes, quitButtonNo;

    private UIController parentUI;
    private SaveHandler saveHandler;

    private void Start()
    {
        parentUI = GetComponentInParent<UIController>();
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
    }

    public void Resume()
    {
        parentUI.ResetPanels();
    }

    public void LoadGame()
    {
        parentUI.ResetPanels();
        saveHandler.LoadSaveScene();
    }

    public void Instructions()
    {
        panelInstruct.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Shop()
    {
        panelShop.SetActive(true);
        gameObject.SetActive(false);
        moneyPanel.SetActive(true);
    }

    public void Options()
    {
        //Options
    }

    public void Quit()
    {
        quitButtonYes.interactable = true;
        quitButtonNo.interactable = true;
        panelQuit.SetActive(true);
    }

    public void QuitYes()
    {
        quitButtonYes.interactable = false;
        quitButtonNo.interactable = false;
        Destroy(GameObject.FindGameObjectWithTag("SaveHandler"));
        Destroy(GameObject.FindGameObjectWithTag("CarryOver"));
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitNo()
    {
        panelQuit.SetActive(false);
    }
}
