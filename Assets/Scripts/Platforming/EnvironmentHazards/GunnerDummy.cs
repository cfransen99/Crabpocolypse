using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunnerDummy : MonoBehaviour
{
    public GameObject Spawn;
    public Transform gunSpot;
    public Rigidbody rb;
    public bool inRegularLevel;

    private float spawnTimer = 0.0f;
    private bool isFalling = false;

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            transform.position = gunSpot.position;
        }

        if (inRegularLevel)
        {
            if (rb.velocity.magnitude < 0.01f && spawnTimer > 0.1f)
            {
                GameObject temp = Instantiate(Spawn, transform.position, transform.rotation);
                temp.transform.GetChild(0).GetComponent<RoboCrab>().MakeVulnerable();
                Destroy(gameObject);
            }
        }
    }

    public void Fall()
    {
        rb.useGravity = true;
        isFalling = true;
    }
}
