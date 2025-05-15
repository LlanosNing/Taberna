using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GiantCarrot : MonoBehaviour
{
    public int maxSink;
    int sinkLevels;
    public float sinkDistance;

    public GameObject minigameManager;
    bool alreadyActivated;

    public Transform cameraWaypoint;

    UltimatePlayerController playerController;
    Rigidbody rb;

    public Transform checkpoint;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if(sinkLevels >= maxSink && !alreadyActivated)
        {
            ActivateMinigame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && sinkLevels < maxSink)
        {
            if(playerController.canGroundPoundCarrot)
            {
                sinkLevels++;

                transform.localPosition -= new Vector3(0, sinkDistance, 0);

                AudioManager.aMRef.PlaySFX(3);
            }
        }
    }
    void ActivateMinigame()
    {
        StartCoroutine(ActivateMinigameCO());
    }
    IEnumerator ActivateMinigameCO()
    {
        yield return new WaitForSeconds(0.15f);
        alreadyActivated = true;
        minigameManager.SetActive(true);
        minigameManager.GetComponent<ClickFastMinigame>().ResetData(checkpoint);
    }
}
