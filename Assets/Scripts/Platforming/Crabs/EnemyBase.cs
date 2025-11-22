using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField]protected float health;
    protected Transform player;
    public GameObject vulnIndicator;
    
    protected Animator animator;
    protected Rigidbody r;
    protected NavMeshAgent agent;
    
    protected SaveHandler saveHandler;
    protected DataCarryOver dco;

    [SerializeField] protected float distance;
    protected PlayerLevel playerLevel;

    public List<GameObject> eInventory = new List<GameObject>();

    public int[] percentDropChance;

    public bool isAdvantage = false, isDead = false;

    public int experience;
    [SerializeField] protected int level = 1;

    [SerializeField] protected bool applyAmbush;

    public float advantageTimer = 0.0f;
    
    public int enemyID = 0;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
    }

    public virtual void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        animator = GetComponent<Animator>();
        r = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        playerLevel = player.GetComponent<PlayerLevel>();

        if (!saveHandler.GetDidLoadSave())
        {
            for (int i = 0; i < dco.enemyDead.Length; i++)
            {
                if (i == enemyID)
                {
                    isDead = dco.enemyDead[i];
                }
            }
            if (isDead)
            {
                if(agent != null)
                {
                    agent.isStopped = true;
                }
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < saveHandler.enemyDead.Length; i++)
            {
                if (i == enemyID)
                {
                    isDead = saveHandler.enemyDead[i];
                }
            }
            if (isDead)
            {
                agent.isStopped = true;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
            }
        }

        #region Potential Random Level For Enemies Code
        //int random = Random.Range(0, 4);
        //switch(random)
        //{
        //    case 0:
        //        level = playerLevel.level - 2;
        //        break;
        //    case 1:
        //        level = playerLevel.level - 1;
        //        break;
        //    case 2:
        //        level = playerLevel.level + 0;
        //        break;
        //    case 3:
        //        level = playerLevel.level + 1;
        //        break;
        //    case 4:
        //        level = playerLevel.level + 2;
        //        break;
        //};
        //if(level < 0)
        //{
        //    level = 0;
        //}
        #endregion

        level = playerLevel.level;
    }

    public virtual void Update()
    {
        //Calculates distance from player
        distance = Vector3.Distance(transform.position, player.position);
        Move();
        Attack();
    }

    //Moves enemy and plays animation
    public virtual void Move()
    {
        if (!isAdvantage)
        {
            animator.SetBool("isRunning", true);
            if (agent != null)
            {
                agent.enabled = true;
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
            if (agent != null)
            {
                agent.enabled = false;
            }
            vulnIndicator.SetActive(true);
            advantageTimer += Time.deltaTime;
            if (advantageTimer > 2.0f)
            {
                advantageTimer = 0.0f;
                isAdvantage = false;
                vulnIndicator.SetActive(false);
            }
        }
    }

    //Enemy Attacks
    public abstract void Attack();

    //Damages enemy
    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    //Kills enemy, gives player XP and drops items
    public void Die()
    {
        isDead = true;
        if (agent != null)
        {
            agent.isStopped = true;
        }
        GetComponent<CapsuleCollider>().enabled = false;

        //int giveXP = (int)(experience * (Mathf.Pow(1.35f, level - 1)));
        //Debug.Log("XP given: " + giveXP);
        //player.GetComponent<PlayerLevel>().AddExperience(giveXP);
        
        StartCoroutine(WaitToSpawn());
    }

    //Calculates what items an enemy has and instantiates them
    public virtual void DropItems()
    {
        int[] randomInts = new int[percentDropChance.Length];

        for (int i = 0; i < percentDropChance.Length; i++)
        {
            randomInts[i] = Random.Range(0, 99);
            if (randomInts[i] < percentDropChance[i])
            {
                Instantiate(eInventory[i], transform.position, transform.rotation);
            }
        }
    }

    //Timer before killing enemy and dropping items
    IEnumerator WaitToSpawn()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.15f);
        DropItems();
    }

    public void MakeVulnerable()
    {
        isAdvantage = true;
        advantageTimer = 0.0f;
    }

    public bool ApplyAmbush()
    {
        return true;
    }

    public void UpdateLevel(int playerLevel)
    {
        level = playerLevel;
    }
}
