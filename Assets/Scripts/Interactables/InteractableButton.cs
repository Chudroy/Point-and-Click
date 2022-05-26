using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableButton : Interactable
{
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 pressedPosition;
    [SerializeField] float step;
    public Action PressButton;
    Coroutine pressingButtonRoutine;


    public override void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("pressing button" + name);
        if (this.enabled == false) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;
        PressButton?.Invoke();
        pressingButtonRoutine = StartCoroutine(MoveButton());
    }

    IEnumerator MoveButton()
    {
        if (pressingButtonRoutine != null) yield break;

        while (transform.localPosition != pressedPosition)
        {
            yield return null;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, pressedPosition, step * Time.deltaTime);
        }

        while (transform.localPosition != startPosition)
        {
            yield return null;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPosition, step * Time.deltaTime);
        }

        pressingButtonRoutine = null;
    }
}
