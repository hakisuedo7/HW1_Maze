using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCurrentOptionData : MonoBehaviour
{
    public enum Type{
        volume,
        mxs,
        mxy
    }
    public Type type;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case Type.volume:
                slider.value = OptionData.VOLUME;
                break;
            case Type.mxs:
                slider.value = OptionData.M_SensitivityX;
                break;
            case Type.mxy:
                slider.value = OptionData.M_SensitivityY;
                break;
            default:
                break;
        }
    }
}
