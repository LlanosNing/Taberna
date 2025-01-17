using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public bool isTutorial;
    public GameObject worldCanvas;

    public bool inTrigger;
    public bool tutorialActive;

    public GameObject tutorialCanvas;
    UIController uIRef;
    // Start is called before the first frame update
    void Start()
    {
        uIRef = GameObject.FindWithTag("Canvas").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isTutorial && inTrigger && !tutorialActive && uIRef.canAccessTutorials)
        {
            tutorialCanvas.SetActive(true);

            Time.timeScale = 0;

            uIRef.canAccessOptions = false;

            tutorialActive = true;
        }
        else if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && isTutorial && inTrigger && tutorialActive)
        {
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
