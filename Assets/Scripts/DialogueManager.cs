using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Queue<string> sentences;
    [SerializeField] private Queue<string> names;
    
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;

    public bool noActiveDialogue;

    private UIController uIController;

    public Animator animator;

    private PlayerMovement playerMove;

    private CinemachineVirtualCamera[] cinemachineCams;
    private int activeCam;

    private PlayerStats stats;

    private int[] indexesToChange;
    private int index;

    // Start is called before the first frame update
    void Awake()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();

        uIController = FindObjectOfType<UIController>();

        playerMove = FindObjectOfType<PlayerMovement>();
        stats = playerMove.GetComponent<PlayerStats>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        stats.EndCoroutine();
        noActiveDialogue = false;
        playerMove.DisableMovement();

        uIController.DialoguePanel();
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.names[0];

        names.Clear();
        sentences.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue, CinemachineVirtualCamera cam)
    {
        stats.EndCoroutine();

        noActiveDialogue = false;
        cinemachineCams = new CinemachineVirtualCamera[1];

        playerMove.DisableMovement();
        cinemachineCams[0] = cam;

        uIController.DialoguePanel();
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.names[0];

        names.Clear();
        sentences.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void StartDialogue(Dialogue dialogue, CinemachineVirtualCamera[] cams, int[] nums)
    {
        stats.EndCoroutine();

        index = 0;
        noActiveDialogue = false;
        cinemachineCams = new CinemachineVirtualCamera[cams.Length];

        playerMove.DisableMovement();

        activeCam = 0;

        cinemachineCams = cams;
        indexesToChange = nums;

        uIController.DialoguePanel();
        animator.SetBool("isOpen", true);
        nameText.text = dialogue.names[0];

        names.Clear();
        sentences.Clear();

        foreach (string name in dialogue.names)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            StartCoroutine(EndDialogue());
            return;
        }

        if(cinemachineCams != null)
        {
            if (cinemachineCams.Length > 1)
            {
                index++;
                foreach (int num in indexesToChange)
                {
                    if (num == index)
                    {
                        activeCam++;
                        ChangeCam(cinemachineCams[activeCam]);
                    }
                }
            }
        }

        string name = names.Dequeue();
        string sentence = sentences.Dequeue();

        nameText.text = name;
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void ChangeCam(CinemachineVirtualCamera cam)
    {
        cinemachineCams[activeCam - 1].Priority = 9;
        cam.Priority = 11;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private IEnumerator EndDialogue()
    {
        noActiveDialogue = true;

        animator.SetBool("isOpen", false);
        uIController.ResetPanels();
        uIController.dialogueActive = false;

        if(cinemachineCams != null)
        {
            foreach(CinemachineVirtualCamera cinemachineCam in cinemachineCams)
            {
                cinemachineCam.Priority = 9;
            }
        }
       
        playerMove.EnableMovement();
        yield return new WaitForEndOfFrame();

        if (cinemachineCams != null)
        {
            yield return new WaitForSeconds(1.25f);
            playerMove.EnableMovement();
        }
    }
}
