using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nomouse : MonoBehaviour
{
    public Button mainbutton;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //El ratón se hace invisible
        Cursor.lockState = CursorLockMode.Locked; //Bloquea el ratón en el centro de la pantalla (Ideal para juegos FPS)
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mainbutton.Select();
        }
    }
}
