using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject mainCamera, perspectiveCamera;
    public bool isPerspective;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !isPerspective) 
        {
            mainCamera.SetActive(false);
            perspectiveCamera.SetActive(true);
            isPerspective = true;
        }
        else if (Input.GetKeyDown(KeyCode.C) && isPerspective)
        {
            perspectiveCamera.SetActive(false); 
            mainCamera.SetActive(true);
            isPerspective = false;
        }
    }
}
