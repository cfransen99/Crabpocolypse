using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float moveSpeed;

    public bool lookRight;

    private void Update()
    {
        lookRight = player.GetComponent<PlayerMovement>().lookRight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lookRight)
        {
            transform.position = Vector3.Lerp(transform.position, player.position + offset, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x - offset.x, player.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }
}
