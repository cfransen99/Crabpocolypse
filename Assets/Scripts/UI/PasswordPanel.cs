using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordPanel : MonoBehaviour
{
    private UIController uIController;
    private PasswordConsole console;
    private PlayerMovement playerMove;

    [SerializeField] private GameObject cText;
    [SerializeField] private GameObject rText;
    [SerializeField] private GameObject aText;
    [SerializeField] private GameObject bText;

    [SerializeField] private GameObject endLevelTrigger;

    private bool[] hasLetters = new bool[4];

    private void Start()
    {
        uIController = FindObjectOfType<UIController>();
        console = FindObjectOfType<PasswordConsole>();
        playerMove = FindObjectOfType<PlayerMovement>();
    }

    public void Exit()
    {
        uIController.ResetPanels();
        gameObject.SetActive(false);
        console.IsActive = false;
        playerMove.EnableMovement();
    }

    public void HasC()
    {
        cText.SetActive(true);
        hasLetters[0] = true;
        CheckIfHasAll();
    }

    public void HasR()
    {
        rText.SetActive(true);
        hasLetters[1] = true;
        CheckIfHasAll();
    }

    public void HasA()
    {
        aText.SetActive(true);
        hasLetters[2] = true;
        CheckIfHasAll();
    }

    public void HasB()
    {
        bText.SetActive(true);
        hasLetters[3] = true;
        CheckIfHasAll();
    }

    public void CheckIfHasAll()
    {
        foreach(bool hasLetter in hasLetters)
        {
            if(!hasLetter)
            {
                return;
            }
        }
        endLevelTrigger.SetActive(true);
    }
}
