using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Transform endpoint;
    [SerializeField] private float speed;

    private List<GameObject> onBelt;

    float distance;

    private void Start()
    {
        onBelt = new List<GameObject>();
    }

    private void FixedUpdate()
    {
        if (onBelt.Count > 0)
        {
            foreach (GameObject gameObject in onBelt)
            {
                if(gameObject != null)
                {
                    distance = endpoint.transform.position.x - gameObject.transform.position.x;

                    if (distance == 0)
                    {
                        return;
                    }
                    else if (distance > 0)
                    {
                        Rigidbody r = gameObject.GetComponent<Rigidbody>();
                        r.velocity = new Vector3(speed * Time.deltaTime, r.velocity.y);
                    }
                    else
                    {
                        Rigidbody r = gameObject.GetComponent<Rigidbody>();
                        r.velocity = new Vector3(-speed * Time.deltaTime, r.velocity.y);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody r = collision.gameObject.GetComponent<Rigidbody>();
            r.velocity = new Vector3(0.0f, r.velocity.y);
        }
        else
        {
            Rigidbody r = collision.gameObject.GetComponent<Rigidbody>();
            r.velocity = new Vector3(r.velocity.x, r.velocity.y);
        }
    }

    public void RemoveFromBelt(GameObject removeThis)
    {
        onBelt.Remove(removeThis);
    }
}
