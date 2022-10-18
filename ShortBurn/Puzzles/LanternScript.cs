using System.Collections;
using System.Collections.Generic;
using Control_and_Input;
using Player_Controls;
using puzzle;
using UnityEngine;
using UnityEngine.InputSystem;


public class LanternScript : MonoBehaviour
{
    [SerializeField] private PuzzleManager _manager;
    [SerializeField] private InputManager _input;

    [SerializeField] private int _lanternNumber;
    
    [SerializeField] private Material _material;

    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterController>())
        {
            if (_input.PickupItem())
            {
                _material.SetColor("_EmissionColor",Color.black);
                _manager.LaternActivation(_lanternNumber);
            }
        }
    }
}
