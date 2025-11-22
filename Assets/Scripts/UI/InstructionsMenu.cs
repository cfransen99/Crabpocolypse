using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    public void Back()
    {
        pausePanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
