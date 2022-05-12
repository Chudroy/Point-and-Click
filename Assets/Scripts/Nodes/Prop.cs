using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Highlighter))]
public class Prop : Node
{
    Interactable interactable;

    public override void Awake()
    {
        base.Awake();
        interactable = GetComponent<Interactable>();
    }

    public override void Arrive()
    {
        if (interactable != null && interactable.enabled)
        {
            // interactable.Interact();
            return;
        }

        base.Arrive();

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
}
