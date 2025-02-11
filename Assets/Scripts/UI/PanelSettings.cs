using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSettings : MonoBehaviour
{
    public GameObject[] panels;
    public int panelIndex;
    public bool settingsOn;
    public Button mainGeneralButton, mainSoundButton, mainScreenButton, mainControlsButton;
    public AudioSource changeSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (settingsOn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeRight();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                ChangeLeft();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelIndex = 0;
            settingsOn = false;
            ChangePanel();
        }
    }

    public void ChangeRight()
    {
        panelIndex++;
        if (panelIndex >= panels.Length)
        {
            panelIndex = 0;
        }
        ChangePanel();
        changeSound.Play();
    }

    public void ChangeLeft()
    {
        panelIndex--;
        if (panelIndex < 0)
        {
            panelIndex = panels.Length - 1;
        }
        ChangePanel();
        changeSound.Play();
    }
    void ChangePanel()
    {
        switch (panelIndex) 
        {
            case 0:
                panels[0].SetActive(true); 
                panels[1].SetActive(false); 
                panels[2].SetActive(false); 
                panels[3].SetActive(false);

                if(settingsOn) mainGeneralButton.Select();
                break;
            case 1:
                panels[0].SetActive(false); 
                panels[1].SetActive(true); 
                panels[2].SetActive(false); 
                panels[3].SetActive(false);

                if (settingsOn) mainSoundButton.Select();
                break;
            case 2:
                panels[0].SetActive(false);
                panels[1].SetActive(false);
                panels[2].SetActive(true);
                panels[3].SetActive(false);

                if (settingsOn) mainScreenButton.Select();
                break;
            case 3:
                panels[0].SetActive(false);
                panels[1].SetActive(false);
                panels[2].SetActive(false);
                panels[3].SetActive(true);

                if (settingsOn) mainControlsButton.Select();
                break;
            default:
                panels[0].SetActive(true);
                panels[1].SetActive(false);
                panels[2].SetActive(false);
                panels[3].SetActive(false);

                if (settingsOn) mainGeneralButton.Select();
                break;
        }
    }

    public void ActivateSettings()
    {
        settingsOn = true;
    }

    public void ResetSettings()
    {
        panelIndex = 0;
        ChangePanel();
    }
}
