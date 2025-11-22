using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour, IInteractable
{
    protected PlayerMovement playerMove;
    protected Player player;
    protected SpriteRenderer playerSprite;
    protected Rigidbody playerRB;

    public Transform firePosition;

    private Animator animator;

    protected bool isInteracting = false;

    [SerializeField] protected float rotationAngle;

    [SerializeField] protected Transform[] flyPath = new Transform[3];
    protected int destination = 0;
    
    [SerializeField] protected float speed;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected int aimNum;

    protected bool isFlying;

    private CannonballAimManager cannonball;

    public virtual void Start()
    {
        playerMove = FindObjectOfType<PlayerMovement>();
        player = playerMove.GetComponent<Player>();
        playerSprite = playerMove.GetComponent<SpriteRenderer>();
        playerRB = playerMove.GetComponent<Rigidbody>();

        cannonball = FindObjectOfType<CannonballAimManager>();

        animator = GetComponent<Animator>();

    }

    public virtual void Update()
    {
        if(isFlying)
        {
            MoveOnFlyPath();
            if(cannonball != null)
            {
                cannonball.enabled = false;
            }
        }

        if (player.transform.position == flyPath[flyPath.Length - 1].position)
        {
            destination = 0;
            playerMove.EnableMovement();
            playerRB.useGravity = true;

            isFlying = false;
            playerMove.isFlying = false;
            if(cannonball != null)
            {
                cannonball.enabled = true;
            }
        }
    }

    public virtual void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerMove.canMove)
        {
            playerSprite.enabled = false;

            playerMove.DisableMovement();

            AimCannon();
        }
    }

    public virtual void Uninteract()
    {
        isInteracting = false;
    }

    public void AimCannon()
    {
        //transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime), rotationAngle);
        animator.SetBool("Aim" + aimNum, true);
    }

    public virtual IEnumerator Fire()
    {
        animator.SetBool("Aim" + aimNum, false);

        if(firePosition != null)
        {
            playerMove.transform.position = firePosition.position;
            playerMove.GetComponent<Rigidbody>().useGravity = false;
            isFlying = true;
            playerMove.isFlying = true;
        }
        

        yield return new WaitForSeconds(.25f);

        playerSprite.enabled = true;
    }

    public virtual void MoveOnFlyPath()
    {
        int flyPathNum = SetFlyPath();
        playerMove.transform.position = Vector3.MoveTowards(playerMove.transform.position, flyPath[flyPathNum].position, speed * Time.deltaTime);
    }

    public virtual int SetFlyPath()
    {
        if(player.transform.position == flyPath[destination].transform.position)
        {
            return destination = (destination + 1) % flyPath.Length;
        }
        return destination;
    }
}