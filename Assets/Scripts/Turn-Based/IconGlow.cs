using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconGlow : MonoBehaviour
{
    public Color fadeColor, fullColor;
    private float fadeTimer = 0.0f;
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        fadeTimer += Time.deltaTime;
        if (fadeTimer > 2.0f)
        {
            fadeTimer = 0.0f;
        }

        if (fadeTimer > 1.0f)
        {
            img.color = Color.Lerp(fullColor, fadeColor, fadeTimer - 1.0f);
        }
        else
        {
            img.color = Color.Lerp(fadeColor, fullColor, fadeTimer);
        }
    }
}
