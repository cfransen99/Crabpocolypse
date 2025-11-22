using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    [SerializeField] private string levelName;
    private Animator animator;

    private LevelLoader levelLoader;

    private void Start()
    {
        animator = GetComponent<Animator>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    public void GoToLevel()
    {
        StartCoroutine(WaitToLeave());
    }

    IEnumerator WaitToLeave()
    {
        animator.SetBool("isOpen", true);
        yield return new WaitForSeconds(.5f);
        levelLoader.LoadNewLevel(levelName);
    }
}
