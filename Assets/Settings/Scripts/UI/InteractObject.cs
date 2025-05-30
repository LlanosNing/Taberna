using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public bool isTutorial, isNPC;
    public GameObject worldCanvas, exclamationMark;

    public bool inTrigger;
    public bool tutorialActive;
    public GameObject objectToDeactivate;

    public GameObject tutorialCanvas, whatTutorial;
    UIController uIRef;
    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name != "InteriorTaberna")
            uIRef = GameObject.FindWithTag("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isNPC && inTrigger)
        {
            worldCanvas.SetActive(false);
            exclamationMark.SetActive(false);

            if(SceneManager.GetActiveScene().name == "InteriorTaberna")
            {
                objectToDeactivate.SetActive(false);
            }
        }

        else if (Input.GetKeyDown(KeyCode.E) && isTutorial && inTrigger && !tutorialActive && uIRef.canAccessTutorials)
        {
            tutorialCanvas.SetActive(true);

            whatTutorial.SetActive(true);

            Time.timeScale = 0;

            uIRef.canAccessOptions = false;

            tutorialActive = true;
        }
        else if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && isTutorial && inTrigger && tutorialActive)
        {
            whatTutorial.SetActive(false);

            tutorialCanvas.SetActive(false);

            Time.timeScale = 1f;

            uIRef.canAccessOptions = true;

            tutorialActive = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            worldCanvas.SetActive(true);

            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            worldCanvas.SetActive(false);

            inTrigger = false;
        }
    }
}
