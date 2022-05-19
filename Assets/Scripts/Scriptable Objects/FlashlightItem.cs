using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

[CreateAssetMenu(fileName = "Flashlight", menuName = "ScriptableObjects/Flashlight")]
public class FlashlightItem : InventoryItem
{
    public override void Use()
    {
        FlashlightController flashlightController = GameObject.FindGameObjectWithTag("CameraRig").GetComponentInChildren<FlashlightController>(true);

        flashlightController.gameObject.SetActive(!flashlightController.gameObject.activeInHierarchy);
    }
}

