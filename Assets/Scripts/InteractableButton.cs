using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableButton : Interactable
{
    public Action PressButton;

    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("pressing button" + name);
        if (this.enabled == false) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;
        PressButton?.Invoke();
    }
}
