using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuMario : MonoBehaviour
{
    public Transform mainCameraWaypoint;
    public Button mainButton;

    public float timeToDeactivateFS;

    private CameraTraveling _cameraTraveling;

    private void Start()
    {
        _cameraTraveling = Camera.main.GetComponent<CameraTraveling>();

        if(mainButton != null)
        {
            mainButton.Select();
        }
        else
        {
            Debug.Log("Falta asignar el bot�n principal!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && mainCameraWaypoint != null)
        {
            Escape();
        }
    }

    public void Escape()
    {
        if (_cameraTraveling.gameObject.transform.parent != null)
        {
            _cameraTraveling.gameObject.transform.parent = null;
        }

        _cameraTraveling.target = mainCameraWaypoint;

        mainButton.Select();
    }

    public void QuitApplication()
    {
        StartCoroutine(QuitApplicationCO());
    }

    IEnumerator QuitApplicationCO()
    {
        yield return new WaitForSeconds(4f);

        Application.Quit();
    }
}
