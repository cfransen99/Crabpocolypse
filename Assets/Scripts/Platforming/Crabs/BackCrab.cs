using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCrab : EnemyBase
{
    private SpriteRenderer spriteRenderer;
    private Collider cc;
    public float ambushDistance;
    protected bool isSeen = false;
    public float jumpForce;
    public float giveUpDistance;
    public LayerMask groundLayer;
    private bool stopAmbush = false;

    public float rayDist;

    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cc = GetComponent<Collider>();
        spriteRenderer.enabled = false;
        agent.enabled = false;
        r.constraints = RigidbodyConstraints.FreezeAll;
        cc.enabled = false;

    }

    public override void Update()
    {
        base.Update();
        if (stopAmbush && OnGround())
        {
            agent.enabled = true;
            applyAmbush = false;
            stopAmbush = false;
        }
    }

    public override void Move()
    {
        if (agent.enabled && agent.isStopped)
        {
            animator.SetBool("isRunning", false);
        }
        if (isSeen && distance < giveUpDistance && agent.enabled)
        {
            agent.SetDestination(player.position);
            base.Move();
            return;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        if (isAdvantage)
        {
            agent.enabled = false;
            vulnIndicator.SetActive(true);
            advantageTimer += Time.deltaTime;
            if (advantageTimer > 2.0f)
            {
                advantageTimer = 0.0f;
                agent.enabled = true;
                isAdvantage = false;
                vulnIndicator.SetActive(false);
            }
        }
    }

    public override void Attack()
    {
        if(!isSeen && distance < ambushDistance)
        {
            r.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            applyAmbush = true;
            spriteRenderer.enabled = true;

            if(transform.position.x - player.position.x > 0)
            {
                r.AddForce(new Vector2(-1, 1) * jumpForce, ForceMode.Impulse);
            }
            else
            {
                r.AddForce(new Vector2(1, 1) * jumpForce, ForceMode.Impulse);
            }
            isSeen = true;
            StartCoroutine(WaitCOR());
        }
    }

    public bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayDist, groundLayer);
    }

    IEnumerator WaitCOR()
    {
        yield return new WaitForSeconds(.5f);
        stopAmbush = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && applyAmbush)
        {
            collision.gameObject.GetComponent<Player>().isAmbush = ApplyAmbush();
        }
    }
}
