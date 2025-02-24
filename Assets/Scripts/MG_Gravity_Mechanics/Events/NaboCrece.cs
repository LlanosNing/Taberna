using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaboCrece : MonoBehaviour
{
    public GameObject state1;
    public GameObject state2;
    public GameObject state3;

    public int growLevel = 0;

    void Start()
    {
        DayManager.Instance.OnNextDay.AddListener(Grow);
    }

    void Grow()
    {
        if(growLevel >= 3)
        {
            return;
        }

        growLevel++;
        if(growLevel == 1)
        {
            state1.SetActive(true);
        }
        if(growLevel == 2)
        {
            state1.SetActive(false);
            state2.SetActive(true);
        }
        if(growLevel == 3)
        {
            state2.SetActive(false);
            state3.SetActive(true);
        }
    }

    public void Reiniciar()
    {
        growLevel = 0;
    }

    private void OnDestroy()
    {
        
    }
}
