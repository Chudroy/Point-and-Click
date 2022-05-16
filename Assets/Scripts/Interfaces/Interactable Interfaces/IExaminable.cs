using System;
using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;

public interface IExaminable : IInteractable
{
    //To use this interface, have a public static event that the class FeedbackTextPoster subscribes the "PostExamineMessage()" method to.
    public void Examine();
}