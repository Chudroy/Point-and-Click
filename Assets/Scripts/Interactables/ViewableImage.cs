using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (this.enabled == false) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;
        viewer2D.Activate(sprite);
        return;
    }
}
