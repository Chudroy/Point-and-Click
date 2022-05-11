using UnityEngine;
using GameDevTV.Inventories;
using InventoryExample.Control;
using System;
using System.Collections;

namespace InventoryExample.Control
{
    [RequireComponent(typeof(Pickup))]
    public class ClickablePickup : MonoBehaviour, IRaycastable
    {
        Pickup pickup;
        float countDown = 0.5f;

        void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        void Update()
        {
            countDown -= Time.deltaTime;
        }

        public CursorType GetCursorType()
        {
            if (pickup.CanBePickedUp())
            {
                return CursorType.Pickup;
            }
            else
            {
                return CursorType.FullPickup;
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0) && countDown <= 0)
            {
                Debug.Log("pickup");
                pickup.PickupItem();
            }
            return true;
        }
    }
}