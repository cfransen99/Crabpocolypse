using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawdiusProjectile : MonoBehaviour
{
    private Vector3 startPos, endPos;
    private float killTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(-6.0f, 1.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        killTimer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, endPos, killTimer / 0.3f);
        if (killTimer >= 0.3f)
        {
            Destroy(gameObject);
        }
    }
}
