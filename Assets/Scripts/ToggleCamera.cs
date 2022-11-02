using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCamera : MonoBehaviour
{
    public GameObject p1Camera;
    public GameObject p2Camera;


    // Start is called before the first frame update
    void Start()
    {
        ToggleP1Camera();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleP1Camera();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleP2Camera();
        }

    }

    public void ToggleP1Camera()
    {
        p1Camera.SetActive(true);
        p1Camera.GetComponent<AudioListener>().enabled = true;
        p1Camera.tag = "MainCamera";
        p2Camera.SetActive(false);
        p2Camera.GetComponent<AudioListener>().enabled = false;
        p2Camera.tag = "Untagged";
        p2Camera.tag = "Untagged";
    }

    public void ToggleP2Camera()
    {
        p1Camera.SetActive(false);
        p1Camera.GetComponent<AudioListener>().enabled = false;
        p1Camera.tag = "Untagged";
        p2Camera.SetActive(true);
        p2Camera.GetComponent<AudioListener>().enabled = true;
        p2Camera.tag = "MainCamera";
    }

}
