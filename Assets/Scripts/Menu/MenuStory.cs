using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuStory : MonoBehaviour
{
    public bool isInLevel = false;
    public string loadLevelName;
    public float delaySec;
    public Animator sceneTrans;
    public GameObject blackBG;
    public GameObject skipButton;
    public GameObject[] storyScreens;

    public bool storyFinished = false;
    private bool isStarting = false;

    public void DoStory()
    {
        StartCoroutine("Story");
    }

    public void SkipStory()
    {
        StopCoroutine("Story");
        if (!isInLevel)
        {
            StartCoroutine("GoToLevel");
        }
        else
        {
            StartCoroutine("EndInLevel");
        }
    }

    IEnumerator Story()
    {
        for (int i = 0; i < storyScreens.Length; i++)
        {
            if (i == 0)
            {
                //if (!isInLevel)
                //{
                    sceneTrans.SetTrigger("Start");
                    isStarting = true;
                    yield return new WaitForSeconds(1.0f);
                    isStarting = false;
                //}
                blackBG.SetActive(true);
                skipButton.SetActive(true);
                storyScreens[0].SetActive(true);
                //if (!isInLevel)
                //{
                    sceneTrans.SetTrigger("FadeIn");
                //}
                yield return new WaitForSeconds(1.0f + delaySec);
            }
            else
            {
                sceneTrans.SetTrigger("Start");
                isStarting = true;
                yield return new WaitForSeconds(1.0f);
                isStarting = false;
                storyScreens[i - 1].SetActive(false);
                storyScreens[i].SetActive(true);
                sceneTrans.SetTrigger("FadeIn");
                yield return new WaitForSeconds(1.0f + delaySec);
            }
        }

        if (!isInLevel)
        {
            StartCoroutine("GoToLevel");
        }
        else
        {
            StartCoroutine("EndInLevel");
        }
    }

    IEnumerator GoToLevel()
    {
        skipButton.SetActive(false);
        sceneTrans.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(loadLevelName);
    }

    IEnumerator EndInLevel()
    {
        skipButton.SetActive(false);
        if (!isStarting)
        {
            sceneTrans.SetTrigger("Start");
        }
        yield return new WaitForSeconds(1.0f);
        blackBG.SetActive(false);
        foreach (GameObject g in storyScreens)
        {
            g.SetActive(false);
        }
        sceneTrans.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1.0f);
        storyFinished = true;
    }
}
