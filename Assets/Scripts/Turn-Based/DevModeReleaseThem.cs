using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevModeReleaseThem : MonoBehaviour
{
    private float killTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        killTimer += Time.deltaTime;
        if (killTimer >= 1.5f)
        {
            Destroy(gameObject);
        }
    }
}
