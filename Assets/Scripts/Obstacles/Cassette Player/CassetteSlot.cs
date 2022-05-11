using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public class CassetteSlot : Obstacle
{
    GameObject currentCassetteModel;
    public override void Resolve(Tool tool)
    {
        var cassetteTool = tool as CassetteItem;
        if (cassetteTool == null) Debug.LogWarning("accepted item isn't a cassette");

        if (currentCassetteModel != null) return;

        currentCassetteModel = Instantiate(cassetteTool._cassetteModel, Vector3.zero, Quaternion.identity);
        currentCassetteModel.transform.SetParent(this.transform, false);

        // var cassetteRect = cassetteModel.GetComponent<RectTransform>();
        // var slotRect = GetComponent<RectTransform>();
        // cassetteRect.anchoredPosition3D = slotRect.anchoredPosition3D;
        // cassetteRect.sizeDelta = slotRect.sizeDelta;
    }
}
