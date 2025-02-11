using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSettings : MonoBehaviour
{
    private Slider slider;
    public Text sliderText;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();

        ChangeSlider();
    }

    public void ChangeSlider()
    {
        sliderText.text = (slider.value * 10).ToString();
    }
}
