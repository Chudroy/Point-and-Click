using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;

public class ViewableImage : Interactable
{
    public Sprite sprite;


    public override void Start()
    {
        contextMenuName = "View";
        base.Start();
    }
    public override CursorType GetCursorType()
    {
        return base.GetCursorType();
    }
    public override bool HandleRaycast(PlayerController callingController)
    {
        if (this.enabled == false) return false;
        if (!Input.GetMouseButtonDown(0)) return false;
        GameManager.Instance.viewer2D.Activate(sprite);
        return true;
    }
}
