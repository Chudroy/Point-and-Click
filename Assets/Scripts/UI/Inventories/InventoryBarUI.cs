using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Inventories;
using GameDevTV.UI.Inventories;
public class InventoryBarUI : MonoBehaviour
{
    // CONFIG DATA
    InventorySlotUI[] inventorySlotUIs;
    InventoryItem[] itemsOnDisplay;

    // CACHE
    Inventory playerInventory;
    int rowNumber = 0;

    // LIFECYCLE METHODS

    private void Awake()
    {
        playerInventory = Inventory.GetPlayerInventory();

        inventorySlotUIs = GetComponentsInChildren<InventorySlotUI>();
    }

    private void OnEnable()
    {
        playerInventory.inventoryUpdated += Redraw;
    }

    private void OnDisable()
    {
        playerInventory.inventoryUpdated -= Redraw;
    }

    private void Start()
    {
        // itemsOnDisplay = playerInventory.GetItemsOnDisplay(inventorySlotUIs.Length, rowNumber);
        Redraw();
    }

    // PRIVATE

    void Redraw()
    {
        int rowStartIdx = inventorySlotUIs.Length * rowNumber;
        int slotIdx = 0;
        for (int i = rowStartIdx; i < rowStartIdx + inventorySlotUIs.Length; i++)
        {
            inventorySlotUIs[slotIdx].Setup(playerInventory, i);
            slotIdx++;
        }
    }

    // public

    public void ScrollDown()
    {
        Debug.Log("scroll down");
        if ((inventorySlotUIs.Length * (rowNumber + 1)) + inventorySlotUIs.Length > playerInventory._inventorySize) return;
        if (playerInventory.GetItemInSlot(inventorySlotUIs.Length * (rowNumber + 1)) == null) return;
        rowNumber++;
        Redraw();
    }

    public void ScrollUp()
    {
        Debug.Log("scroll up");
        rowNumber--;
        if (rowNumber <= 0) rowNumber = 0;
        Redraw();
    }
}
