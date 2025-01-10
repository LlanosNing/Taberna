using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DayManager : MonoBehaviour
{
    public static int currentDay = 0;
    public UnityEvent OnNextDay;

    public static DayManager Instance;

    void Awake()
    {
        Instance = this;        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            NextDay();
        }
    }

    public void NextDay()
    {
        Debug.Log("newday" + currentDay);
        currentDay++;
        OnNextDay.Invoke();
    }
}
