using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbilityBase : MonoBehaviour
{
    [SerializeField] protected float cost;
    [SerializeField] protected float damage;
    
    [SerializeField] protected float abilityCooldown;
    protected float timeBetweenAbility;

    protected bool isUnlocked = false;

    protected Rigidbody r;
    protected PlayerMovement playerMove;
    protected PlayerStats stats;
    protected Player player;
    protected CapsuleCollider capsuleCollider;
    protected SoundManager sm;

    public virtual void Start()
    {
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
        r = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMovement>();
        stats = GetComponent<PlayerStats>();
        player = GetComponent<Player>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        timeBetweenAbility = 0;
    }

    private void Update()
    {
        if(timeBetweenAbility > 0)
        {
            timeBetweenAbility -= Time.deltaTime;
        }
    }

    public abstract void Use();

    public float Damage()
    {
        return damage;
    }

    public bool CanUse()
    {
        if (timeBetweenAbility > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool IsUnlocked()
    {
        return isUnlocked;
    }
}
