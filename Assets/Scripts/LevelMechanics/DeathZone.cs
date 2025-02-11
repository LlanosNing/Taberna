using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    GameOver gameOverRef;

    private void Start()
    {
        gameOverRef = GameObject.FindWithTag("Canvas").GetComponent<GameOver>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);

            gameOverRef.GameOverAnimation();
        }
    }
}
