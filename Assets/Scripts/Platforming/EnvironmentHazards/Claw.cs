using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public float timeFullPass = 5.0f;
    public Transform marker1, marker2, dropSpot, clawEntity, clawArm, clawFinger1, clawFinger2, loadPos;
    public Collider detectTrigger, grabTrigger;
    public GameObject mashArrows;
    public bool canMash;
    public int numMashes;

    private Vector3 corner1, corner2, corner3, corner4, clawUp, clawDown, fingerOpen1, fingerClose1, fingerOpen2, fingerClose2, grabSpot, sendSpot;
    private float moveTimer = 0.0f, grabTimer = 0.0f;
    private float timeQuarter;
    private bool foundPlayer, releasedPlayer;
    public GameObject load;

    // Start is called before the first frame update
    void Start()
    {
        corner1 = new Vector3(marker1.position.x, clawEntity.position.y, marker1.position.z);
        corner2 = new Vector3(marker2.position.x, clawEntity.position.y, marker1.position.z);
        corner3 = new Vector3(marker2.position.x, clawEntity.position.y, marker2.position.z);
        corner4 = new Vector3(marker1.position.x, clawEntity.position.y, marker2.position.z);
        timeQuarter = timeFullPass / 4.0f;
        fingerOpen1 = new Vector3(0.0f, 0.0f, 0.0f);
        fingerOpen2 = new Vector3(0.0f, 180.0f, 0.0f);
        fingerClose1 = new Vector3(0.0f, 0.0f, 45.0f);
        fingerClose2 = new Vector3(0.0f, 180.0f, 45.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (load != null && load.name == "Player")
        {
            mashArrows.SetActive(true);
        }
        else
        {
            mashArrows.SetActive(false);
        }

        if (!foundPlayer)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer <= timeQuarter)
            {
                clawEntity.position = Vector3.Lerp(corner1, corner2, moveTimer / timeQuarter);
            }
            else if (moveTimer <= 2 * timeQuarter)
            {
                clawEntity.position = Vector3.Lerp(corner2, corner3, (moveTimer - timeQuarter) / timeQuarter);
            }
            else if (moveTimer <= 3 * timeQuarter)
            {
                clawEntity.position = Vector3.Lerp(corner3, corner4, (moveTimer - (2 * timeQuarter)) / timeQuarter);
            }
            else if (moveTimer <= timeFullPass)
            {
                clawEntity.position = Vector3.Lerp(corner4, corner1, (moveTimer - (3 * timeQuarter)) / timeQuarter);
            }
            else
            {
                moveTimer = 0.0f;
            }
        }
        else
        {
            grabTimer += Time.deltaTime;
            if (grabTimer <= 1.0f)
            {
                clawArm.position = Vector3.Lerp(clawUp, clawDown, grabTimer);
                clawFinger1.localRotation = Quaternion.Lerp(Quaternion.Euler(fingerOpen1), Quaternion.Euler(fingerClose1), grabTimer);
                clawFinger2.localRotation = Quaternion.Lerp(Quaternion.Euler(fingerOpen2), Quaternion.Euler(fingerClose2), grabTimer);
                //clawFinger1.localEulerAngles = Vector3.Lerp(fingerOpen1, fingerClose1, grabTimer * 2);
                //clawFinger1.localEulerAngles = Vector3.Lerp(fingerOpen2, fingerClose2, grabTimer * 2);
            }
            else if (grabTimer <= 2.0f)
            {
                clawArm.position = Vector3.Lerp(new Vector3(clawEntity.position.x, clawDown.y, clawEntity.position.z), new Vector3(clawEntity.position.x, clawUp.y, clawEntity.position.z), (grabTimer - 1.0f));
                clawEntity.position = Vector3.Lerp(grabSpot, sendSpot, (grabTimer - 1.0f));
            }
            else if (grabTimer <= 5.0f)
            {
                canMash = true;
                clawEntity.position = Vector3.Lerp(sendSpot, dropSpot.position, (grabTimer - 2.0f) / 3.0f);
            }
            else if (grabTimer <= 5.5f)
            {
                clawFinger1.localRotation = Quaternion.Lerp(Quaternion.Euler(fingerClose1), Quaternion.Euler(fingerOpen1), (grabTimer - 5.0f) * 2);
                clawFinger2.localRotation = Quaternion.Lerp(Quaternion.Euler(fingerClose2), Quaternion.Euler(fingerOpen2), (grabTimer - 5.0f) * 2);
                //clawFinger1.localEulerAngles = Vector3.Lerp(fingerClose1, fingerOpen1, (grabTimer - 4.0f) * 2);
                //clawFinger1.localEulerAngles = Vector3.Lerp(fingerClose2, fingerOpen2, (grabTimer - 4.0f) * 2);
                if (load != null && load.name == "Player" && !releasedPlayer)
                {
                    load.GetComponent<PlayerMovement>().RestoreGrav();
                    //Already done by player function
                    //load = null;
                    releasedPlayer = true;
                }
                else if (load != null && load.name == "Weight" && !releasedPlayer)
                {
                    load.GetComponent<Weight>().DropFromClaw();
                    load = null;
                    releasedPlayer = true;
                }
            }
            else if (grabTimer <= 7.0f)
            {
                clawEntity.position = Vector3.Lerp(dropSpot.position, grabSpot, (grabTimer - 5.5f) / 1.5f);
            }
            else
            {
                grabTimer = 0.0f;
                foundPlayer = false;
                detectTrigger.enabled = true;
                grabTrigger.enabled = false;
                canMash = false;
            }
        }
    }

    public void SendGrab()
    {
        releasedPlayer = false;
        foundPlayer = true;
        grabTimer = 0.0f;
        grabSpot = new Vector3(clawEntity.position.x, clawEntity.position.y, clawEntity.position.z);
        sendSpot = new Vector3(grabSpot.x, grabSpot.y, dropSpot.position.z);
        clawUp = new Vector3(grabSpot.x, clawArm.transform.position.y, grabSpot.z);
        clawDown = new Vector3(grabSpot.x, clawArm.transform.position.y - 1.25f, grabSpot.z);
        detectTrigger.enabled = false;
        grabTrigger.enabled = true;
        canMash = false;
        numMashes = Random.Range(5, 11);
    }
}
