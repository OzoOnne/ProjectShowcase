using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;

public class KeyItemController : MonoBehaviour
{
    [SerializeField] private bool mainDoor = false;
    [SerializeField] private bool mainKey = false;
    [SerializeField] private bool basementDoor = false;
    [SerializeField] private bool basementKey = false;
    [SerializeField] private bool gardenDoor = false;
    [SerializeField] private bool gardenKey = false;
    
    [SerializeField] private KeyInventory keyInventory;

    public KeyDoorController mainDoorObject;
    public KeyDoorController basementDoorObject;
    public KeyDoorController basementDoorObject2;
    public KeyDoorController gardenDoorObject;

    public Collider basementDoorCollider;
    
    public GameObject mainKeyGameObject;
    public GameObject basementKeyGameObject;
    public GameObject gardenKeyGameObject;

    public PostProcessingManager postProcessingManager;

    public void ObjectInteraction()
    {
        if (mainDoor && keyInventory.hasMainKey) 
        {
            mainDoorObject.OpenDoor();
            mainKeyGameObject.SetActive(false);
            keyInventory.hasMainKey = false;
        }
        else if (mainKey)
        {
            keyInventory.hasMainKey = true;
        }
        else if (basementDoor && keyInventory.hasBasementKey)
        {
            basementDoorObject.OpenDoor();
            basementDoorObject2.OpenDoor();
            basementDoorCollider.enabled = false;
            basementKeyGameObject.SetActive(false);
            keyInventory.hasGardenKey = false;
        }
        else if (basementKey)
        {
            keyInventory.hasBasementKey = true;
        }
        else if (gardenDoor)
        {
            postProcessingManager.ChangeToNormal();
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("WinMenu");
            gardenKeyGameObject.SetActive(false);
        } 
        else if (gardenKey)
        {
            keyInventory.hasGardenKey = true;
        }
    }
}
