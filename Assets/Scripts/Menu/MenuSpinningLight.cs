using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSpinningLight : MonoBehaviour
{
    public bool reverse;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (reverse)
        {
            transform.Rotate(new Vector3(0, 0, -speed) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
        }
    }
}
