using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantSandstorm : MonoBehaviour
{
    bool alreadyEnabled;
    WindPush windPushScript;

    // Start is called before the first frame update
    void Start()
    {
        windPushScript = GameObject.FindWithTag("Player").GetComponent<WindPush>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!alreadyEnabled)
        {
            InstantWindPush();
        }
    }

    public void InstantWindPush()
    {
        StartCoroutine(InstantWindPushCO());
    }
    IEnumerator InstantWindPushCO()
    {
        alreadyEnabled = true;
        windPushScript.windEnabled = true;
        windPushScript.windIntervalCounter = 3f;
        yield return new WaitForSeconds(7f);
        windPushScript.windEnabled = false;
        gameObject.SetActive(false);
    }
}
