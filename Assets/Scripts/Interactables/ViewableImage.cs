using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;

public class ViewableImage : Interactable
{
    public Sprite sprite;
    Viewer2D viewer2D;
    private void Awake()
    {
        viewer2D = Viewer2D.GetViewer2D();
    }

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
        viewer2D.Activate(sprite);
        return true;
    }
}
