using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using InventoryExample.Control;
using UnityEngine;

public class CassetteSlot : Obstacle
{
    //Obstacle should handle removing tool from inventory, because it needs to check if the tool was placed in world space, for example
    GameObject currentCassetteModel;
    Inventory playerInventory;

    private void Awake()
    {
        playerInventory = Inventory.GetPlayerInventory();
    }

    public override void Resolve(Tool tool)
    {
        var cassetteTool = tool as CassetteItem;
        if (cassetteTool == null) Debug.LogWarning("accepted item isn't a cassette");
        if (currentCassetteModel != null) return;

        currentCassetteModel = Instantiate(cassetteTool._cassetteModel, Vector3.zero, Quaternion.identity);
        currentCassetteModel.transform.SetParent(this.transform, false);
        currentCassetteModel.GetComponent<Collider>().enabled = true;

        playerInventory.RemoveItem(tool as InventoryItem, 1);
    }

    public override bool HandleRaycast(PlayerController callingController)
    {
        if (this.enabled == false) return false;
        if (!Input.GetMouseButtonDown(0)) return false;
        if (currentCassetteModel != null) return false;

        Debug.Log("handling raycast cassette");
        currentCassetteModel.GetComponent<Interactable>().HandleRaycast(callingController);
        return true;
    }
}
