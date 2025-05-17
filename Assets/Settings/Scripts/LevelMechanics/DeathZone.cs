using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Transform spawnpoint;

    Respawn respawnRef;

    private void Start()
    {
        respawnRef = GameObject.FindWithTag("Player").GetComponent<Respawn>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.aMRef.PlaySFX(13);
            respawnRef.RespawnPlayer(spawnpoint.position, spawnpoint.rotation);
        }
    }
}
