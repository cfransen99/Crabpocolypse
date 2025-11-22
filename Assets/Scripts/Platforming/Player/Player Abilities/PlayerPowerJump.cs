using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerJump : PlayerAbilityBase
{
    [SerializeField] private float jumpForce;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemyLayer;

    public override void Use()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundPJump);
        timeBetweenAbility = abilityCooldown;
        stats.playerDrive -= cost;


        if (!player.isOverdrive)
        {
            r.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
        else
        {
            r.AddForce(Vector2.up * (jumpForce * 1.1f), ForceMode.Impulse);
            Collider[] enemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

            foreach(Collider enemy in enemies)
            {
                enemy.GetComponent<EnemyBase>().Die();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
