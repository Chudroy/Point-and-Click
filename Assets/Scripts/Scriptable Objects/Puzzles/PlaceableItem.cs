using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;


[CreateAssetMenu(fileName = "PlaceableItem", menuName = "ScriptableObjects/Tools/PlaceableItem")]

public class PlaceableItem : Tool, IObservable
{

    //TODO Refactor Cassette Item to abstract it into placeable item 26/05/2022
    [SerializeField] GameObject Model;
    [SerializeField] CassetteType cassetteType;
    [SerializeField] AudioClip tapeClip;
    public GameObject _Model => Model;
    public CassetteType _cassetteType => cassetteType;
    public AudioClip _tapeClip => tapeClip;

    public void Observe()
    {
        ObserveModel?.Invoke(Model);
    }

    public override void OnResolve()
    {
        Inventory.GetPlayerInventory().RemoveItem(this as InventoryItem, 1);
    }
}
