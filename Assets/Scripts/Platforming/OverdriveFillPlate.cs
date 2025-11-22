using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveFillPlate : MonoBehaviour, IInteractable
{
    [SerializeField] private float driveIncrease;
    private Player player;
    private bool isCoroutine;

    public void Interact()
    {
        player = FindObjectOfType<Player>();
        if(!isCoroutine)
        {
            StartCoroutine(FillOverTime());
        }
    }

    public void Uninteract()
    {

    }

    private IEnumerator FillOverTime()
    {
        isCoroutine = true;
        yield return new WaitForSeconds(.1f);
        player.ChangeDrive(driveIncrease);
        isCoroutine = false;
    }
}
