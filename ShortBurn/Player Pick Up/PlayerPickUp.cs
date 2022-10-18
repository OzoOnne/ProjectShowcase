using Control_and_Input;
using Player_Controls;
using UnityEngine;
using UnityEngine.Events;

namespace Player_Pick_Up
{
    public class PlayerPickUp : MonoBehaviour
    {
        private PickUpItem _pickable;
        public GameObject playerHands;
        public Transform raycastPoint;
        public float raycastMaxDistance;

        public KeyInventory keyInventory;

        public UnityEvent onItemPickup;

        private InputManager _input;
        
        [Header("Sounds")]
        [SerializeField] private SoundManager soundManager;
        [SerializeField] private string flutePickUpSound;
        [SerializeField] private string keyPickUpSound;
        
        private void Start()
        {
            _input = InputManager.instance;
        }

        private void Update()
        {
            if (_input.PickupItem())
            {
                TryPickUp();
            }
        }
        private void TryPickUp()
        {
            if (_pickable)
            {
                DropPickedUpItem(_pickable);
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out hit,raycastMaxDistance))
                {
                    _pickable = hit.transform.GetComponent<PickUpItem>();
                    if (_pickable && _pickable.Flute)
                    {
                        soundManager.Play(flutePickUpSound);
                        _pickable.gameObject.SetActive(false);
                    }
                    else if (_pickable && !_pickable.Flute)
                    {
                        soundManager.Play(keyPickUpSound);
                        PickUpItem(_pickable);
                    }
                }
            }
        }

        private void DropPickedUpItem(PickUpItem item)
        {
            _pickable = null;
            item.transform.SetParent(null);
            
            if (item.GetComponent<KeyItemController>())
            {
                keyInventory.hasMainKey = false;
                keyInventory.hasBasementKey = false;
                keyInventory.hasGardenKey = false;
            }
            
            item.GetRigidbody.isKinematic = false;
            item.GetRigidbody.AddForce(item.transform.forward * 3,ForceMode.VelocityChange);
        }
        
        private void PickUpItem(PickUpItem item)
        {
            item.GetRigidbody.isKinematic = true;
            
            item.transform.parent = playerHands.transform;
            item.transform.localPosition = Vector3.zero;
            item.transform.localEulerAngles = Vector3.zero;
        }
    }
}
