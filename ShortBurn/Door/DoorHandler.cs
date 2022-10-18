using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    //public variables
    public Transform leftDoor;
    public Transform rightDoor;
    public float moveSpeed = 1;
    public float sizeOfDoorInZ = 1;
    public float amountOfDoorInWall = 0.9f;
    public bool _DoorsOpen = false;
    
    //private variables
    private Vector3 _leftDoorCloseTarget;
    private Vector3 _rightDoorCloseTarget;
    private Vector3 _leftDoorOpenTarget;
    private Vector3 _rightDoorOpenTarget;
    private float _startTime;
    private float _totalDistanceToCover;
    

    private void Start()
    {
        SetInitialReferences();
    }
    
    //will set all te required references for the door movement
    void SetInitialReferences()
    {
        Vector3 leftLocalPosition = leftDoor.localPosition;
        Vector3 rightLocalPosition = rightDoor.localPosition;
        
        _leftDoorCloseTarget = leftLocalPosition;
        _rightDoorCloseTarget = rightLocalPosition;

        _leftDoorOpenTarget = new Vector3(
            leftLocalPosition.x, leftLocalPosition.y, leftLocalPosition.z - (sizeOfDoorInZ * amountOfDoorInWall));
        _rightDoorOpenTarget = new Vector3(
            rightLocalPosition.x, rightLocalPosition.y, rightLocalPosition.z + (sizeOfDoorInZ * amountOfDoorInWall));

        _totalDistanceToCover = Vector3.Distance(_leftDoorCloseTarget, _leftDoorOpenTarget);
    }
    
    void OpenDoors()
    {
        if (_DoorsOpen)
        {
            return;
        }
        
        float distanceCovered = Time.time * moveSpeed;
        float fractionOfJourney = distanceCovered / _totalDistanceToCover;
        leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, _leftDoorOpenTarget, fractionOfJourney);
        rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, _rightDoorOpenTarget, fractionOfJourney);

        if (Mathf.Approximately(rightDoor.localPosition.z,_rightDoorOpenTarget.z))
        {
            _DoorsOpen = true;
        }
    }
    void CloseDoors()
    {
        if (!_DoorsOpen)
        {
            return;
        }

        float distanceCovered = Time.time * moveSpeed;
        float fractionOfJourney = distanceCovered / _totalDistanceToCover;
        leftDoor.localPosition = Vector3.Lerp(leftDoor.localPosition, _leftDoorCloseTarget, fractionOfJourney);
        rightDoor.localPosition = Vector3.Lerp(rightDoor.localPosition, _rightDoorCloseTarget, fractionOfJourney);

        if (Mathf.Approximately(rightDoor.localPosition.z,_rightDoorCloseTarget.z))
        {
            _DoorsOpen = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            OpenDoors();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            CloseDoors();
        }
    }
}
