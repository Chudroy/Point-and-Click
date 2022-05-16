using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MannequinPart", menuName = "ScriptableObjects/Tools/MannequinPart")]
public class MannequinItem : Tool
{
    [SerializeField] MannequinItemType mannequinPartType;
    public MannequinItemType _mannequinPartType => mannequinPartType;

    public override void OnResolve()
    {
        Inventory.GetPlayerInventory().RemoveItem(this as InventoryItem, 1);
    }
}

public enum MannequinItemType { Head, LeftArm, RightArm, LeftLeg, RightLeg, Torso }
