using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabWitch : EnemyBase
{
    public ProjectileBase[] projectile = new ProjectileBase[2];
    [SerializeField] private float[] projectileDamage = new float[2];
    public Transform projectileSpawn;
    
    public float rateOfFire;
    private float lastFired;

    public float attackDist;

    public override void Start()
    {
        base.Start();
        lastFired = Time.time;
    }

    public override void Update()
    {
        animator.SetBool("isRunning", false);

        distance = Vector3.Distance(transform.position, player.position);

        //If player is in range, aim at them and attack
        if (distance < attackDist && !isAdvantage)
        {
            Aim();
            Attack();
        }

        if (isAdvantage)
        {
            vulnIndicator.SetActive(true);
            advantageTimer += Time.deltaTime;
            if (advantageTimer > 2.0f)
            {
                advantageTimer = 0.0f;
                isAdvantage = false;
                vulnIndicator.SetActive(false);
            }
        }
    }

    //Fire the projectiles at a set rate of fire
    public override void Attack()
    {
        if (!isAdvantage)
        {
            if (Time.time - lastFired > 1 / rateOfFire)
            {
                lastFired = Time.time;
                ChooseProjectile();
            }
        }
    }

    //Aims the spell spawner at the player and orients it so that it is looking down at the player
    public void Aim()
    {
        projectileSpawn.LookAt(player);

        projectileSpawn.RotateAround(projectileSpawn.position, projectileSpawn.right, -90);
        projectileSpawn.RotateAround(projectileSpawn.position, projectileSpawn.up, -90);
    }

    //Randomly chooses which projectile for fire
    public void ChooseProjectile()
    {
        int random = Random.Range(0, 100);
        if(random < 50)
        {
            float damage = projectileDamage[0] + (level/1.5f);
            ProjectileBase bullet = Instantiate(projectile[0], projectileSpawn.position, projectileSpawn.rotation);
            bullet.SetDamage(damage);
        }
        else
        {
            float damage = projectileDamage[1] + (level/1.5f);
            ProjectileBase bullet = Instantiate(projectile[1], projectileSpawn.position, projectileSpawn.rotation);
            bullet.SetDamage(damage);
        }
    }
}
