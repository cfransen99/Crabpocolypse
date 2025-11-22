using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMesh number;
    public Color colorPos, colorNeg, colorInterrupt, colorArmor, colorEvade;
    private Vector3 startPos, endPos;
    private float killTimer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = new Vector3(startPos.x, startPos.y + 1.0f, startPos.z);
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

    public void SetDamage(int amount)
    {
        number.text = "" + amount;
        if (amount > 0)
        {
            number.color = colorPos;
        }
        else if (amount < 0)
        {
            number.color = colorNeg;
        }
    }

    public void SetMessage(string msg)
    {
        number.text = msg;
        number.fontSize = 20;
        if (msg == "Interrupted!")
        {
            number.color = colorInterrupt;
        }
        else if (msg == "Armored!")
        {
            number.color = colorArmor;
        }
        else if (msg == "Evaded!")
        {
            number.color = colorEvade;
        }
    }
}
