using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTrigger : MonoBehaviour
{
    public string sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadScene(0.5f);
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
