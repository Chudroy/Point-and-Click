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
        var cassetteItem = tool as CassetteItem;
        if (cassetteItem == null) Debug.LogWarning("accepted item isn't a cassette");
        if (currentCassetteModel != null) return;

        currentCassetteModel = Instantiate(cassetteItem._cassetteModel, Vector3.zero, Quaternion.identity);
        currentCassetteModel.transform.SetParent(this.transform, false);
        currentCassetteModel.GetComponent<Collider>().enabled = true;

        cassetteItem.OnResolve();
    }

    public override bool HandleRaycast(PlayerController callingController)
    {
        //pick up cassette in slot if the click goes to the cassette slot and not to the cassette interactable model itself.

        if (this.enabled == false) return false;
        if (!Input.GetMouseButtonDown(0)) return false;
        if (currentCassetteModel != null) return false;

        currentCassetteModel?.GetComponent<Interactable>().HandleRaycast(callingController);
        return true;
    }
}
