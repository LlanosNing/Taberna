using FirstGearGames.SmoothCameraShaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FirstGearGames.SmoothCameraShaker;

public class ClickFastMinigame : MonoBehaviour
{
    public float minigameDuration;
    public float timeCounter;

    public float clicks;
    public float timePassed;
    public float cps;
    public float cpsThreshold;
    bool minigameActivated;

    GameObject mainCamera;
    public GameObject undergroundCamera;
    Animator fadeScreen;
    public GameObject uiIndicator;
    public Image indicatorImage;
    public Image progressBar;

    public FollowPlayer followPlayerScript;
    public CameraArmController armControllerScript;
    public ZoomCamera zoomScript;

    UltimatePlayerController playerController;
    Rigidbody playerRB;

    public World3Manager world3Manager;

    public ShakeData cameraShakeData;
    public float cameraShakeTime;
    float cameraShakeCounter;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = minigameDuration;

        mainCamera = GameObject.FindWithTag("MainCamera");
        fadeScreen = GameObject.FindWithTag("FadeScreen").GetComponent<Animator>();

        playerController = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
        playerRB = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
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

        if(cameraShakeCounter > 0)
        {
            cameraShakeCounter -= Time.deltaTime;
        }
    }

    void CalculateCPS()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            clicks++;
            indicatorImage.color = new Color(1f, 1f, 0.4f, 1f);

            if(cameraShakeCounter <= 0)
            {
                Debug.Log("Agitar Cámara");
                cameraShakeCounter = cameraShakeTime;
                CameraShakerHandler.Shake(cameraShakeData);
            }
                
        }

        timePassed += Time.deltaTime;
        cps = clicks / timePassed;

        if(cps > cpsThreshold)
        {
            timeCounter -= Time.deltaTime;
            progressBar.fillAmount = (minigameDuration - timeCounter) / minigameDuration;
        }

        if(timePassed > 2f)
        {
            clicks = 0;
            timePassed = 0;
            indicatorImage.color = new Color(1f, 1f, 1f, 1f);
        } 
    }

    public void StartMinigame(Transform checkpoint)
    {
        StartCoroutine(StartMinigameCO(checkpoint));
    }

    IEnumerator StartMinigameCO(Transform checkpoint)
    {
        fadeScreen.SetTrigger("FadeOut");

        yield return new WaitForSeconds(1f);

        mainCamera.SetActive(false);
        undergroundCamera.SetActive(true);

        followPlayerScript.staticCamera = true;
        armControllerScript.staticCamera = true;
        zoomScript.staticCamera = true;

        playerController.gameObject.transform.position = checkpoint.position;
        playerController.gameObject.transform.rotation = checkpoint.rotation;
        playerController.canMove = false;
        playerController.canJump = false;
        fadeScreen.SetTrigger("FadeIn");
        playerRB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        progressBar.fillAmount = 0;


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
        playerRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;


        mainCamera.SetActive(true);
        undergroundCamera.SetActive(false);

        fadeScreen.SetTrigger("FadeOut");

        world3Manager.AddRoot();
        minigameActivated = false;
        uiIndicator.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ResetData(Transform checkpoint)
    {
        timeCounter = minigameDuration;
        clicks = 0;
        timePassed = 0;
        cps = 0;

        timeCounter = minigameDuration;
        mainCamera = GameObject.FindWithTag("MainCamera");
        fadeScreen = GameObject.FindWithTag("FadeScreen").GetComponent<Animator>();
        playerController = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();

        StartMinigame(checkpoint);
    }
}
