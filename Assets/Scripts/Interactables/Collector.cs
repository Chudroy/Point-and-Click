using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Pickup))]
public class Collector : Interactable
{
    Pickup pickup;
    [SerializeField] bool destroyOnCollect;
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

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (this.enabled == false) return;
        if (countDown >= 0) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;

        pickup.PickupItem(destroyOnCollect);

    }
}
