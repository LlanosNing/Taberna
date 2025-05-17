using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSound : MonoBehaviour
{
    public void PickupPlaySFX()
    {
        AudioManager.aMRef.PlaySFX(5);
    }
}
