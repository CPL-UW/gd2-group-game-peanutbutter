using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuP1 : MonoBehaviour
{
    public static bool GameIsPausedP1 = false;
    public GameObject pauseMenuUIP1;
    public GameObject p1Camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPausedP1)
            {
                ResumeP1();
            }
            else if(p1Camera.activeSelf)
            {
                PauseP1();
            }
        }
    }

    public void ResumeP1()
    {
        pauseMenuUIP1.SetActive(false);
        Time.timeScale = 1f;
        GameIsPausedP1 = false;
    }

    void PauseP1()
    {
        pauseMenuUIP1.SetActive(true);
        Time.timeScale = 0f;
        GameIsPausedP1 = true;
    }

}