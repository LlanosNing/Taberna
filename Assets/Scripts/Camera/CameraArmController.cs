using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraArmController : MonoBehaviour
{
    public float verticalClamp = 30f;
    public Vector2 sensitivity = Vector2.one;


    private void FixedUpdate()
    {
        AdjustCamera();
    }

    void AdjustCamera()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        input *= sensitivity;
        transform.localRotation = Quaternion.Euler(new Vector3(input.y, input.x * -1f, 0) + transform.localRotation.eulerAngles);

        float clamped_x = 0;

        if (transform.localRotation.eulerAngles.x < 180)
            clamped_x = Mathf.Clamp(transform.localRotation.eulerAngles.x, -verticalClamp, verticalClamp);
        else
            clamped_x = Mathf.Clamp(transform.localRotation.eulerAngles.x, 360f - verticalClamp, 360f + verticalClamp);

        transform.localRotation = Quaternion.Euler(
            new Vector3(
                clamped_x,
                transform.localRotation.eulerAngles.y,
                0));
    }
}
