using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMVolume : MonoBehaviour
{
    AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        bgm = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        bgm.volume = OptionData.VOLUME;
    }
}
