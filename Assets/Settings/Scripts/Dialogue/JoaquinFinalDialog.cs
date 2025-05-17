using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoaquinFinalDialog : MonoBehaviour
{
    public string[] finalDialogue;
    public DialogActivator dialogActivator;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.endgame)
        {
            dialogActivator.firstLines = finalDialogue;
        }
    }
}
