using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using GameDevTV.Inventories;
using GameDevTV.Core.UI.Dragging;
using System;
using InventoryExample.Control;

namespace GameDevTV.UI.Inventories
{
    public class InventorySlotUI : MonoBehaviour, IItemHolder, IPointerClickHandler
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;

        // STATE
        int index;
        InventoryItem item;
        Inventory inventory;
        ExamineTextPoster examineTextPoster;

        public void Setup(Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
            this.item = GetItem();
            icon.SetItem(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
        }

        public int MaxAcceptable(InventoryItem item)
        {
            if (inventory.HasSpaceFor(item))
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void AddItems(InventoryItem item, int number)
        {
            inventory.AddItemToSlot(index, item, number);
        }

        public InventoryItem GetItem()
        {
            return inventory.GetItemInSlot(index);
        }

        public int GetNumber()
        {
            return inventory.GetNumberInSlot(index);
        }

        public void RemoveItems(int number)
        {
            inventory.RemoveFromSlot(index, number);
        }

        public CursorType GetCursorType()
        {
            return CursorType.None;
        }

        // public bool HandleRaycast(PlayerController callingController)
        // {
        //     IUsable usableItem = item as IUsable;

        //     if (Input.GetMouseButtonDown(0) && usableItem != null)
        //     {
        //         usableItem.Use(callingController);
        //         return true;
        //     }

        //     return false;
        // }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            IUsable usableItem = item as IUsable;
            PlayerController playerController = PlayerController.GetPlayerController();
            usableItem.Use(playerController);
        }
    }
}