using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using InventoryExample.Control;
using UnityEngine;

[RequireComponent(typeof(Pickup))]
public class Collector : Interactable
{
    Pickup pickup;
    [SerializeField] bool DestroyOnCollect;
    float countDown = 0.5f;

    void Awake()
    {
        pickup = GetComponent<Pickup>();
    }

    public override void Start()
    {
        contextMenuName = "Pick Up";
        base.Start();
    }

    void Update()
    {
        countDown = Mathf.Clamp(countDown, 0, 1f);
        countDown -= Time.deltaTime;
    }

    public override CursorType GetCursorType()
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

    public override bool HandleRaycast(PlayerController callingController)
    {
        if (this.enabled == false) return false;
        if (countDown >= 0) return false;
        if (!Input.GetMouseButtonDown(0)) return false;
        pickup.PickupItem(DestroyOnCollect);
        return true;
    }
}
