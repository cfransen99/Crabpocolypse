using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossDoor : GarageDoor
{
    [SerializeField] private EnemyBase[] areDead;


    private void Start()
    {
        CheckIfOpen();
    }

    private void CheckIfOpen()
    {
        foreach (EnemyBase enemy in areDead)
        {
            if (enemy.isDead == false)
            {
                return;
            }
        }

        GetComponent<Animator>().SetTrigger("Open");
    }
}
