using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpout : OilTrap
{
    private Animator animator;
    public AuraGenerator ag;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //ag = GetComponent<AuraGenerator>();
    }

    private void OnEnable()
    {
        StartCoroutine("WaitToRise");
    }

    public IEnumerator WaitToRise()
    {
        yield return new WaitForSeconds(0.2f);
        ag.StartGenerate("pow");
        yield return new WaitForSeconds(0.6f);
        ag.StartGenerate("def");
        yield return new WaitForSeconds(0.6f);
        ag.StartGenerate("speed");
        yield return new WaitForSeconds(0.6f);
        ag.StopGenerate("all");
        animator.SetTrigger("Up");
        StartCoroutine("WaitToDrop");
    }

    public IEnumerator WaitToDrop()
    {
        yield return new WaitForSeconds(2);
        animator.SetTrigger("Down");
    }

    public void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
