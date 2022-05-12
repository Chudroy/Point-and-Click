using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;

public class Observervable : Interactable
{
    public override void Start()
    {
        contextMenuName = "Observe";
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
        GameManager.Instance.viewer3D.Activate(gameObject);
        return true;
    }

    // public override void Interact()
    // {
    //     GameManager.Instance.viewer3D.Activate(gameObject);
    // }
}
