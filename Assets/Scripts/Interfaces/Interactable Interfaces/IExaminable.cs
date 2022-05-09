using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;

public interface IExaminable : IInteractable
{
    public void Examine(PlayerController player);
}