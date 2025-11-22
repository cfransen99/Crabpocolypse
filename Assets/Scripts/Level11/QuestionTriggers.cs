using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionTriggers : MonoBehaviour
{
    private DialogueTrigger dialogue;

    [SerializeField] private TMP_Text[] answerTexts;
    [SerializeField] private string[] answers;
    [SerializeField] private PressurePlateQuestion[] questionPlates;

    void Start()
    {
        dialogue = GetComponent<DialogueTrigger>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dialogue.TriggerDialogue();
            RandomlyGenerateAnswers();
        }
    }

    public void RandomlyGenerateAnswers()
    {
        int rand = Random.Range(0, 2);

        Debug.Log(rand);

        questionPlates[rand].isCurrectAnswer = true;
        answerTexts[rand].text = answers[0];
        answerTexts[(rand + 1) % 2].text = answers[1];
    }
}
