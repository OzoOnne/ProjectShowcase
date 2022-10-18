using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AnimationCurve openSpeedCurve = new AnimationCurve(new Keyframe[] { new Keyframe(0, 1, 0, 0), new Keyframe(0.8f, 1, 0, 0), new Keyframe(1, 0, 0, 0) });
    
    public float openDistance = 0.58f; //How far should door slide (change direction by entering either a positive or a negative value)
    public float openSpeedMultiplier = 1.0f; //Increasing this value will make the door open faster
    public Transform doorLeft; //Door body Transform
    public Transform doorRight;
    private bool open = false;

    private Vector3 defaultLeftDoorPosition;
    private Vector3 currentLeftDoorPosition;
    private Vector3 defaultRightDoorPosition;
    private Vector3 currentRightDoorPosition;
    private float openTime = 0;
    
    [Header("Sound")]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private string soundName;

    private void Start()
    {
        if (doorLeft && doorRight)
        {
            defaultLeftDoorPosition = doorLeft.localPosition;
            defaultRightDoorPosition = doorRight.localPosition;
        }
    }

    // Main function
    private void Update()
    {
        if (!doorLeft&&!doorRight)
            return;

        if (openTime < 1)
        {
            openTime += Time.deltaTime * openSpeedMultiplier * openSpeedCurve.Evaluate(openTime);
        }
        doorLeft.localPosition = new Vector3(doorLeft.localPosition.x, doorLeft.localPosition.y, Mathf.Lerp(currentLeftDoorPosition.z, defaultLeftDoorPosition.z + (open ? openDistance : 0), openTime));
        doorRight.localPosition = new Vector3(doorRight.localPosition.x, doorRight.localPosition.y, Mathf.Lerp(currentRightDoorPosition.z, defaultRightDoorPosition.z - (open ? openDistance : 0), openTime));

        
    }

    // Activate the Main function when Player enter the trigger area
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            soundManager.Play(soundName);
            open = true;
            currentLeftDoorPosition = doorLeft.localPosition;
            currentRightDoorPosition = doorRight.localPosition;
            openTime = 0;
        }
    }

    // Deactivate the Main function when Player exit the trigger area
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            open = false;
            currentLeftDoorPosition = doorLeft.localPosition;
            currentRightDoorPosition = doorRight.localPosition;
            openTime = 0;
        }
    }
}


