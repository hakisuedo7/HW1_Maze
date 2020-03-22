using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectVolume : MonoBehaviour
{
    AudioSource effect;
    // Start is called before the first frame update
    void Start()
    {
        effect = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        effect.volume = OptionData.EFFECTVOLUME;
    }
}
