using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LoadFiles : MonoBehaviour
{
    public Transform tablePivot;
    public Transform rotationTarget;
    public Button file1Button, file2Button, file3Button;
    public bool file1, file2, file3;
    public int lastFile;

    public float rotTime = 3f;

    private void Update()
    {
        if(rotationTarget != null)
        {
            tablePivot.rotation = Quaternion.Lerp(tablePivot.rotation, rotationTarget.rotation, rotTime * Time.deltaTime);
        }
    }

    public void SetNewRotation(Transform _newRot)
    {
        rotationTarget = _newRot;
    }
}
