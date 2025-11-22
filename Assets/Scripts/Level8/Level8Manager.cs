using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8Manager : MonoBehaviour
{
    [SerializeField] private CannonballAimManager cannonball;
    private ShipScript ship;

    private DataCarryOver dco;
    private SaveHandler saveHandler;
    private LevelLoader levelLoader;

    private DialogueTrigger[] dialogues;
    [SerializeField] private DialogueManager dialogueManager;

    private PlayerEnemyCollision playerEnemyCollision;

    [SerializeField] private int bossHPMax = 250;

    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform alreadyStartPosition;

    [SerializeField] private Transform player;
    private PlayerMovement playerMove;

    private bool endFight;

    [SerializeField] private Animator clawdiusAnim;

    private void Awake()
    {
        dialogues = GetComponents<DialogueTrigger>();

        ship = FindObjectOfType<ShipScript>();
        playerEnemyCollision = FindObjectOfType<PlayerEnemyCollision>();

        playerMove = player.GetComponent<PlayerMovement>();
    }

    private IEnumerator Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        levelLoader = FindObjectOfType<LevelLoader>();

        if (dco.beatClawdius)
        {
            dco.bossAidHP = 150;
            dco.playerPosX = startPosition.position.x;
            dco.playerPosY = startPosition.position.y;
            dco.playerPosZ = startPosition.position.z;
            clawdiusAnim.SetBool("isEnraged", true);
            endFight = true;
        }

        if (!dco.returningBoss)
        {
            dco.bossHP = 250;
            player.position = startPosition.position;
        }
        else if (dco.returningBoss && dco.bossHP > 0)
        {
            Debug.Log("Fight Spawn");
            player.position = alreadyStartPosition.position;
        }

        yield return new WaitForSeconds(1f);

        if (saveHandler.GetDidLoadSave())
        {
            dco.bossHP = bossHPMax;
            dco.bossAidHP = 100;

            dco.returningBoss = false;
        }

       if (dco.beatClawdius)
        {
            Debug.Log("Playing end dialogue");
            if (!dialogues[1].isTriggered)
            {
                dialogues[1].TriggerDialogue();
            }
        }
        else if (!dco.returningBoss && dco.bossHP > 0)
        {
            if (!dialogues[0].isTriggered)
            {
                dialogues[0].TriggerDialogue();
            }
        }
        else if (dco.returningBoss && dco.bossHP > 0)
        {
            cannonball.gameObject.SetActive(true);
            float healthLost = 1 - (dco.bossHP / (float)bossHPMax);

            if (1 - healthLost >= .40)
            {
                cannonball.rateOfFire -= cannonball.rateOfFire * healthLost;
            }
            else
            {
                cannonball.rateOfFire -= cannonball.rateOfFire * .60f;
            }
        }
    }

    private void Update()
    {
        if (dialogueManager.noActiveDialogue && !dco.returningBoss && !dco.beatClawdius)
        {
            playerMove.DisableMovement();
            StartCoroutine("StartFirstEncounter");
        }
        if (dialogueManager.noActiveDialogue && endFight)
        {
            clawdiusAnim.SetBool("OffBoat", true);

            endFight = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerEnemyCollision.enemyType = ship.gameObject.name;
            collision.gameObject.GetComponent<PlayerStats>().Damage(5);
            levelLoader.LoadBattleScene();
        }
    }

    IEnumerator StartFirstEncounter()
    {
        dco.returningBoss = true;
        yield return new WaitForSeconds(0.5f);
        levelLoader.LoadBattleScene("Clawdius");
    }
}
