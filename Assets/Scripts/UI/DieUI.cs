using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DieUI : MonoBehaviour
{
    private SaveHandler saveHandler;
    private Animator sceneTrans;
    private GameObject dco;
    private SoundManager sm;

    private void Start()
    {
        sceneTrans = GameObject.FindGameObjectWithTag("LevelLoader").transform.GetChild(0).GetComponent<Animator>();
        dco = GameObject.FindGameObjectWithTag("CarryOver");
        sm = dco.GetComponent<SoundManager>();
    }

    public void LoadGame()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        saveHandler.LoadSaveScene();
    }

    public void Quit()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundButton);
        Debug.Log("Pressed Quit");
        StartCoroutine("QuitPress");
    }

    IEnumerator QuitPress()
    {
        Debug.Log("Received Call");
        sceneTrans.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Waited");
        sm.musicPlayer.Stop();
        Destroy(dco);
        SceneManager.LoadScene("MainMenu");
    }
}
