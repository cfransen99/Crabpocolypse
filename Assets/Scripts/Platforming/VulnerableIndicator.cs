using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableIndicator : MonoBehaviour
{
    private float disableTimer = 0.0f;
    public bool onZero;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
        if (gameObject.activeSelf && !onZero)
        {
            disableTimer += Time.deltaTime;
            if (disableTimer >= 2.0f)
            {
                disableTimer = 0.0f;
                gameObject.SetActive(false);
            }
        }
    }
}
