using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZeroExplodeController : MonoBehaviour
{
    public float smallSize, bigSize;
    public Color fadeColor, fullColor;

    private Image screenColor;
    private Animator sceneTrans;
    private float killTimer = 0.0f;
    private Vector3 startSize, endSize;
    private Renderer rend;
    private SoundManager sm;
    private DataCarryOver dco;

    // Start is called before the first frame update
    void Start()
    {
        startSize = new Vector3(smallSize, smallSize, smallSize);
        endSize = new Vector3(bigSize, bigSize, bigSize);

        GameObject temp = GameObject.FindGameObjectWithTag("LevelLoader");
        sceneTrans = temp.transform.GetChild(0).GetComponent<Animator>();
        screenColor = temp.transform.GetChild(0).GetChild(0).GetComponent<Image>();
        rend = gameObject.GetComponent<Renderer>();
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
        dco = sm.GetComponent<DataCarryOver>();
        StartCoroutine("DemoOver");
    }

    // Update is called once per frame
    void Update()
    {
        if (killTimer <= 1.85f)
        {
            killTimer += Time.deltaTime;
        }
        else
        {
            gameObject.transform.localScale = endSize;
        }

        if (killTimer > 1.5f)
        {
            gameObject.transform.localScale = Vector3.Lerp(endSize / 4, endSize, (killTimer - 1.5f) * (1.0f / 0.35f));
            rend.material.color = Color.Lerp(fadeColor, fullColor, killTimer * (1.0f / 1.75f));
        }
        else if (killTimer <= 1.5f)
        {
            gameObject.transform.localScale = Vector3.Lerp(startSize, endSize / 4, killTimer * (1.0f / 2.0f));
            rend.material.color = Color.Lerp(fadeColor, fullColor, killTimer * (1.0f / 3.5f));
        }
    }

    IEnumerator DemoOver()
    {
        dco.ResetCarryOver();
        screenColor.color = Color.white;
        yield return new WaitForSeconds(2f);
        sceneTrans.SetTrigger("Start");
        sm.sfxPlayer.PlayOneShot(sm.soundExplode);
        yield return new WaitForSeconds(1.0f);
        if (SceneManager.GetActiveScene().name == "Level16")
        {
            Destroy(dco.gameObject);
        }
        else
        {
            sm.musicPlayer.clip = sm.musicLevel;
            sm.musicPlayer.Play();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
