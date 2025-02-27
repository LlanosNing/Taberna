using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{
    public string sceneToLoad;
    public float timeToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene(timeToLoad);
        }
    }

    public void LoadScene(float timeToLoad)
    {
        StartCoroutine(LoadSceneCO(timeToLoad));
    }

    IEnumerator LoadSceneCO(float timeToLoad)
    {
        yield return new WaitForSeconds(timeToLoad);

        if(sceneToLoad != "")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
