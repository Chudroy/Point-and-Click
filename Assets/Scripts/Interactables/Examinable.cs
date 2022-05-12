using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;

public class Examinable : Interactable
{
    public override void Start()
    {
        contextMenuName = "";
        base.Start();
    }
    public override CursorType GetCursorType()
    {
        return base.GetCursorType();
    }
    public override bool HandleRaycast(PlayerController callingController)
    {
        return base.HandleRaycast(callingController);
    }
}


