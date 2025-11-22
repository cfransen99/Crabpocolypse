using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed;
    private Rigidbody r;
    private float damage;
    private SoundManager sm;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
        r = GetComponent<Rigidbody>();
        sm.sfxPlayer.PlayOneShot(sm.soundShoot);

        //Moves projectile down at specific speed
        r.velocity = -transform.up * speed;
        
        StartCoroutine(DestroyTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        //If player is hit damage them
        if(other.gameObject.tag == "Player" && !other.gameObject.GetComponent<PlayerBarrier>().UsedShield())
        {
            if (other.gameObject.GetComponent<PlayerStats>().isVulnerable && other.gameObject.GetComponent<PlayerStats>().playerHealth > 0)
            {
                other.gameObject.GetComponent<PlayerStats>().Damage(damage);
            }
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Battery")
        {
            other.gameObject.GetComponent<Battery>().Damage(damage);
            Destroy(gameObject);
        }
        //Dont damage enemy
        else if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Item")
        {
            return;
        }
        //If hits solid surface, destroy projectile
        else if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

    //Destorys object if it has been 5 seconds without being destroyed
    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
