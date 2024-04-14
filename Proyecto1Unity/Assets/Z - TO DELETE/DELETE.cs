using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DELETE : MonoBehaviour
{
    private bool slowTime = false;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            if (slowTime)
            {
                Time.timeScale = 1.0f;
                slowTime = false;
            }
            else
            {
                Time.timeScale = 0.25f;
                slowTime = true;
            }
        }

        if(Input.GetKeyDown (KeyCode.H))
        {
            CharacterManager.Hurt();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            CharacterManager.IsDead();
        }
    }
}
