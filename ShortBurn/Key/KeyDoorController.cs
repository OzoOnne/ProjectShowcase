using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoorController : MonoBehaviour
{
    [SerializeField] private float timeToTurn = 2;
    [SerializeField] private Vector3 rotate;

    [SerializeField] private SoundManager soundManager;
    [SerializeField] private string doorOpenSoundName;
    public void OpenDoor()
    {
        soundManager.Play(doorOpenSoundName);
        StartCoroutine(RotateMe(rotate, timeToTurn));
    }

    IEnumerator RotateMe(Vector3 byAngles, float time) 
    {    
        Quaternion fromAngle = transform.rotation;
        Quaternion toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        
        for(var t = 0f; t < 1; t += Time.deltaTime/time) {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }
}
