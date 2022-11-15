using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuP2 : MonoBehaviour
{
    public static bool GameIsPausedP2 = false;
    public GameObject pauseMenuUIP2;
    public GameObject p2Camera;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPausedP2)
            {
                ResumeP2();
            }
            else if (p2Camera) {
                PauseP2();
            }
        }
    }

    public void ResumeP2() {
        pauseMenuUIP2.SetActive(false);
        Time.timeScale = 1f;
        GameIsPausedP2 = false;
    }

    void PauseP2() {
        pauseMenuUIP2.SetActive(true);
        Time.timeScale = 0f;
        GameIsPausedP2 = true;
    }

}
