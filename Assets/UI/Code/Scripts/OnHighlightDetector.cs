using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHighlightDetector : MonoBehaviour
{
    public CameraTraveling _spotLightScript;

    public void OnHighlight(Transform newSLWaypoint)
    {
        _spotLightScript.ChangeTarget(newSLWaypoint);
    }
}
