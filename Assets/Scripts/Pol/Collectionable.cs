using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectionable : MonoBehaviour
{
    public GameObject coin;
    public Animator anim;
    public AudioSource audioRef;
    public Collider cll;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("CatchCoin");
            audioRef.Play();
            
            StartCoroutine(delayDestroy());
        }
    }

    public IEnumerator delayDestroy()
    {
        cll.enabled = false;
        yield return new WaitForSeconds(0.7f);
        Destroy(coin);
    }
}
