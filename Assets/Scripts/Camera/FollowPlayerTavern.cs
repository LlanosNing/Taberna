using System.Collections;
using System.Collections.Generic;
using Autodesk.Fbx;
using TMPro;
using UnityEngine;

public class FollowPlayerTavern : MonoBehaviour
{
    [Range(.1f, 1f)]
    public float followDamping;
    public Transform playerTransform;

    public Transform limitLeft, limitRight, limitFront, limitBack;
    private void Update()
    {
        Vector3 targetPosition = Vector3.Lerp(transform.position, playerTransform.position, 1 / followDamping * Time.fixedDeltaTime);

        // Restringir la posici�n dentro de los l�mites
        Vector2 xLimits = new Vector2(limitBack.position.x, limitFront.position.x); // M�nimo y m�ximo en X
        Vector2 zLimits = new Vector2(limitRight.position.z, limitLeft.position.z); // M�nimo y m�ximo en Z

        float clampedX = Mathf.Clamp(targetPosition.x, xLimits.x, xLimits.y);
        float clampedZ = Mathf.Clamp(targetPosition.z, zLimits.x, zLimits.y);

        // Aplicar la posici�n con los l�mites
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
