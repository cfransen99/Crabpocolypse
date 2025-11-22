using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    private SoundManager sm;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("CarryOver").GetComponent<SoundManager>();
    }

    public void WinEvent()
    {
        sm.sfxPlayer.PlayOneShot(sm.soundKaching);
    }
}
