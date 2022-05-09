using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "MannequinPart", menuName = "ScriptableObjects/MannequinPart")]
public class MannequinItem : Tool
{
    [SerializeField] MannequinItemType mannequinPartType;
    public MannequinItemType _mannequinPartType => mannequinPartType;
    List<UnityAction> actions = new List<UnityAction>();

    public override void Examine(PlayerController player)
    {
        Debug.Log("examining mannequin item");
    }
}

public enum MannequinItemType { Head, LeftArm, RightArm, LeftLeg, RightLeg, Torso }
