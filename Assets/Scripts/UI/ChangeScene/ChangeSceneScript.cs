using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneScript : MonoBehaviour
{
    public string sceneToLoad;
    public float timeToChange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeScene();
        }
    }
    public void ChangeScene()
    {
        StartCoroutine("ChangeSceneCO");
    }
    private IEnumerator ChangeSceneCO()
    {
        yield return new WaitForSeconds(timeToChange);

        if (sceneToLoad != "")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.Log("Falta rellenar el nombre de la escena!");
        }
    }
}
