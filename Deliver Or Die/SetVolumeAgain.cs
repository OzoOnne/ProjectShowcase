using UnityEngine;
using UnityEngine.Audio;

public class SetVolumeAgain : MonoBehaviour
{
    // will set volume again of the audio when going to the new scene
    public AudioMixer mixer;
    void Start()
    {
        Debug.Log("Volume is working " + SetVolume.value);
        mixer.SetFloat("MusicVol", SetVolume.value);
    }
}
