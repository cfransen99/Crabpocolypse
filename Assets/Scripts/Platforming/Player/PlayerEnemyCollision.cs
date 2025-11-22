using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class PlayerEnemyCollision : MonoBehaviour
{
    private bool trigger;
    public bool canEncounter = false;

    public string enemyType;
    public int enemyID;
    public bool isAdvantage;

    private Player player;
    private PlayerStats stats;
    private PlayerMovement move;
    private Rigidbody r;
    private PlayerLevel level;
    private PlayerDash dash;
    private new CapsuleCollider collider;
    private PlayerBarrier barrier;

    private SoundManager sm;
    private bool encounterStarted;

    private DataCarryOver dco;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
        player = GetComponent<Player>();
        level = GetComponent<PlayerLevel>();
        r = GetComponent<Rigidbody>();
        stats = GetComponent<PlayerStats>();
        move = GetComponent<PlayerMovement>();
        collider = GetComponent<CapsuleCollider>();

        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        dash = GetComponent<PlayerDash>();
        barrier = GetComponent<PlayerBarrier>();
        StartCoroutine("StartEncounters");
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (canEncounter)
    //    {
    //        if (collision.gameObject.tag == "Enemy")
    //        {
    //            if (barrier.UsedShield())
    //            {
    //                return;
    //            }
    //            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

    //            //If player dropkicks an enemy and is Max, apply advantage and bounce off of the enemy
    //            if (player.isDropKick && !player.isOverdrive)
    //            {
    //                sm.sfxPlayer.PlayOneShot(sm.soundHurt);
    //                r.AddForce(collision.GetContact(0).normal * player.Bounce(), ForceMode.Impulse);
    //                player.StopKick();
    //                enemy.isAdvantage = true;
    //                return;
    //            }
    //            //If player dropkicks an enemy and is Maximum Overdrive, kill the enemy and bounce off of the enemy
    //            else if (player.isDropKick && player.isOverdrive)
    //            {
    //                sm.sfxPlayer.PlayOneShot(sm.soundHurt);
    //                r.AddForce(collision.GetContact(0).normal * player.Bounce(), ForceMode.Impulse);
    //                enemy.Die();
    //                player.ChangeDrive(5);
    //                return;
    //            }
    //            else if (dash.IsDashing())
    //            {
    //                sm.sfxPlayer.PlayOneShot(sm.soundHurt);
    //                enemy.Damage(dash.Damage());
    //                return;
    //            }
    //            else if (!encounterStarted)
    //            {
    //                sm.sfxPlayer.PlayOneShot(sm.soundClang);
    //                encounterStarted = true;
    //            }

    //            //Freezes both player and enemy in place during transition
    //            NavMeshAgent temp = collision.gameObject.GetComponent<NavMeshAgent>();
    //            if (temp != null && temp.enabled)
    //            {
    //                temp.isStopped = true;
    //            }
    //            else
    //            {
    //                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //            }
    //            r.constraints = RigidbodyConstraints.FreezeAll;

    //            enemyType = collision.gameObject.name;
    //            enemyID = collision.gameObject.GetComponent<EnemyBase>().enemyID;
    //            isAdvantage = enemy.isAdvantage;
    //            trigger = true;
    //            player.animPlayer.SetBool("isRunning", false);
    //            player.enabled = false;

    //        }
    //        else if(collision.gameObject.tag == "Boss")
    //        {
    //            sm.sfxPlayer.PlayOneShot(sm.soundClang);
    //            BossBase enemy = collision.gameObject.GetComponent<BossBase>();

    //            NavMeshAgent temp = collision.gameObject.GetComponent<NavMeshAgent>();
    //            if (temp != null && temp.enabled)
    //            {
    //                temp.isStopped = true;
    //            }
    //            else
    //            {
    //                collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //            }
    //            r.constraints = RigidbodyConstraints.FreezeAll;

    //            enemyType = collision.gameObject.name;
    //            isAdvantage = enemy.isAdvantage;
    //            trigger = true;
    //            player.animPlayer.SetBool("isRunning", false);
    //            player.enabled = false;
    //            dco.returningBoss = true;
                
    //        }
    //        else
    //        {
    //            trigger = false;
    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Laser")
        {
            other.gameObject.GetComponent<GunnerLaser>().SendShoot();
        }
        else if (other.gameObject.tag == "ClawTrap" && !move.isGrabbed)
        {
            other.transform.parent.GetComponentInParent<Claw>().SendGrab();
            //other.gameObject.GetComponent<Claw>().SendGrab();
        }
        else if (other.gameObject.tag == "ClawAttack" && !move.isGrabbed && !move.movingOnZ)
        {
            other.transform.parent.GetComponentInParent<Claw>().grabTrigger.enabled = false;
            other.transform.parent.GetComponentInParent<Claw>().load = gameObject;
            move.GetGrabbed(other.transform.parent.GetComponentInParent<Claw>());
            //other.gameObject.GetComponent<Claw>().grabTrigger.enabled = false;
            //other.gameObject.GetComponent<Claw>().load = gameObject;
            //move.GetGrabbed(other.GetComponent<Claw>());
        }
        else if (other.gameObject.tag == "Boss")
        {
            sm.sfxPlayer.PlayOneShot(sm.soundClang);
            BossBase enemy = other.gameObject.GetComponent<BossBase>();

            NavMeshAgent temp = other.gameObject.GetComponent<NavMeshAgent>();
            if (temp != null && temp.enabled)
            {
                temp.isStopped = true;
            }
            else
            {
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            r.constraints = RigidbodyConstraints.FreezeAll;

            enemyType = other.gameObject.name;
            isAdvantage = enemy.isAdvantage;
            trigger = true;
            player.animPlayer.SetBool("isRunning", false);
            player.enabled = false;
            dco.returningBoss = true;

        }

        if (canEncounter)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (barrier.UsedShield())
                {
                    return;
                }
                EnemyBase enemy = other.gameObject.GetComponent<EnemyBase>();

                //If player dropkicks an enemy and is Max, apply advantage and bounce off of the enemy
                if (player.isDropKick && !player.isOverdrive)
                {
                    sm.sfxPlayer.PlayOneShot(sm.soundHurt);
                    r.velocity = new Vector3(r.velocity.x, 0, r.velocity.z);
                    r.AddForce(Vector3.up * player.Bounce(), ForceMode.Impulse);
                    player.StopKick();
                    enemy.isAdvantage = true;
                    return;
                }
                //If player dropkicks an enemy and is Maximum Overdrive, kill the enemy and bounce off of the enemy
                else if (player.isDropKick && player.isOverdrive)
                {
                    sm.sfxPlayer.PlayOneShot(sm.soundHurt);
                    r.AddForce(Vector3.up * player.Bounce(), ForceMode.Impulse);
                    enemy.Die();
                    player.ChangeDrive(5);
                    return;
                }
                else if (dash.IsDashing())
                {
                    sm.sfxPlayer.PlayOneShot(sm.soundHurt);
                    enemy.Damage(dash.Damage());
                    return;
                }
                else if (!encounterStarted)
                {
                    sm.sfxPlayer.PlayOneShot(sm.soundClang);
                    encounterStarted = true;
                }

                //Freezes both player and enemy in place during transition
                NavMeshAgent temp = other.gameObject.GetComponent<NavMeshAgent>();
                if (temp != null && temp.enabled)
                {
                    temp.isStopped = true;
                }
                else
                {
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                r.constraints = RigidbodyConstraints.FreezeAll;

                enemyType = other.gameObject.name;
                enemyID = other.gameObject.GetComponent<EnemyBase>().enemyID;
                isAdvantage = enemy.isAdvantage;
                trigger = true;
                player.animPlayer.SetBool("isRunning", false);
                player.enabled = false;

            }
            else if (other.gameObject.tag == "Boss")
            {
                sm.sfxPlayer.PlayOneShot(sm.soundClang);
                BossBase enemy = other.gameObject.GetComponent<BossBase>();

                NavMeshAgent temp = other.gameObject.GetComponent<NavMeshAgent>();
                if (temp != null && temp.enabled)
                {
                    temp.isStopped = true;
                }
                else
                {
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                r.constraints = RigidbodyConstraints.FreezeAll;

                enemyType = other.gameObject.name;
                isAdvantage = enemy.isAdvantage;
                trigger = true;
                player.animPlayer.SetBool("isRunning", false);
                player.enabled = false;
                dco.returningBoss = true;

            }
            else
            {
                trigger = false;
            }
        }
    }

    public bool Trigger()
    {
        return trigger;
    }

    IEnumerator StartEncounters()
    {
        yield return new WaitForSeconds(1.0f);
        canEncounter = true;
    }
}
