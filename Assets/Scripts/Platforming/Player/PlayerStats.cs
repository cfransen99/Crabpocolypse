using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float playerHealth, playerMaxHealth;
    public float playerDrive, playerMaxDrive;

    public int playerAttack, playerSpeed, playerDefense;
    public int initPlayerDefense;
    public int gold;

    public int driveDrain;

    public bool isVulnerable = true;
    private bool isOverdrive = false;
    public bool overdriveLocked;

    IEnumerator overdriveCorountine;

    public Image playerDriveBar;
    public Image playerHealthBar;

    public Text playerHealthText;
    public Text playerDriveText;

    public Text playerCoinsText;

    public UIController uIController;

    private Animator animator;

    private int level;

    public DataCarryOver dco;
    private SoundManager sm;

    private Player player;
    private PlayerMovement pm;
    private PlayerEnemyCollision pec;

    private SaveHandler saveHandler;

    private float hurtTimer = 0.0f;
    public bool dontDrain = false;

    // Start is called before the first frame update
    void Awake()
    {
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();

        overdriveCorountine = OverdriveCOR();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
        pm = GetComponent<PlayerMovement>();
        pec = GetComponent<PlayerEnemyCollision>();

        initPlayerDefense = playerDefense;

    }

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.GetComponent<SoundManager>();

        if (!saveHandler.GetDidLoadSave())
        {
            playerHealth = dco.playerHealth;
            playerDrive = dco.playerDrive;
            Debug.Log("Loaded " + dco.playerDrive + " drive from DCO");
            playerAttack = dco.playerPow;
            playerDefense = dco.playerDef;
            playerSpeed = dco.playerSpeed;
            overdriveLocked = dco.overdriveLocked;
            EarnGold(dco.playerMoney);
        }

        if (player.isOverdrive)
        {
            RunCoroutine();
        }

        UpdateCoinsText();
    }

    // Update is called once per frame
    void Update()
    {
        //Doesnt let drive or health go below 0
        if (playerDrive < 0)
        {
            playerDrive = 0;
        }

        //Doesnt let player health or drive go above maximum
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
        if (playerDrive > playerMaxDrive)
        {
            playerDrive = playerMaxDrive;
        }

        //Ticks hurtTimer if not 0
        if (hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
        }

        UpdateBars();
    }

    //Returns the players overdrive status
    public bool Overdrive()
    {
        return isOverdrive;
    }

    //Drains players overdrive over time
    public IEnumerator OverdriveCOR()
    {
        while (true)
        {
            if(playerDrive <= 0)
            {
                player.isOverdrive = false;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(.1f);
            if(playerSpeed > 0)
            {
                playerDrive -= (driveDrain / (float)playerSpeed);
            }
            else
            {
                playerDrive -= driveDrain;
            }
        }
    }

    //Updates players resource bars
    public void UpdateBars()
    {
        playerDriveBar.fillAmount = playerDrive / playerMaxDrive;
        playerHealthBar.fillAmount = playerHealth / playerMaxHealth;

        playerHealthText.text = (int)playerHealth + "/" + playerMaxHealth;
        playerDriveText.text = (int)playerDrive + "/" + playerMaxDrive;
    }

    private void UpdateCoinsText()
    {
        playerCoinsText.text = gold.ToString();
    }


    //Runs the overdrive coroutine
    public void RunCoroutine()
    {
        if (!dco.cheating)
        {
            StopCoroutine(overdriveCorountine);
            StartCoroutine(overdriveCorountine);
        }
    }

    //Stops the overdrive coroutine
    public void EndCoroutine()
    {
        StopCoroutine(overdriveCorountine);
    }

    //Damages player
    public void Damage(float damage)
    {
        if (damage * (1 - playerDefense / 100.0f) > 0 && hurtTimer <= 0.0f && pec.canEncounter)
        {
            playerHealth -= (int)(damage * (1 - playerDefense / 100.0f));
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                animator.SetBool("isDead", true);
                return;
            }
            else
            {
                sm.sfxPlayer.PlayOneShot(sm.soundHurt);
                animator.SetTrigger("isHurt");
                hurtTimer = 1.0f;
            }
        }
    }

    //Kills player
    public void Die()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundHurt);
        sm.musicPlayer.clip = sm.musicDefeat;
        sm.musicPlayer.Play();
        //Time.timeScale = 0;
        pm.canMove = false;
        pec.canEncounter = false;
        uIController.DiePanel();
    }

    public void Buy(int cost)
    {
        gold -= cost;
        UpdateCoinsText();
    }

    public void EarnGold(int goldEarned)
    {
        gold += goldEarned;
        UpdateCoinsText();
    }
}
