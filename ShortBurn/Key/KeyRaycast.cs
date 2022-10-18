using System;
using System.Collections;
using System.Collections.Generic;
using Control_and_Input;
using UnityEngine;

namespace Player_Controls
{
    public class KeyRaycast : MonoBehaviour
    {
        [SerializeField] private int _rayLenght = 5;
        [SerializeField] private LayerMask _layerMaskInteract;
        [SerializeField] private string _excluseLayerName = null;

        private KeyItemController raycastedObject;
        private InputManager _input;

        private bool _IsActive;
        private bool _DoOnce;

        private string _InteractableTag = "InteractiveObject";
        
        private void Start()
        {
            _input = InputManager.instance;
        }

        private void Update()
        {
            RaycastHit hit;
            Vector3 fwd = transform.TransformDirection(Vector3.forward);

            int mask = 1 << LayerMask.NameToLayer(_excluseLayerName) | _layerMaskInteract.value;

            if (Physics.Raycast(transform.position, fwd, out hit, _rayLenght, mask))
            {
                if (hit.collider.CompareTag(_InteractableTag))
                {
                    if (!_DoOnce)
                    {
                        raycastedObject = hit.collider.gameObject.GetComponent<KeyItemController>();
                    }

                    _IsActive = true;
                    _DoOnce = true;

                    if (_input.PickupItem())
                    {
                        raycastedObject.ObjectInteraction();
                    }
                }
            }
            else ;
            {
                if (_IsActive)
                {
                    _DoOnce = false;
                }
            }
        }
    }
}