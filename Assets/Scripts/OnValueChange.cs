using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnValueChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Voloume()
    {
        GameObject slider = GameObject.Find("Slider_Volume");
        OptionData.VOLUME = slider.GetComponent<Slider>().value;
    }
    public void MSensitivityX()
    {
        GameObject slider = GameObject.Find("Slider_MSensitivityX");
        OptionData.M_SensitivityX = slider.GetComponent<Slider>().value;
    }
    public void MSensitivityY()
    {
        GameObject slider = GameObject.Find("Slider_MSensitivityY");
        OptionData.M_SensitivityY = slider.GetComponent<Slider>().value;
    }
}
