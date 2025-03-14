using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private FadeScreen _fd;

    public string levelToLoad;

    public static bool hasPortalTwoKey;
    public static bool hasPortalThreeKey;
    public static int lastPortal;
    public static int questNumber;
    public static int joaquinDialog;

    void Start()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitLevel()
    {
        StartCoroutine(ExitLevelCo());
    }

    public IEnumerator ExitLevelCo()
    {
        yield return new WaitForSeconds(0f);
        SceneManager.LoadScene(levelToLoad);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Saliendo de la aplicacion");
    }
}
