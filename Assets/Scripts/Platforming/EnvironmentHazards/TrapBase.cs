using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrapBase : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected Player player;
    protected PlayerStats playerStats;
    protected Rigidbody playerRigidbody;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerStats = player.GetComponent<PlayerStats>();
        playerRigidbody = player.GetComponent<Rigidbody>();
    }


    public abstract void Act();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(playerStats.playerHealth > 0)
            {
                playerStats.Damage(damage);
                playerRigidbody.AddForce(-collision.GetContact(0).normal * player.Bounce(), ForceMode.Impulse);
            }
        }
    }
}
