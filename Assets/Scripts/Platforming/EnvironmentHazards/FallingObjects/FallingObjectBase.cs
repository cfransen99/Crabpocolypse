using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallingObjectBase : MonoBehaviour
{

    protected bool isActive;
    protected BoxCollider boxCollider;
    public LayerMask groundLayer;
    protected SoundManager sm;
    
    public virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
    }

    public virtual void Update()
    {
        //if (OnGround())
        //{
        //    isActive = false;
        //}
    }

    public bool OnGround()
    {
        return Physics.Raycast(boxCollider.bounds.center, Vector3.down, (boxCollider.bounds.size.y / 2) , groundLayer);
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (isActive)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Player>().PlayDieAnim();
            }
            else if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<EnemyBase>().Die();
                sm.sfxPlayer.PlayOneShot(sm.soundHurt);
            }
        }
        if(OnGround())
        {
            isActive = false;
        }
    }

    public abstract void Activate();
}
