using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public static float value;

    // Will set the level of the volume
    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        value = Mathf.Log10(sliderValue) * 20;
    }

    public void Update()
    {
        Debug.Log(value);
    }
}
