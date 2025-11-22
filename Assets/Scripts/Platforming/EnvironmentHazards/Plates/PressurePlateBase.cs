using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PressurePlateBase : MonoBehaviour
{
    protected Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public abstract void OnCollisionEnter(Collision collision);

    public abstract void OnCollisionExit(Collision collision);

    public virtual void Activate()
    {
        animator.SetBool("Activate", true);
    }
}
