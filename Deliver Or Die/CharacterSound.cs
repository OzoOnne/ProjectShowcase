using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    // audio needed for the character
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip[] clips;


    void Start()
    {
        StartCoroutine(PlayAudio());
    }


   // The ieunemator to play the audio
   private IEnumerator PlayAudio()
   {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4, 10));
            source.clip = clips[Random.Range(0, clips.Length)];
            source.Play();
            Debug.Log("ping");
        }
   }
}
