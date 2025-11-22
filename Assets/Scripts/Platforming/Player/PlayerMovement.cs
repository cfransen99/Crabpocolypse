using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2;
    public float overMultiplier;

    private Rigidbody r;
    private Vector3 move;
    private PlayerStats stats;

    public bool lookRight = true, canMove = true, isFlying = false;

    public Animator animPlayer;

    public bool isTransform;

    private float initSpeed, initJumpForce;

    private Player player;

    private DataCarryOver dco;
    
    public bool movingOnZ;
    private Transform moveToOnZ;

    private bool movingOnX;
    private Transform moveToOnX;

    public bool isGrabbed, mashA;
    private int mashed = 0;
    private Claw claw;

    private int onAxis;

    private UIController uIController;

    public int OnAxis { get => onAxis; set => onAxis = value; }

    // Start is called before the first frame update
    void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        if (!dco.facingRight)
        {
            Flip();
        }

        r = GetComponent<Rigidbody>();
        animPlayer = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        player = GetComponent<Player>();

        initSpeed = speed;
        initJumpForce = jumpForce;

        player = GetComponent<Player>();

        if (dco.inDrive)
        {
            isTransform = true;
            animPlayer.SetBool("isOverdrive", true);
            animPlayer.SetTrigger("isTransformed");
        }

        uIController = FindObjectOfType<UIController>();
    }

    private void Update()
    {
        if(!uIController.isPause)
        {
            move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            //if player presses jump button and is ground on the ground they jump
            if (Input.GetButtonDown("Jump") && player.OnGround() && canMove)
            {
                Jump();
            }
        }


        //Plays players move animation if they are moving and stops it if they are not
        if (move.x != 0 && canMove)
        {
            animPlayer.SetBool("isRunning", true);
        }
        else
        {
            animPlayer.SetBool("isRunning", false);
        }

        //If the player has press the overdrive button and is not transformed, transform them
        if (player.isOverdrive && !isTransform)
        {
            TransformMax();
        }
        //If the player has press the overdrive button and is transformed, go back to Max
        else if (!player.isOverdrive && isTransform)
        {
            RevertOverdrive();
        }

        if(movingOnZ)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveToOnZ.position.x, transform.position.y, moveToOnZ.position.z), 2 * speed * Time.deltaTime);
            MoveOnZ(moveToOnZ);
        }
        if (movingOnX)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveToOnX.position.x, transform.position.y, moveToOnX.position.z), 2 * speed * Time.deltaTime);
            MoveOnX(moveToOnX);
        }
    }

    void FixedUpdate()
    {
        if(canMove)
        {
            //Moves player
            MoveCharacter(move);
        }
        else if (isGrabbed)
        {
            transform.parent = claw.loadPos;
            transform.position = claw.loadPos.position;
            if (claw.canMash && mashA && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
            {
                mashed++;
                mashA = false;
            }
            else if (claw.canMash && !mashA && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
            {
                mashed++;
                mashA = true;
            }

            if (mashed >= claw.numMashes)
            {
                RestoreGrav();
            }
        }

        //If the player is falling, add fall multiplier to make them fall faster
        if (r.velocity.y < 0)
        {
            r.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //For when the player wants to make a small jump
        else if (r.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            r.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(player.OnGround())
        {
            animPlayer.SetBool("isJumping", false);
        }
        else
        {
            animPlayer.SetBool("isJumping", true);
        }

        if(player.OnWallHit())
        {
            r.MovePosition(new Vector3(transform.position.x, transform.position.y));
        }
    }

    //Moves player
    public void MoveCharacter(Vector3 move)
    {
        r.MovePosition(transform.position + (move * speed * Time.deltaTime));

        if ((move.x > 0 && !lookRight) || (move.x < 0 && lookRight))
        {
            Flip();
        }
        
    }

    public void DisableMovement()
    {
        canMove = false;
        StopAllCoroutines();
    }
    public void EnableMovement()
    {
        canMove = true;
        if(player.isOverdrive)
        {
            stats.RunCoroutine();
        }
    }

    //Makes player jump
    public void Jump()
    {
        r.velocity = new Vector3(r.velocity.x, 0, r.velocity.z);
        r.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    //Flips the player from right to left and vice versa
    public void Flip()
    {
        lookRight = !lookRight;
        transform.rotation = Quaternion.Euler(0, lookRight ? 0 : 180, 0);        
    }

    //Transforms player into Maximum Overdrive
    public void TransformMax()
    {
        animPlayer.SetBool("isOverdrive", true);
        Transforming();
    }

    //Reverts player into Max
    public void RevertOverdrive()
    {
        speed = initSpeed;
        jumpForce = initJumpForce;
        animPlayer.SetBool("isOverdrive", false);
        isTransform = false;
    }

    //Stops player from moving when transforming
    public void Transforming()
    {
        speed = 0;
        jumpForce = 0;
    }

    //Resets players speed and jump force and adds the overdrive multiplier to both
    public bool IsTransformed()
    {
        speed = initSpeed;
        jumpForce = initJumpForce;

        speed *= overMultiplier;
        jumpForce *= overMultiplier;
        return isTransform = true;
    }

    public Vector3 Move()
    {
        return move;
    }

    public void MoveOnZ(Transform moveTo)
    {
        if (transform.position.z == moveTo.position.z)
        {
            animPlayer.SetBool("isRunning", false);
            movingOnZ = false;
            EnableMovement();
        }
        else
        {
            animPlayer.SetBool("isRunning", true);
            moveToOnZ = moveTo;
            movingOnZ = true;
        }
    }

    public void MoveOnX(Transform moveTo)
    {
        if (transform.position.x == moveTo.position.x)
        {
            animPlayer.SetBool("isRunning", false);
            movingOnX = false;
            EnableMovement();
        }
        else
        {
            animPlayer.SetBool("isRunning", true);
            moveToOnX = moveTo;
            movingOnX = true;
        }
    }

    public void GetGrabbed(Claw c)
    {
        isGrabbed = true;
        DisableMovement();
        r.isKinematic = true;
        player.GetComponent<Collider>().enabled = false;
        claw = c;
        mashed = 0;
        int rand = Random.Range(0, 2);
        //Randomly determines whether 'A' or 'D' is the first mash key
        if (rand == 0)
            mashA = true;
        else
            mashA = false;
    }

    public void RestoreGrav()
    {
        isGrabbed = false;
        EnableMovement();
        transform.parent = null;
        r.isKinematic = false;
        player.GetComponent<Collider>().enabled = true;
        claw.load = null;
    }
}
