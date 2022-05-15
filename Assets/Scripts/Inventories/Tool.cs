using System;
using InventoryExample.Control;
using UnityEngine;

namespace GameDevTV.Inventories
{
    /// <summary>
    /// An inventory item that can be equipped to the player. Weapons could be a
    /// subclass of this.
    /// </summary>
    [CreateAssetMenu(fileName = "Tool", menuName = "ScriptableObjects/Tools/DefaultTool")]
    public class Tool : InventoryItem, IUsable, IExaminable
    {
        public override void Examine(ExamineTextPoster examineTextPoster)
        {
            examineTextPoster.SetExamineText(GetDescription());
        }

        public override void Use(PlayerController playerController)
        {
            Debug.Log("USING TOOL");
            playerController.currentTool = this;
        }

        public virtual void OnResolve()
        {
            //On tool resolving an obstacle
            Debug.Log("On tool resolving an obstacle");
            Inventory.GetPlayerInventory().RemoveItem(this as InventoryItem, 1);
        }
    }
}