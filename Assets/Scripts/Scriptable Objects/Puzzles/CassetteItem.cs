using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

[CreateAssetMenu(fileName = "Cassette", menuName = "ScriptableObjects/Tools/Cassette")]

public class CassetteItem : Tool
{
    Inventory inventory;
    [SerializeField] GameObject cassetteModel;
    public GameObject _cassetteModel => cassetteModel;

    public override void OnResolve()
    {
        Debug.Log("resolving cassette");
        Inventory.GetPlayerInventory().RemoveItem(this as InventoryItem);
    }
}
