using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossFightCannon : Cannon
{
    [SerializeField] private LayerMask shipLayer;
    public SpriteRenderer[] aimers = new SpriteRenderer[2];

    private DataCarryOver dco;

    public override void Start()
    {
        base.Start();
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
    }

    public bool CheckShipLocation()
    {
        return Physics.BoxCast(firePosition.position, new Vector3(5, 5, 5), Vector3.forward, Quaternion.identity, 45, shipLayer);
    }


    public void AddAimer()
    {
        foreach (SpriteRenderer aimer in aimers)
        {
            aimer.enabled = true;
        }
    }

    public void RemoveAimer()
    {
        foreach (SpriteRenderer aimer in aimers)
        {
            aimer.enabled = false;
        }
    }

    public override void Interact()
    {
        AddAimer();

        if (Input.GetKeyDown(KeyCode.E) && playerMove.canMove)
        {
            playerSprite.enabled = false;

            playerMove.DisableMovement();

            RemoveAimer();

            AimCannon();
        }
    }

    public override void Uninteract()
    {
        RemoveAimer();
        base.Uninteract();
    }

    public bool PlayerMissShip()
    {
        return false;
    }

    public bool PlayerHitShip()
    {
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3(firePosition.position.x, firePosition.position.y, firePosition.position.z + 45), new Vector3(10, 10, 10));
    }
}
