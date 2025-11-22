using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BCWaterDetect : MonoBehaviour
{
    private BackCrabWater bcw;
    private float ambushTimer = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        bcw = gameObject.GetComponentInParent<BackCrabWater>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ambushTimer < 3.0f)
        {
            ambushTimer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && ambushTimer >= 3.0f)
        {
            bcw.Attack();
            ambushTimer = 0.0f;
        }
    }
}
