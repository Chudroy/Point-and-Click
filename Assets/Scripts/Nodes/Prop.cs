using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// [RequireComponent(typeof(Highlighter))]
public class Prop : Node
{
    Interactable interactable;
    [SerializeField] private bool canArrive = true;

    public override void Awake()
    {
        base.Awake();
        interactable = GetComponent<Interactable>();
    }

    public override void Arrive()
    {
        if (interactable != null && interactable.enabled)
        {
            return;
        }

        if (canArrive)
        {
            base.Arrive();
        }

        //make this object ineractable if prerequisite is met

        if (interactable != null)
        {
            col.enabled = true;
            interactable.enabled = true;
        }
    }

    public override void Leave()
    {
        base.Leave();

        if (interactable != null)
        {
            interactable.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("clicking on prop");
    }
}
