using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float radius, bounce;

    public bool useInventory, isOverdrive, isDropKick = false, isAmbush, isDash, disableFallReset;
    public bool learnDash, learnAir, learnJump, learnBarrier;

    public bool interacted = false;

    public int playerLevel, skillPoints;

    public Inventory inventory;

    public GameObject invUI;

    public UIController uIController;
    private DialogueManager dialogueManager;

    public LayerMask groundLayer;

    private PlayerDropKick dropKick;
    private PlayerDash dash;
    private PlayerAirDash airDash;
    private PlayerPowerJump powerJump;
    private PlayerBarrier barrier;

    public Animator animPlayer;
    private PlayerStats stats;
    private DataCarryOver dco;
    private PlayerMovement playerMove;
    private PlayerLevel level;
    private CapsuleCollider capsuleCollider;
    //private SkillTree skillTree;
    private Rigidbody r;

    private Vector3 startsPos;

    private SaveHandler saveHandler;
    [SerializeField] private float slopeFriction;

    private void Awake()
    {
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        saveHandler.player = this.gameObject;        
    }


    // Start is called before the first frame update
    public IEnumerator Start()
    {
        if ((SceneManager.GetActiveScene().buildIndex != saveHandler.SavedScene()) || SceneManager.GetActiveScene().buildIndex != saveHandler.SavedScene()
            && !saveHandler.GetDidLoadSave())
        {
            saveHandler.SaveNewScene();
        }

        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        if (!saveHandler.GetDidLoadSave())
        {
            learnAir = dco.learnAir;
            learnDash = dco.learnDash;
            learnBarrier = dco.learnBarrier;
            learnJump = dco.learnJump;
        }

        if (!dco.isNewLevel && !dco.returningBoss)
        {
            Debug.Log("spawning player with dco");
            transform.position = new Vector3(dco.playerPosX, dco.playerPosY, dco.playerPosZ);
            isOverdrive = dco.inDrive;
        }

        inventory = Inventory.instance;
        animPlayer = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        playerMove = GetComponent<PlayerMovement>();

        dropKick = GetComponent<PlayerDropKick>();
        dash = GetComponent<PlayerDash>();
        airDash = GetComponent<PlayerAirDash>();
        powerJump = GetComponent<PlayerPowerJump>();
        barrier = GetComponent<PlayerBarrier>();
        level = GetComponent<PlayerLevel>();

        playerLevel = GetComponent<PlayerLevel>().level;

        //skillTree = GetComponent<SkillTree>();

        capsuleCollider = GetComponent<CapsuleCollider>();

        uIController = FindObjectOfType<UIController>();

        dialogueManager = FindObjectOfType<DialogueManager>();

        r = GetComponent<Rigidbody>();

        startsPos = transform.position;

        level.experienceToNextLevel = (int)(level.baseExperience * (Mathf.Pow(level.experienceScaleMultiplier, level.level - 1)));

        if (saveHandler.GetDidLoadSave())
        {
            Debug.Log("Did Load");
            saveHandler.Load();
        }

        yield return new WaitForSeconds(.6f);
        saveHandler.SetDidLoadSave(false);
    }

    // Update is called once per frame
    public void Update()
    {
        //Interacts with any interacatable objects near player
        Collider[] touching = Physics.OverlapSphere(transform.position - new Vector3(0, .15f, 0), radius);
        foreach (Collider touch in touching)
        {
            IInteractable interactable = touch.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
            }
        }

        if(!uIController.isPause)
        {
            //Dropkicks if the player is not on the ground
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.S)) && !OnGround() && dropKick.CanUse())
            {
                Kick();
            }
            //Stops the dropkick if the player is on the ground
            else if (OnGround())
            {
                StopKick();
            }
        }   

        if(uIController.dialogueActive)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                dialogueManager.DisplayNextSentence();
            }
        }
        else
        {
            //Opens inventory if it isnt empty
            if (Input.GetKeyDown(KeyCode.I) && inventory.InventorySize() > 0)
            {
                invUI.SetActive(!invUI.activeInHierarchy);
            }

            //Goes into Maximum Overdrive if they are able to
            if (Input.GetKeyDown(KeyCode.Q) && stats.playerDrive > 0 && !dco.overdriveLocked && !uIController.isPause)
            {
                isOverdrive = !isOverdrive;
                if (isOverdrive)
                {
                    stats.playerDefense *= 2;
                }
                else
                {
                    stats.playerDefense = stats.initPlayerDefense;
                }
            }

            /*
            if (Input.GetKeyDown(KeyCode.C))
            {
                uIController.useSkillTree = !uIController.useSkillTree;
                if (!uIController.isPause)
                {
                    if (uIController.useSkillTree)
                    {
                        uIController.SkillTreePanel();
                    }
                    else
                    {
                        uIController.ResetPanels();
                    }
                }
            }*/

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                uIController.isPause = !uIController.isPause;
                if (uIController.isPause)
                {
                    uIController.PausePanel();
                }
                else
                {
                    uIController.ResetPanels();
                }
            }
            if(Input.GetKeyDown(KeyCode.T))
            {
                uIController.ActivateDevMode();
            }
        }

        //if the player goes below a certain threshold then they respawn at the start
        if (transform.position.y < -10 && !disableFallReset)
        {
            transform.position = startsPos;
        }

        if(!uIController.isPause)
        {
            if (learnDash)
            {
                Dash();
            }

            if (learnAir)
            {
                AirDash();
            }

            if (learnJump)
            {
                PowerJump();
            }

            if (learnBarrier)
            {
                Barrier();
            }
        }
    }

    private void FixedUpdate()
    {
        NormalizeSlope();
    }

    //Draws gizmos to see what the player can interact with
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, .15f, 0), radius);
    }

    //Ground check with ray cast
    public bool OnGround()
    {
        //RaycastHit hit;
        if (Physics.Raycast(capsuleCollider.bounds.center - new Vector3(.15f, 0, 0), Vector3.down, /*out hit,*/ .375f, groundLayer))
        {
            //transform.parent = hit.transform;
            return true;
        }
        if (Physics.Raycast(capsuleCollider.bounds.center + new Vector3(.15f, 0, 0), Vector3.down, /*out hit,*/ .375f, groundLayer))
        {
            //transform.parent = hit.transform;
            return true;
        }
        //transform.parent = null;
        return false;
    }

    public bool OnWallHit()
    {
        RaycastHit hit;

        if (playerMove.lookRight)
        {
            Ray ray = new Ray(capsuleCollider.bounds.center - new Vector3(0, .15f, 0), Vector3.right);
            if (Physics.Raycast(ray, out hit, .25f, groundLayer))
            {
                if(hit.collider.isTrigger)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        else
        {
            Ray ray = new Ray(capsuleCollider.bounds.center - new Vector3(0, .15f, 0), Vector3.left);
            if (Physics.Raycast(ray, out hit, .25f, groundLayer))
            {
                if (hit.collider.isTrigger)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }

    //Dropkick
    public void Kick()
    {
        dropKick.Use();
        isDropKick = true;
        animPlayer.SetBool("isKick", true);
    }
    //Stops the drop kick
    public void StopKick()
    {
        animPlayer.SetBool("isKick", false);
        isDropKick = false;
    }

    //Changes the player overdrive in whatever way needed
    public void ChangeDrive(float val)
    {
        stats.playerDrive += val;
    }

    //Return players bounce value
    public float Bounce()
    {
        return bounce;
    }

    public void PlayDieAnim()
    {
        animPlayer.SetBool("isDead", true);
    }

    public void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && OnGround() && dash.CanUse())
        {
            StartCoroutine(dash.DashLength());
        }
    }

    public void AirDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !OnGround() && airDash.CanUse())
        {
            if (isOverdrive && playerMove.Move().x == 0)
            {
                StartCoroutine(airDash.DashUpLength());
                return;
            }

            if (!OnGround())
            {
                StartCoroutine(airDash.DashLength());
            }
        }
    }

    public void PowerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dropKick.TimeBetweenDropkicks() > 0 && powerJump.CanUse() && OnGround())
        {
            powerJump.Use();
        }
    }

    public void Barrier()
    {
        if (Input.GetKeyDown(KeyCode.R) && !barrier.UsedShield() && stats.playerDrive > 0)
        {
            barrier.Use();
        }
        else if ((Input.GetKeyDown(KeyCode.R) && barrier.UsedShield()) || (barrier.UsedShield() && stats.playerDrive <= 0))
        {
            barrier.NoShield();
        }
    }

    public void LearnAir()
    {
        learnAir = true;
        dco.learnAir = learnAir;
    }
    public void LearnDash()
    {
        learnDash = true;
        dco.learnDash = learnAir;
    }
    public void LearnPowerJump()
    {
        learnJump = true;
        dco.learnJump = learnAir;
    }
    public void LearnBarrier()
    {
        learnBarrier = true;
        dco.learnBarrier = learnAir;
    }

    void NormalizeSlope()
    {
        // Attempt vertical normalization
        if (OnGround())
        {
            RaycastHit hit;
            Ray ray = new Ray(capsuleCollider.bounds.center, Vector3.left);

            if (Physics.Raycast(ray, out hit, .5f) && Mathf.Abs(hit.normal.x) > 0.1f)
            {
                Rigidbody body = GetComponent<Rigidbody>();
                // Apply the opposite force against the slope force 
                // You will need to provide your own slopeFriction to stabalize movement
                body.velocity = new Vector2(body.velocity.x - (hit.normal.x * slopeFriction), body.velocity.y);

                //Move Player up or down to compensate for the slope below them
                Vector3 pos = transform.position;
                pos.y += -hit.normal.x * Mathf.Abs(body.velocity.x) * Time.deltaTime * (body.velocity.x - hit.normal.x > 0 ? 1 : -1);
                transform.position = pos;
            }
        }
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
    //    if(interactable != null)
    //    {
    //        interacted = true;
    //        interactable.Interact();
    //    }
    //}

    private void OnCollisionExit(Collision collision)
    {
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interacted = false;
            interactable.Uninteract();
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
    //    if (interactable != null)
    //    {
    //        interacted = true;
    //        interactable.Interact();
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
        if (interactable != null)
        {
            interacted = false;
            interactable.Uninteract();
        }
    }
}
