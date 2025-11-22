using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightFloor : MonoBehaviour
{
    public int numWeights = 4;
    public SpriteRenderer sr;
    public Sprite[] damageSprites;

    private int damage = -1;
    private float dropTimer = 0.0f;
    private Vector3 startPos, endPos;
    private SoundManager sm;

    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
        startPos = transform.localPosition;
        endPos = new Vector3(startPos.x, -15.0f, startPos.z);
    }

    private void Update()
    {
        if (numWeights == 0)
        {
            dropTimer += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, endPos, dropTimer * 2.0f);
            if (dropTimer > 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Weight")
        {
            Destroy(other.gameObject);
            numWeights--;
            damage++;

            if (numWeights > 0)
            {
                sm.sfxPlayer.PlayOneShot(sm.soundLandMedium);
                sr.sprite = damageSprites[damage];
            }
            else
            {
                sm.sfxPlayer.PlayOneShot(sm.soundLandHeavy);
            }
        }
    }
}
