using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : MonoBehaviour
{
    public GunnerDummy dummy;
    public GameObject laser, gunBody, bullet;
    public Color activeColor, disabledColor;
    public Renderer[] trimRend;
    public Transform topPos, botPos, gunPos, bulletPos;
    public float moveInterval;
    
    private Vector3 endPos, gunRot1, gunRot2;
    private Material[] trimMat;
    private float moveTimer = 0.0f;
    private float shootTimer = 0.0f;
    private float cdTimer = 0.0f;
    private bool isDisabled, doneDisabling, isShooting, onCooldown;
    private int shotsFired;

    // Start is called before the first frame update
    void Start()
    {
        gunRot1 = new Vector3(0.0f, 0.0f, 0.0f);
        gunRot2 = new Vector3(0.0f, 0.0f, 30.0f);
        isDisabled = false;
        doneDisabling = false;

        trimMat = new Material[trimRend.Length];
        for (int i = 0; i < trimRend.Length; i++)
        {
            trimMat[i] = trimRend[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.K))
        {
            DisableGun();
        }*/

        if (!isDisabled)
        {
            if (!isShooting && !onCooldown)
            {
                moveTimer += Time.deltaTime;
            }

            if (moveTimer > moveInterval * 2.0f)
            {
                moveTimer = 0.0f;
            }

            if (moveTimer > moveInterval)
            {
                gunPos.position = Vector3.Lerp(botPos.position, topPos.position, (moveTimer - moveInterval) * (1.0f / moveInterval));
            }
            else
            {
                gunPos.position = Vector3.Lerp(topPos.position, botPos.position, moveTimer * (1.0f / moveInterval));
            }
        }
        else if (!doneDisabling)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer > 2.0f)
            {
                doneDisabling = true;
            }
            else
            {
                gunPos.position = Vector3.Lerp(endPos, botPos.position, moveTimer / 2.0f);
                gunBody.transform.localRotation = Quaternion.Lerp(Quaternion.Euler(gunRot1), Quaternion.Euler(gunRot2), moveTimer / 2.0f);
                foreach (Material m in trimMat)
                {
                    m.SetColor("_EmissionColor", Color.Lerp(activeColor, disabledColor, moveTimer / 2.0f));
                }
            }
        }

        if (isShooting && !isDisabled)
        {
            shootTimer += Time.deltaTime;
            if (shotsFired < 1)
            {
                GameObject temp = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                temp.GetComponent<ProjectileBase>().SetDamage(10.0f);
                shotsFired++;
            }
            else if (shootTimer > 0.33f && shotsFired < 2)
            {
                GameObject temp = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                temp.GetComponent<ProjectileBase>().SetDamage(10.0f);
                shotsFired++;
            }
            else if (shootTimer > 0.66f && shotsFired < 3)
            {
                GameObject temp = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
                temp.GetComponent<ProjectileBase>().SetDamage(10.0f);
                shotsFired = 0;
                isShooting = false;
                shootTimer = 0.0f;
                onCooldown = true;
                cdTimer = 0.0f;
            }
        }
        if (onCooldown)
        {
            cdTimer += Time.deltaTime;
            if (cdTimer >= 2.0f)
            {
                onCooldown = false;
                cdTimer = 0.0f;
                if (!isDisabled)
                laser.SetActive(true);
            }
        }
    }

    public void ShootGun()
    {
        shootTimer = 0.0f;
        laser.SetActive(false);
        isShooting = true;
    }

    public void DisableGun()
    {
        if (!isDisabled)
        {
            moveTimer = 0.0f;
            endPos = gunPos.position;
            laser.SetActive(false);
            dummy.Fall();
            isDisabled = true;
        }
    }
}
