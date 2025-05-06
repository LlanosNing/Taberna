using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public GameObject exitButton;
    public Button button;
    public float timeToCanExit;
    float timeToCanExitCounter;
    bool canExit;
    // Start is called before the first frame update
    void Start()
    {
        timeToCanExitCounter = timeToCanExit;
    }

    // Update is called once per frame
    void Update()
    {
        timeToCanExitCounter -= Time.deltaTime;

        if (Input.anyKeyDown && timeToCanExitCounter <= 0 && !canExit)
        {
            canExit = true;

            exitButton.SetActive(true);

            button.Select();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && canExit)
        {
            ReturnToMainMenu();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
