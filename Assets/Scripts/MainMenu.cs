using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        // SceneManager.LoadScene("SinglePlayer");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("Multiplayer");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
