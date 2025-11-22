using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crabbit : EnemyBase
{
    public Transform[] points = new Transform[2];
    [SerializeField] private int destination = 0;

    public LayerMask groundLayer;

    public float rayDist;

    public float jumpForce;
    public float chaseMultiplier;
    private float initJumpForce;
    public float attackDistance;
    public float speed;
    public bool disableMovement;

    [SerializeField] protected EnemyStates currentState;

    public bool doOnce;

    public float direction;

    protected enum EnemyStates
    {
        Patrolling,
        Chasing
    }

    public override void Start()
    {
        base.Start();
        initJumpForce = jumpForce; 
    }

    public override void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        base.Move();

        if (!isAdvantage && !disableMovement)
        {
            if (distance > attackDistance)
            {
                currentState = EnemyStates.Patrolling;
            }
            else
            {
                currentState = EnemyStates.Chasing;
            }

            if (currentState == EnemyStates.Patrolling)
            {
                jumpForce = initJumpForce;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(points[destination].position.x, transform.position.y, transform.position.z),
                    speed * Time.deltaTime);
                if (OnGround())
                {
                    Hop();
                }
                else
                {
                    DontChangeDirection();
                }
                if (transform.position.x == points[destination].position.x && OnGround())
                {
                    SetNextPoint();
                }
                doOnce = false;

            }
            else
            {
                if (!doOnce)
                {
                    jumpForce *= chaseMultiplier;
                    doOnce = true;
                }

                if (OnGround())
                {
                    Hop();
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z),
                    speed * Time.deltaTime);
                }
                else
                {
                    DontChangeDirection();
                }
            }
        }
    }

    public void SetNextPoint()
    {
        destination = (destination + 1) % points.Length;
    }

    public void Hop()
    {
        r.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        GetDirection();
    }

    public bool OnGround()
    {
        return Physics.Raycast(transform.position, Vector3.down, rayDist, groundLayer);
    }

    public void DontChangeDirection()
    {
        if(direction < 0)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if(direction > 0)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
    }

    public void GetDirection()
    {
        if(currentState == EnemyStates.Patrolling)
        {
            direction = transform.position.x - points[destination].position.x;
        }
        else
        {
            direction = transform.position.x - player.position.x;
        }
    }

    public override void Attack()
    {

    }
}
