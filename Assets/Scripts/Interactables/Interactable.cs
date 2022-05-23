using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Prop))]
[DisallowMultipleComponent]
public abstract class Interactable : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector] public string contextMenuName = "";

    //if this is disabled, interactables need two clicks to be able to be interacted with.
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

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (this.enabled == false) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;
        Debug.Log("interacting with " + name);
    }

    public virtual void LeaveInteractable()
    {
        Debug.Log("leaving interactable");
    }
}



