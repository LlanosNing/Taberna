using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject uiIndicator;
    public Image indicatorImage;

    public FollowPlayer followPlayerScript;
    public CameraArmController armControllerScript;
    public ZoomCamera zoomScript;

    Vector3 initialCameraPosition;
    Quaternion initialCameraRotation;
    public Transform cameraWaypoint;
    UltimatePlayerController playerController;

    public World3Manager world3Manager;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = minigameDuration;

        cameraObject = GameObject.FindWithTag("MainCamera");
        fadeScreen = GameObject.FindWithTag("FadeScreen").GetComponent<Animator>();

        playerController = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
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
        {
            clicks++;
            indicatorImage.color = new Color(1f, 1f, 0.4f, 1f);
        }

        timePassed += Time.deltaTime;
        cps = clicks / timePassed;

        if(cps > cpsThreshold )
            timeCounter -= Time.deltaTime;

        if(timePassed > 2f)
        {
            clicks = 0;
            timePassed = 0;
            indicatorImage.color = new Color(1f, 1f, 1f, 1f);
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

        uiIndicator.SetActive(true);

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

        world3Manager.AddRoot();
        minigameActivated = false;
        uiIndicator.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ResetData(Transform camWaypoint)
    {
        timeCounter = minigameDuration;
        clicks = 0;
        timePassed = 0;
        cps = 0;

        timeCounter = minigameDuration;
        cameraObject = GameObject.FindWithTag("MainCamera");
        fadeScreen = GameObject.FindWithTag("FadeScreen").GetComponent<Animator>();
        playerController = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();

        cameraWaypoint = camWaypoint;

        StartMinigame();
    }
}
