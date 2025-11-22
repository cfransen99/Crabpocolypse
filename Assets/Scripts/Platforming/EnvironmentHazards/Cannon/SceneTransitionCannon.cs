using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionCannon : Cannon
{
    private LevelLoader levelLoader;

    public override void Update()
    {
        
    }

    public override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerMove.canMove)
        {
            playerSprite.enabled = false;

            playerMove.DisableMovement();

            levelLoader = FindObjectOfType<LevelLoader>();
            levelLoader.LoadNewLevel("Level15");

            AimCannon();
        }
    }
}
