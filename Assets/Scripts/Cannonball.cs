using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public GameObject explosionEffect;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayers;

    [SerializeField] private int damage;

    private void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();   
    }

    public void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, playerLayers);

        foreach(Collider collider in colliders)
        {
            if(collider.GetComponent<PlayerStats>() != null)
                collider.GetComponent<PlayerStats>().Damage(damage);
            else
            {
                collider.GetComponent<Battery>().Damage(damage);
            }
        }

        Destroy(transform.parent.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
