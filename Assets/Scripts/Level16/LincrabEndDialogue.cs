using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LincrabEndDialogue : MonoBehaviour
{
    [SerializeField] private Animator crossfade;

    private DataCarryOver dco;
    private SoundManager sm;

    private void Start()
    {
        dco = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<DataCarryOver>();

        sm = dco.GetComponent<SoundManager>();
    }

    public void IsCharging()
    {
        crossfade.SetTrigger("Start");
        sm.sfxPlayer.PlayOneShot(sm.soundClang);
        StartCoroutine(WaitToFaidIn());
    }

    public IEnumerator WaitToFaidIn()
    {
        yield return new WaitForSeconds(1.5f);
        crossfade.SetTrigger("FadeIn");
    }
}
