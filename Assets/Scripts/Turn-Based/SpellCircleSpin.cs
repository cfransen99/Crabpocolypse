using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircleSpin : MonoBehaviour
{
    public Color fadeColor, fullColor;
    private float killTimer = 0.0f;
    private SpriteRenderer rend;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 100) * Time.deltaTime);

        killTimer += Time.deltaTime;
        if (killTimer > 1.0f)
        {
            rend.color = Color.Lerp(fullColor, fadeColor, (killTimer - 1.0f) * 3.0f);
        }
        else
        {
            rend.color = Color.Lerp(fadeColor, fullColor, killTimer);
        }

        if (killTimer >= 1.33f)
        {
            Destroy(gameObject);
        }
    }
}
