using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandingBubble : MonoBehaviour
{
    public float smallSize, bigSize;
    public Color fadeColor, fullColor;
    private float killTimer = 0.0f;
    private Vector3 startSize, endSize;
    private MeshRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        startSize = new Vector3(smallSize, smallSize, smallSize);
        endSize = new Vector3(bigSize, bigSize, bigSize);
        rend = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        killTimer += Time.deltaTime;
        gameObject.transform.localScale = Vector3.Lerp(startSize, endSize, killTimer * (1.0f / 0.3f));
        if (killTimer > 0.06f)
        {
            rend.material.color = Color.Lerp(fullColor, fadeColor, (killTimer - 0.025f) * (1.0f / 0.24f));
        }
        else
        {
            rend.material.color = Color.Lerp(fadeColor, fullColor, killTimer * (1.0f / 0.06f));
        }

        if (killTimer >= 0.3f)
        {
            Destroy(gameObject);
        }
    }
}
