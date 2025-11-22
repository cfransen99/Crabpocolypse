using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBarricade : MonoBehaviour
{
    private SaveHandler saveHandler;
    private DataCarryOver dco;
    private SoundManager sm;
   
    public bool isBroken;

    public int ID;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(.1f);
        saveHandler = GameObject.FindGameObjectWithTag("SaveHandler").GetComponent<SaveHandler>();
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();
        sm = dco.gameObject.GetComponent<SoundManager>();

        if (saveHandler.GetDidLoadSave())
        {
            isBroken = saveHandler.isBroken[ID];
        }
        else
        {
            isBroken = dco.isBroken[ID];
        }

        if (isBroken)
        {
            BreakOnLoad();
        }
    }

    public void BreakOnLoad()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }

    public void Break()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundLandHeavy);
        isBroken = true;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        dco.isBroken[ID] = isBroken;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();

            if(player.isOverdrive && player.isDropKick)
            {
                Break();
            }
        }
    }
}
