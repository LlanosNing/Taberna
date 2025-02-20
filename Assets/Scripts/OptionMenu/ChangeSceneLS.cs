using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneLS : MonoBehaviour
{
    public void GoToLS()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelector");
    }
}
