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
        GameObject ObservableCassette = new GameObject();
        GameObject ObservableModel = Instantiate(cassetteModel);
        ObservableModel.transform.SetParent(ObservableCassette.transform);

        ObserveModel?.Invoke(ObservableCassette);
    }

    public override void OnResolve()
    {
        Debug.Log("resolving cassette");
        Inventory.GetPlayerInventory().RemoveItem(this as InventoryItem, 1);
    }
}
