using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFastMinigame : MonoBehaviour
{
    public float minigameDuration;
    public float timeCounter;

    public float clicks;
    public float timePassed;
    public float cps;
    public float cpsThreshold;
    bool minigameActivated;

    GameObject cameraObject;
    Animator fadeScreen;

    public FollowPlayer followPlayerScript;
    public CameraArmController armControllerScript;
    public ZoomCamera zoomScript;

    Vector3 initialCameraPosition;
    Quaternion initialCameraRotation;
    public Transform cameraWaypoint;
    UltimatePlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = minigameDuration;

        cameraObject = GameObject.FindWithTag("MainCamera");
        fadeScreen = GameObject.FindWithTag("FadeScreen").GetComponent<Animator>();

        playerController = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();

        StartMinigame();
    }

    // Update is called once per frame
    void Update()
    {
        if (minigameActivated)
        {
            CalculateCPS();
        }

        if (timeCounter <= 0)
            EndMinigame();
    }

    void CalculateCPS()
    {
        if (Input.GetButtonDown("Fire1"))
            clicks++;
        timePassed += Time.deltaTime;
        cps = clicks / timePassed;

        if(cps > cpsThreshold )
            timeCounter -= Time.deltaTime;

        if(timePassed > 2f)
        {
            clicks = 0;
            timePassed = 0;
        } 
    }

    void StartMinigame()
    {
        StartCoroutine(StartMinigameCO());
    }

    IEnumerator StartMinigameCO()
    {
        fadeScreen.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        initialCameraPosition = cameraObject.transform.position;
        initialCameraRotation = cameraObject.transform.rotation;

        followPlayerScript.staticCamera = true;
        armControllerScript.staticCamera = true;
        zoomScript.staticCamera = true;

        playerController.canMove = false;
        playerController.canJump = false;

        cameraObject.transform.position = cameraWaypoint.position;
        cameraObject.transform.rotation = cameraWaypoint.rotation;

        fadeScreen.SetTrigger("FadeIn");

        yield return new WaitForSeconds(0.5f);

        minigameActivated = true;

    }

    void EndMinigame()
    {
        StartCoroutine (EndMinigameCO());
    }

    IEnumerator EndMinigameCO()
    {
        fadeScreen.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        followPlayerScript.staticCamera = false;
        armControllerScript.staticCamera = false;
        zoomScript.staticCamera = false;

        playerController.canMove = true;
        playerController.canJump = true;

        cameraObject.transform.position = initialCameraPosition;
        cameraObject.transform.rotation = initialCameraRotation;

        fadeScreen.SetTrigger("FadeOut");

        minigameActivated = false;
        gameObject.SetActive(false);
    }


}
