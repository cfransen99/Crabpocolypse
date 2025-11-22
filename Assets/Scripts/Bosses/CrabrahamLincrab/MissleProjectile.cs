using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    public void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.up, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
