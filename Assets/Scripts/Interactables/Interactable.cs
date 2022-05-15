using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;

[RequireComponent(typeof(Prop))]
[DisallowMultipleComponent]
public abstract class Interactable : MonoBehaviour, IRaycastable
{
    [HideInInspector] public string contextMenuName = "";
    [SerializeField] bool disableOnStart = true;

    public virtual void Start()
    {
        if (disableOnStart)
        {
            this.enabled = false;
        }

    }

    public virtual CursorType GetCursorType()
    {
        return CursorType.None;
    }

    public virtual bool HandleRaycast(PlayerController callingController)
    {
        if (this.enabled == false) return false;
        if (!Input.GetMouseButtonDown(0)) return false;
        Debug.Log("interacting with " + name);
        return true;
    }
}



