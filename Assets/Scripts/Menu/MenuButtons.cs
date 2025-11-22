using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject labelTitle, labelSub, buttonNew, buttonLoad, buttonCred, buttonQuit, buttonQuitY, buttonQuitN, panelCred, panelQuit, panelNew, panelLoad;
    public Text quitText;
    public AudioSource sfxPlayer;
    public Animator sceneTrans;
    public MenuStory ms;

    private SaveHandler saveHandler;

    // Start is called before the first frame update
    void Start()
    {
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        Time.timeScale = 1;
    }

    public void NewGameButton()
    {
        sfxPlayer.Play();
        labelTitle.SetActive(false);
        labelSub.SetActive(false);
        buttonNew.SetActive(false);
        buttonLoad.SetActive(false);
        buttonCred.SetActive(false);
        buttonQuit.SetActive(false);
        panelNew.SetActive(true);
    }

    public void NewYesButton()
    {
        sfxPlayer.Play();
        saveHandler.DeleteFiles();
        ms.DoStory();
    }

    public void NewNoButton()
    {
        sfxPlayer.Play();
        labelTitle.SetActive(true);
        labelSub.SetActive(true);
        buttonNew.SetActive(true);
        buttonLoad.SetActive(true);
        buttonCred.SetActive(true);
        buttonQuit.SetActive(true);
        panelNew.SetActive(false);
    }

    public void LoadGameButton()
    {
        sfxPlayer.Play();
        if (CheckForSave())
        {
            StartCoroutine("LoadGameFade");
        }
        else
        {
            labelTitle.SetActive(false);
            labelSub.SetActive(false);
            buttonNew.SetActive(false);
            buttonLoad.SetActive(false);
            buttonCred.SetActive(false);
            buttonQuit.SetActive(false);
            panelLoad.SetActive(true);
        }
    }

    private bool CheckForSave()
    {
        bool found = false;
        if (File.Exists(Application.dataPath + "/Saves/playerSave.txt"))
        {
            found = true;
        }
        return found;
    }

    public void LoadBackButton()
    {
        sfxPlayer.Play();
        labelTitle.SetActive(true);
        labelSub.SetActive(true);
        buttonNew.SetActive(true);
        buttonLoad.SetActive(true);
        buttonCred.SetActive(true);
        buttonQuit.SetActive(true);
        panelLoad.SetActive(false);
    }

    public void CreditsButton()
    {
        sfxPlayer.Play();
        labelTitle.SetActive(false);
        labelSub.SetActive(false);
        buttonNew.SetActive(false);
        buttonLoad.SetActive(false);
        buttonCred.SetActive(false);
        buttonQuit.SetActive(false);
        panelCred.SetActive(true);
    }

    public void CreditsBackButton()
    {
        sfxPlayer.Play();
        labelTitle.SetActive(true);
        labelSub.SetActive(true);
        buttonNew.SetActive(true);
        buttonLoad.SetActive(true);
        buttonCred.SetActive(true);
        buttonQuit.SetActive(true);
        panelCred.SetActive(false);
    }

    public void QuitButton()
    {
        sfxPlayer.Play();
        labelTitle.SetActive(false);
        labelSub.SetActive(false);
        buttonNew.SetActive(false);
        buttonLoad.SetActive(false);
        buttonCred.SetActive(false);
        buttonQuit.SetActive(false);
        panelQuit.SetActive(true);
    }

    public void QuitYesButton()
    {
        sfxPlayer.Play();
        quitText.text = "See you next time!";
        quitText.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        buttonQuitY.SetActive(false);
        buttonQuitN.SetActive(false);
        StartCoroutine("QuitFade");
    }

    public void QuitNoButton()
    {
        sfxPlayer.Play();
        labelTitle.SetActive(true);
        labelSub.SetActive(true);
        buttonNew.SetActive(true);
        buttonLoad.SetActive(true);
        buttonCred.SetActive(true);
        buttonQuit.SetActive(true);
        panelQuit.SetActive(false);
    }

    /*IEnumerator NewGameFade()
    {
        sceneTrans.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);

        saveHandler.DeleteFiles();
        SceneManager.LoadScene("Level1");
    }*/

    IEnumerator LoadGameFade()
    {
        sceneTrans.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);

        saveHandler.LoadSaveScene();
    }

    IEnumerator QuitFade()
    {
        sceneTrans.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        Application.Quit();
    }
}
