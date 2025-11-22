using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAura : MonoBehaviour
{
    private Vector3 startPos, endPos;
    private float killTimer;
    private float auraHeight;

    // Start is called before the first frame update
    void Start()
    {
        float rand = Random.Range(0.1f, 0.2f);
        transform.localScale = new Vector3(rand, rand, rand);
        startPos = transform.position;
        endPos = new Vector3(startPos.x, startPos.y + auraHeight, startPos.z);
        killTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        killTimer += Time.deltaTime;
        if (killTimer < 0.5f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, killTimer * 2.0f);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHeight(float height)
    {
        auraHeight = height;
    }
}
