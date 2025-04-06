using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    UltimatePlayerController playerRef;
    Rigidbody rb;

    public Animator fadeScreenAnim;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GetComponent<UltimatePlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    public void RespawnPlayer(Vector3 checkPointPos, Quaternion checkPointRot)
    {
        StartCoroutine(RespawnPlayerCO(checkPointPos, checkPointRot));
    }

    IEnumerator RespawnPlayerCO(Vector3 spawnPos, Quaternion spawnRot)
    {
        fadeScreenAnim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        transform.position = spawnPos;
        transform.rotation = spawnRot;
        rb.velocity = Vector3.zero;
        fadeScreenAnim.SetTrigger("FadeIn");
    }
}
