using System.Collections;
using System.Collections.Generic;
using InventoryExample.Control;
using UnityEngine;

public interface IUsable : IInteractable
{
    public void Use(PlayerController player);
}
