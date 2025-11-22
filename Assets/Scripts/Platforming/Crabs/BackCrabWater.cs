using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCrabWater : EnemyBase
{
    public float jumpForce = 15;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void Attack()
    {
        r.AddForce(new Vector2(0, 1) * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && applyAmbush)
        {
            collision.gameObject.GetComponent<Player>().isAmbush = ApplyAmbush();
        }
    }
}
