using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DemoOverButtons : MonoBehaviour
{
    public GameObject buttonMenu, buttonQuit;
    public Image fadeColor;
    public Text endText;
    public AudioSource sfxPlayer;
    public Animator sceneTrans;

    private void Awake()
    {
        fadeColor.color = Color.white;
    }

    public void MenuButton()
    {
        sfxPlayer.Play();
        endText.text = "Going back...";
        endText.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        buttonMenu.SetActive(false);
        buttonQuit.SetActive(false);
        StartCoroutine("MenuFade");
    }

    public void QuitButton()
    {
        sfxPlayer.Play();
        endText.text = "See you next time!";
        endText.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        buttonMenu.SetActive(false);
        buttonQuit.SetActive(false);
        StartCoroutine("QuitFade");
    }

    IEnumerator MenuFade()
    {
        fadeColor.color = Color.black;
        sceneTrans.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator QuitFade()
    {
        fadeColor.color = Color.black;
        sceneTrans.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        Application.Quit();
    }
}
