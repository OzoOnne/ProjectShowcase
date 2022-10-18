using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public GameObject popUpText;
    public GameObject objectAttachedTo;

    public KeyInventory _key;
    public bool door;
    private void OnTriggerEnter(Collider other)
    {
        if (door)
        {
            if (other.GetComponent<CharacterController>() && _key.hasMainKey) 
            {
                popUpText.SetActive(true);
            }
        }
        if(!door)
        {
            if (other.GetComponent<CharacterController>()) 
            {
                popUpText.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>())
        {
            popUpText.SetActive(false);
        }
    }

    private void Update()
    {
        if (objectAttachedTo.transform.parent != null)
        {
            popUpText.SetActive(false);
        }
    }

    private void OnDisable()
    {
        popUpText.SetActive(false);
    }
}
