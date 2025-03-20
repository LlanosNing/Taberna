using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public string levelToLoad;

    public static bool hasPortalTwoKey;
    public static bool hasPortalThreeKey;
    public static int lastPortal;
    public static int questNumber;
    public static int joaquinDialog;

    void Start()
    {
        if (hasPortalTwoKey)
        {
            Debug.Log("Tienes la llave 2");
        }
        else
        {
            Debug.Log("No tienes la llave 2");
        }
    }
    private void Update()
    {
    }
}
