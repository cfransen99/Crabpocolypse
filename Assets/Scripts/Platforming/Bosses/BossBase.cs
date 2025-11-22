using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BossBase : MonoBehaviour
{
    public int hp;
    public int aidHP;
    public bool canMove;
    
    protected Animator animator;
    protected NavMeshAgent agent;

    public Transform player;

    public bool isAdvantage;

    protected DataCarryOver dco;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        canMove = dco.returningBoss;

        hp = dco.bossHP;
        if(hp <= 0)
        {
            canMove = false;
            if(agent != null)
            {
                agent.isStopped = true;
            }
            animator.SetBool("isRunning", false);
        }
    }

    private void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    public abstract void Move();

    public void Stun()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Stunned");
        agent.isStopped = true;
        isAdvantage = true;
        canMove = false;
        animator.SetTrigger("getHurt");
    }

}
