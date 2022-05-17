using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

[CreateAssetMenu(fileName = "Cassette", menuName = "ScriptableObjects/Tools/Cassette")]

public class CassetteItem : Tool, IObservable
{
    Inventory inventory;
    [SerializeField] GameObject cassetteModel;
    public GameObject _cassetteModel => cassetteModel;

    public void Observe()
    {
        ObserveModel?.Invoke(cassetteModel);
    }

    public override void OnResolve()
    {
        Inventory.GetPlayerInventory().RemoveItem(this as InventoryItem, 1);
    }
}
