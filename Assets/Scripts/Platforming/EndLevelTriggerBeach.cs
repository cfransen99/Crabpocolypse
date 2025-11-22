using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class EndLevelTriggerBeach : MonoBehaviour
{
    [SerializeField] private string levelName;
    [SerializeField] private CinemachineVirtualCamera cam;

    [SerializeField] private Transform whereToMove;

    private DataCarryOver dco;

    [SerializeField] private Vector3 newLevelPos;

    private LevelLoader levelLoader;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            dco.ResetCarryOver();
            SetDCOPos();
            PlayerMovement playerMove = other.GetComponent<PlayerMovement>();
            playerMove.DisableMovement();

            cam.LookAt = null;
            cam.Follow = null;

            playerMove.MoveOnX(whereToMove);

            StartCoroutine(WaitToStartNextScene());
        }
    }

    public IEnumerator WaitToStartNextScene()
    {
        yield return new WaitForSeconds(2);

        levelLoader = FindObjectOfType<LevelLoader>();
        levelLoader.LoadNewLevel(levelName);
    }

    public void SetDCOPos()
    {
        dco.playerPosX = newLevelPos.x;
        dco.playerPosY = newLevelPos.y;
        dco.playerPosZ = newLevelPos.z;
    }
}
