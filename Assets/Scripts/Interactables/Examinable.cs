using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.EventSystems;

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
}


