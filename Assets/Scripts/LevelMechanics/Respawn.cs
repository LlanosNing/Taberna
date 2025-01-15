using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    UltimatePlayerController playerRef;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindWithTag("Player").GetComponent<UltimatePlayerController>();
    }

    public void RespawnPlayer()
    {
        playerRef.gameObject.SetActive(true);

        playerRef.Respawn();
    }
}
