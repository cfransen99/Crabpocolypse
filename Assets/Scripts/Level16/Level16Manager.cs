using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Level16Manager : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform postShovelStartPosition;
    [SerializeField] private Transform alreadyStartedPosition;
    [SerializeField] private Transform endPosition;

    [SerializeField] private Transform player;
    private Player p;
    private PlayerMovement playerMove;

    private DataCarryOver dco;
    private SaveHandler saveHandler;

    [SerializeField] private GameObject[] oilSpouts;
    [SerializeField] private float timeTillSpout;

    [SerializeField] private GameObject shovelCrab;

    private DialogueTrigger[] endDialogue;

    private int whichSpout1;
    private int whichSpout2;

    private DialogueManager dialogueManager;
    private bool endFight;

    [SerializeField] private GameObject[] cannons;

    private LevelLoader levelLoader;

    [SerializeField] private DialogueTrigger shovelDialogue;
    [SerializeField] private DialogueTrigger lincrabDialogue;

    [SerializeField] private CinemachineVirtualCamera fightCam;
    [SerializeField] private CinemachineVirtualCamera lincrabCam;
    [SerializeField] private CinemachineVirtualCamera endCam;

    [SerializeField] private Animator lincrabAnim;
    private bool endGame;

    [SerializeField] private Transform newLoc;
    private bool isOver;

    [SerializeField] private GameObject explode;
    [SerializeField] private Transform zero;

    private void Awake()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();

        dialogueManager = FindObjectOfType<DialogueManager>();

        endDialogue = GetComponents<DialogueTrigger>();

        playerMove = player.GetComponent<PlayerMovement>();
        p = player.GetComponent<Player>();

        levelLoader = FindObjectOfType<LevelLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (dco.beatLincrab)
        {
            dco.playerPosX = endPosition.position.x;
            dco.playerPosY = endPosition.position.y;
            dco.playerPosZ = endPosition.position.z;
            shovelCrab.SetActive(false);
            shovelDialogue.gameObject.SetActive(false);
            Debug.Log("After Boss Fight start position");
            endFight = true;

            lincrabCam.Priority = 11;
            endDialogue[0].TriggerDialogue(lincrabCam);

            fightCam.Priority = 9;
        }
        else if (dco.returningBoss && dco.bossHP > 0)
        {
            player.position = alreadyStartedPosition.position;

            fightCam.Priority = 11;

            foreach(GameObject cannon in cannons)
            {
                cannon.SetActive(true);
            }
            ChooseOilSpout();
            
            Debug.Log("During Boss Fight start position");
        }
        else if(!dco.beatShovel)
        {
            player.position = startPosition.position;
            Debug.Log("starting position");
        }
        else if (dco.beatShovel && !dco.beatLincrab)
        {
            dco.bossHP = 500;
            player.position = postShovelStartPosition.position;
            shovelCrab.SetActive(false);
            shovelDialogue.gameObject.SetActive(false);
            Debug.Log("post Shovel Crab starting position");
        }
    }

    private void Update()
    {
        if (dialogueManager.noActiveDialogue && !dco.beatShovel)
        {
            if(shovelDialogue.isTriggered)
            {
                playerMove.DisableMovement();
                StartCoroutine("StartFirstEncounter");
            }    
        }
        else if(dialogueManager.noActiveDialogue && dco.beatShovel && !dco.beatLincrab)
        {
            if (lincrabDialogue.isTriggered)
            {
                Debug.Log("Starting Lincrab Fight");
                playerMove.DisableMovement();
                StartCoroutine("StartFinalEncounter");
            }
        }
        else if (dialogueManager.noActiveDialogue && endFight)
        {
            Debug.Log("Lincrab Dying");
            playerMove.DisableMovement();
            lincrabDialogue.gameObject.SetActive(false);

            lincrabAnim.SetTrigger("charge");

            endFight = false;
        }

        if (playerMove.isTransform && endGame)
        {
            playerMove.DisableMovement();


            StartCoroutine(WaitToRun());

            endGame = false;
        }
        if(isOver && dialogueManager.noActiveDialogue)
        {
            Instantiate(explode, new Vector3(zero.position.x, zero.position.y, -0.7f), explode.transform.rotation);

            playerMove.DisableMovement();
            playerMove.Flip();
            playerMove.MoveOnX(newLoc);
            endCam.Priority = 11;
            isOver = false;
        }
    }

    IEnumerator StartFirstEncounter()
    {
        yield return new WaitForSeconds(0.5f);
        levelLoader.LoadBattleScene("Shovel");
    }

    IEnumerator StartFinalEncounter()
    {
        yield return new WaitForSeconds(0.5f);
        levelLoader.LoadBattleScene("Lincrab");
    }


    private void ChooseOilSpout()
    {
        if (whichSpout1 > 3)
        {
            whichSpout1 = 0;
            whichSpout2 = 4;
        }

        oilSpouts[whichSpout1].SetActive(true);
        oilSpouts[whichSpout2].SetActive(true);

        whichSpout1++;
        whichSpout2++;

        StartCoroutine("WaitToSpout");
    }

    public IEnumerator WaitToSpout()
    {
        yield return new WaitForSeconds(timeTillSpout);
        ChooseOilSpout();
    }

    public void EndTheGame()
    {
        endCam.Priority = 11;
        playerMove.TransformMax();
        p.isOverdrive = true;

        endGame = true;
    }

    private IEnumerator WaitToRun()
    {
        yield return new WaitForSeconds(1f);

        player.GetComponent<PlayerStats>().StopAllCoroutines();

        endDialogue[1].TriggerDialogue();

        isOver = true;
    }
}
