using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObject : MonoBehaviour
{
    public bool isTutorial, isPlaceBomb;
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
        if (Input.GetKeyDown(KeyCode.E) && isTutorial && inTrigger && !tutorialActive)
        {
            tutorialCanvas.SetActive(true);

            Time.timeScale = 0;

            uIRef.canAccessOptions = false;
        }
        else if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)) && isTutorial && inTrigger && tutorialActive)
        {
            tutorialCanvas.SetActive(false);

            Time.timeScale = 1f;

            uIRef.canAccessOptions = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        worldCanvas.SetActive(true);

        inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        worldCanvas.SetActive(false);

        inTrigger = false;
    }
}
