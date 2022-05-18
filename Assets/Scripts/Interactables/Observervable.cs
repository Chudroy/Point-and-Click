using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.EventSystems;

public class Observervable : Interactable
{
    Viewer3D viewer3D;
    private void Awake()
    {
        viewer3D = Viewer3D.GetViewer3D();
    }
    public override void Start()
    {
        contextMenuName = "Observe";
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
        viewer3D.Activate(gameObject);
    }
}
