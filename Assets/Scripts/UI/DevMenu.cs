using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevMenu : MonoBehaviour
{
    private DataCarryOver dco;
    private SoundManager sm;
    private PlayerStats ps;
    private PlayerLevel pl;
    private LevelLoader ll;
    private UIController uc;

    // Start is called before the first frame update
    void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.gameObject.GetComponent<SoundManager>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        pl = ps.gameObject.GetComponent<PlayerLevel>();
        ll = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        uc = GameObject.FindGameObjectWithTag("UIPanel").GetComponent<UIController>();
    }

    public void Items()
    {
        ps.playerHealth = ps.playerMaxHealth;
        ps.playerDrive = ps.playerMaxDrive;
        ps.UpdateBars();
        ps.EarnGold(1000);
        dco.playerMoney = 1000;

        Player player = ps.GetComponent<Player>();

        player.LearnAir();
        player.LearnBarrier();
        player.LearnDash();
        player.LearnPowerJump();
        ps.overdriveLocked = false;
        dco.overdriveLocked = false;
        dco.invMeatS = 5;
        dco.invMeatM = 5;
        dco.invMeatL = 5;
        dco.invMeatXL = 5;
        dco.invPow = 5;
        dco.invDef = 5;
        dco.invSpeed = 5;
        dco.invSpeed2 = 5;
        dco.invMagic = 5;
        dco.invCheat = 5;
        Inventory.instance.PopulateFromDCO();
    }

    public void Skip()
    {
        ps.overdriveLocked = false;
        dco.overdriveLocked = false;
        uc.ResetPanels();
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Level1")
        {
            ps.gameObject.transform.position = new Vector3(-54.0f, 3.0f, 0.0f);
        }
        else if (sceneName == "Level2")
        {
            ps.gameObject.transform.position = new Vector3(78.0f, 1.5f, 0.0f);
        }
        else if (sceneName == "Level3")
        {
            ps.gameObject.transform.position = new Vector3(99.0f, 9.5f, 0.0f);
        }
        else if (sceneName == "Level5" || sceneName == "Level6")
        {
            ps.gameObject.transform.position = new Vector3(330.0f, 2.0f, 16.0f);
        }
        else if (sceneName == "Level7")
        {
            ps.gameObject.transform.position = new Vector3(330.0f, 2.0f, 0.0f);
        }
        else if (sceneName == "Level9" || sceneName == "Level10" || sceneName == "Level11" || sceneName == "Level13" || sceneName == "Level14" || sceneName == "Level15")
        {
            Debug.Log("Trying a direct skip");
            dco.ResetCarryOver();
            ll.LoadNewLevel(NameOfSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
        }
        else
        {
            Debug.Log("Can't skip from " + sceneName);
        }
        /*
        else if (name == "Level10")
        {
            ps.gameObject.transform.position = new Vector3(78.0f, 1.5f, 0.0f);
        }
        else if (name == "Level11")
        {
            ps.gameObject.transform.position = new Vector3(78.0f, 1.5f, 0.0f);
        }
        else if (name == "Level13")
        {
            ps.gameObject.transform.position = new Vector3(78.0f, 1.5f, 0.0f);
        }
        else if (name == "Level14")
        {
            ps.gameObject.transform.position = new Vector3(78.0f, 1.5f, 0.0f);
        }
        else if (name == "Level15")
        {
            ps.gameObject.transform.position = new Vector3(78.0f, 1.5f, 0.0f);
        }*/
        /*
        string sceneName = SceneManager.GetActiveScene().name;
        if (name != "Crapsolute0Fight" || name != "Level8" || name != "Level12" || name != "Level16")
        {
            ps.overdriveLocked = false;
            dco.overdriveLocked = false;
            dco.ResetCarryOver();
            uc.ResetPanels();
            ll.LoadNewLevel(NameOfSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
        }*/
    }

    private string NameOfSceneByBuildIndex(int buildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    public void GiveExp()
    {
        pl.AddExperience(1000000);
    }

    /*
    public void Skip1()
    {
        ps.gameObject.transform.position = new Vector3(-54.0f, 3.0f, 0.0f);
        ps.overdriveLocked = false;
    }

    public void Skip2()
    {
        ps.gameObject.transform.position = new Vector3(78.0f, 1.5f, 0.0f);
    }

    public void Skip3()
    {
        ps.gameObject.transform.position = new Vector3(99.0f, 9.5f, 0.0f);
    }*/

    public void BossDefeat()
    {
        dco.bossHP = 10;
    }
}
