using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerChange : MonoBehaviour
{
    public Image image;
    public Sprite keyboardSprite, gamePadSprite;
    public GameObject keyboardMapping, gamepadMapping;
    public bool isGamepad;
    // Start is called before the first frame update

    
    public void ChangeSprite()
    {
        if (!isGamepad)
        {
            image.sprite = gamePadSprite;
            keyboardMapping.SetActive(false);
            gamepadMapping.SetActive(true);
            isGamepad = true;
        }
        else if (isGamepad)
        {
            image.sprite = keyboardSprite;
            keyboardMapping.SetActive(true);
            gamepadMapping.SetActive(false);
            isGamepad = false;
        }
    }
}
