using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    public GameObject playerOneCamera;
    public GameObject playerTwoCamera;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerOneCamera.SetActive(true);
            playerTwoCamera.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerOneCamera.SetActive(false);
            playerTwoCamera.SetActive(true);
        }

    }
}
