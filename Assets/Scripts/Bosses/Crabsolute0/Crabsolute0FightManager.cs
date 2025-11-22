using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Crabsolute0FightManager : MonoBehaviour
{
    public Transform player;
    public Transform startPosition;
    public Transform alreadyStartedPosition;
    public Animator zeroAnim, shovelAnim;
    public GameObject shovelCrab;
    public BossBase zeroCrab;
    public PlayerMovement playMove;

    public CinemachineVirtualCamera maxCrabCam;

    public GameObject[] traps;
    public Transform[] trapSpawns;

    private DialogueManager dialogueManager;
    private DialogueTrigger endDemoTrigger;

    private LevelLoader levelLoader;
    private DataCarryOver dco;
    private SaveHandler saveHandler;

    private bool endDemo, temp;

    public GameObject explode;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();

        if(saveHandler.GetDidLoadSave())
        {
            Debug.Log("Loaded Save");
            dco.bossHP = 150;
            dco.bossAidHP = 25;

            dco.returningBoss = false;
        }

        if (dco.beatZero)
        {
            dco.playerPosX = -7.32f;
            dco.playerPosY = 1.5f;
            dco.playerPosZ = 0f;
            maxCrabCam.Priority = 11;
            endDemoTrigger = GetComponent<DialogueTrigger>();
            endDemoTrigger.TriggerDialogue(maxCrabCam);
            endDemo = true;
            zeroAnim.SetTrigger("afterFight");
            return;
        }

        if (!dco.returningBoss)
        {
            playMove.canMove = false;
            player.position = startPosition.position;
        }
        else
        {
            shovelCrab.SetActive(false);
            player.position = alreadyStartedPosition.position;
            for (int i = 0; i < trapSpawns.Length; i++)
            {
                SpawnRandomTraps(trapSpawns[i]);
            }
        }
    }

    private void Update()
    {
        if (dialogueManager.noActiveDialogue && !dco.returningBoss && !dco.beatZero && !temp)
        {
            playMove.canMove = false;
            StartCoroutine("StartFirstEncounter");
            temp = true;
        }
        if(dialogueManager.noActiveDialogue && endDemo)
        {
            zeroAnim.SetTrigger("getHurt");
            Instantiate(explode, new Vector3(85.08f, 12.0f, -0.7f), explode.transform.rotation);
            maxCrabCam.Priority = 11;
            endDemo = false;
        }
    }

    private void SpawnRandomTraps(Transform spawnPosition)
    {
        int random = Random.Range(0, traps.Length);

        if(random == 0)
        {
            Instantiate(traps[random], new Vector3(spawnPosition.position.x, 12.5f, 0), spawnPosition.rotation);
        }
        else if (random == 1)
        {
            Instantiate(traps[random], new Vector3(spawnPosition.position.x, 11.3f, 0), spawnPosition.rotation);
        }
        else
        {
            Instantiate(traps[random], new Vector3(spawnPosition.position.x, 15f, 0), spawnPosition.rotation);
        }
    }

    IEnumerator StartFirstEncounter()
    {
        zeroCrab.canMove = true;
        shovelAnim.SetTrigger("runAway");
        yield return new WaitForSeconds(0.5f);
        levelLoader.LoadBattleScene("Zero");
    }
}
