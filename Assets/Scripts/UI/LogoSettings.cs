using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoSettings : MonoBehaviour
{
    float rotationSpeed = 7f;
    public RectTransform[] positions;
    public int positionIndex;
    public int startPosition;

    private PanelSettings panelSettingsScript;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = positionIndex;

        panelSettingsScript = GameObject.FindWithTag("PanelSettings").GetComponent<PanelSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (panelSettingsScript.settingsOn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RotateRight();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                RotateLeft();
            }
        }

        transform.position = Vector3.Lerp(transform.position, positions[positionIndex].position, rotationSpeed * Time.unscaledDeltaTime);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetPosition();
        }
    }

    public void RotateRight()
    {
        positionIndex--;
        if (positionIndex < 0)
        {
            positionIndex = positions.Length - 1;
        }
    }

    public void RotateLeft()
    {
        positionIndex++;
        if (positionIndex >= positions.Length)
        {
            positionIndex = 0;
        }
    }

    public void ResetPosition()
    {
        positionIndex = startPosition;
    }
}
