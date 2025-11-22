using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level7Manager : MonoBehaviour
{
    private DataCarryOver dco;
    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        player.position = new Vector3(dco.playerPosX, dco.playerPosY, dco.playerPosZ);
    }
}
