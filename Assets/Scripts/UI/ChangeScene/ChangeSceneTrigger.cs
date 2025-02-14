using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    ChangeSceneScript changeSceneRef;
    // Start is called before the first frame update
    void Start()
    {
        changeSceneRef = GameObject.FindWithTag("GameManager").GetComponent<ChangeSceneScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeSceneRef.ChangeScene();
        }
    }
}
