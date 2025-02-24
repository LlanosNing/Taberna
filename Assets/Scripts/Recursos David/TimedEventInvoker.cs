using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedEventInvoker : MonoBehaviour
{
    [Serializable]
    public class TimedEvent
    {
        public string eventName;
        public UnityEvent action;
        public float delay;
    }

    [Serializable]
    public class TimedEventCollection
    {
        public TimedEvent[] events;
    }

    public TimedEventCollection eventCollection = new TimedEventCollection();

    private void OnEnable()
    {
        foreach (var timedEvent in eventCollection.events)
        {
            StartCoroutine(InvokeEventAfterDelay(timedEvent));
        }
    }

    private IEnumerator InvokeEventAfterDelay(TimedEvent timedEvent)
    {
        yield return new WaitForSeconds(timedEvent.delay);
        Debug.Log($"Invoking event: {timedEvent.eventName}");
        timedEvent.action.Invoke();
    }
}