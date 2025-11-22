using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarrier : PlayerAbilityBase
{
    [SerializeField] private GameObject shield;
    private bool usedShield = false;

    [SerializeField] private Animator shieldAnim;
    
    public override void Use()
    {
        shield.SetActive(true);
        usedShield = true;

        StartCoroutine(DriveDrain());
    }

    public void NoShield()
    {
        usedShield = false;
        StopCoroutine(DriveDrain());
        
        shieldAnim.SetTrigger("closeShield");
        StartCoroutine(CloseShield());
    }

    public bool UsedShield()
    {
        return usedShield;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(usedShield)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                player.isAmbush = false;
                
                if (player.isOverdrive)
                {
                    collision.gameObject.GetComponent<EnemyBase>().Die();
                }
            }
        }
    }

    public IEnumerator DriveDrain()
    {
        while (usedShield)
        {
            yield return new WaitForSeconds(.1f);
            stats.playerDrive -= cost;
        }
    }

    public IEnumerator CloseShield()
    {
        yield return new WaitForSeconds(.3f);
        shield.SetActive(false);
    }
}
