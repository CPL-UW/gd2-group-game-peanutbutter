using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoNotDestroy : MonoBehaviour
{
    public GameObject music;

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");

        if (musicObj.Length > 1 || sceneName == "Multiplayer" || sceneName == "SinglePlayer")
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        if (musicObj.Length < 1 && sceneName == "MainMenu")
        {
            Instantiate(music, new Vector2(0, 0), Quaternion.identity);
        }
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == "Multiplayer" || sceneName == "SinglePlayer")
        {
            Destroy(this.gameObject);
        }
    }
}
