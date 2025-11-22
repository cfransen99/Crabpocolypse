using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : BossBase
{
    public Transform position0;
    public Transform position1;

    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;

    private int destination = 0;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
    }

    private void Update()
    {
        Move();
    }

    public override void Move()
    {
        if(destination == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(position0.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, position0.position) < 3)
            {
                transform.LookAt(position1.position);
                destination = 1;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(position1.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, position1.position) < 3)
            {
                transform.LookAt(position0.position);
                transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
                destination = 0;
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAdvantage = true;
        }
    }
}
