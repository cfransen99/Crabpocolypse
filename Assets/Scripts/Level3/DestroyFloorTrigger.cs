using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFloorTrigger : MonoBehaviour
{
    public GameObject[] floors;
    private AudioSource explosion;
    private DataCarryOver dco;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        explosion = GetComponent<AudioSource>();
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dco.bossHP = 150;
            dco.bossAidHP = 25;
            other.GetComponent<PlayerMovement>().DisableMovement();
            explosion.Play();
            yield return new WaitForSeconds(.5f);
            foreach(GameObject floor in floors)
            {
                Destroy(floor);
            }
        }
    }
}
