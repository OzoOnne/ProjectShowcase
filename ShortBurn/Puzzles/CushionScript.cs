using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace puzzle
{
    public class CushionScript : MonoBehaviour
    {
        [Header("cushion number")] public int cushionNumber;

        [SerializeField] private PuzzleManager _manager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<CharacterController>())
            {
                _manager.CushionActivation(cushionNumber);
            }
        }
    }
}