using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Level12Manager : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform alreadyStartedPosition;

    [SerializeField] private Transform player;
    private PlayerMovement playerMove;

    private DataCarryOver dco;
    private SaveHandler saveHandler;
    private DialogueTrigger[] dialogues;
    private LevelLoader levelLoader;
    private DialogueManager dialogueManager;

    [SerializeField] private GameObject[] paths = new GameObject[6];
    [SerializeField] private Transform[] pathSpawns = new Transform[3];

    [SerializeField] private CinemachineVirtualCamera[] cams;
    [SerializeField] private int[] indexes;
    private bool endFight;

    [SerializeField] private Animator moustacheAnim;

    private void Awake()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();

        dialogues = GetComponents<DialogueTrigger>();

        levelLoader = FindObjectOfType<LevelLoader>();
        dialogueManager = FindObjectOfType<DialogueManager>();

        playerMove = player.GetComponent<PlayerMovement>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        if (dco.beatMoustache)
        {
            dco.playerPosX = startPosition.position.x;
            dco.playerPosY = startPosition.position.y;
            dco.playerPosZ = startPosition.position.z;
        }
        else if (!dco.returningBoss)
        {
            dco.bossHP = 400;
            player.position = startPosition.position;
        }
        else if (dco.returningBoss && dco.bossHP > 0)
        {
            player.position = alreadyStartedPosition.position;
        }

        yield return new WaitForSeconds(1f);

        if (!dco.returningBoss && dco.bossHP > 0)
        {
            Debug.Log("starting position");

            if (!dialogues[0].isTriggered)
            {
                cams[0].Priority = 11;
                dialogues[0].TriggerDialogue(cams, indexes);
            }
        }
        else if (dco.beatMoustache)
        {
            dco.returningBoss = false;
            Debug.Log("ending");
            cams[0].Priority = 11;
            dialogues[1].TriggerDialogue();
            endFight = true;
        }
        else if(dco.returningBoss && dco.bossHP > 0)
        {
            for (int i = 0; i < pathSpawns.Length; i++)
            {
                SpawnRandomPath(pathSpawns[i]);
            }
            Debug.Log("new starting position");
        }
    }

    private void Update()
    {
        if (dco.beatMoustache)
        {
            playerMove.DisableMovement();
        }
        if (dialogueManager.noActiveDialogue && !dco.returningBoss && !dco.beatMoustache)
        {
            playerMove.DisableMovement();
            StartCoroutine("StartFirstEncounter");
        }
        else if (dialogueManager.noActiveDialogue && endFight)
        {
            Debug.Log("Moustache Dying");
            playerMove.DisableMovement();

            moustacheAnim.SetTrigger("Die");

            endFight = false;
        }
    }

    private void SpawnRandomPath(Transform spawnPosition)
    {
        int random = Random.Range(0, paths.Length);

        Instantiate(paths[random], spawnPosition.position, spawnPosition.rotation);
    }

    IEnumerator StartFirstEncounter()
    {
        dco.returningBoss = true;
        yield return new WaitForSeconds(0.5f);
        levelLoader.LoadBattleScene("Moustache");
    }
}
