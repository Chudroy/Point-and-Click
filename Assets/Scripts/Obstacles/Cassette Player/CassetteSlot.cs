using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using InventoryExample.Control;
using UnityEngine;
using UnityEngine.EventSystems;

public class CassetteSlot : Obstacle
{
    //Obstacle should handle removing tool from inventory, because it needs to check if the tool was placed in world space, for example

    //click cool down should be positive number.
    [SerializeField] float clickCooldown = 1f;
    float clickTime;
    GameObject currentCassetteModel;
    Inventory playerInventory;
    public GameObject _currentCassetteModel => currentCassetteModel;


    private void Awake()
    {
        playerInventory = Inventory.GetPlayerInventory();
    }

    public override void Start()
    {
        base.Start();
        ResetClickCoolDown();
    }

    private void Update()
    {
        clickTime -= Time.deltaTime;
        clickTime = Mathf.Clamp(clickTime, -1, clickCooldown);
    }

    public override void Resolve(Tool tool)
    {
        ResetClickCoolDown();

        var cassetteItem = tool as CassetteItem;
        if (cassetteItem == null) Debug.LogWarning("accepted item isn't a cassette");
        if (currentCassetteModel != null) return;

        currentCassetteModel = Instantiate(cassetteItem._cassetteModel, Vector3.zero, Quaternion.identity);
        currentCassetteModel.transform.SetParent(this.transform, false);
        currentCassetteModel.GetComponent<Collider>().enabled = true;

        cassetteItem.OnResolve();
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        //pick up cassette in slot if the click goes to the cassette slot and not to the cassette interactable model itself.
        if (this.enabled == false) return;
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (currentCassetteModel == null) return;
        if (clickTime <= 0)
        {
            currentCassetteModel.GetComponent<Interactable>().OnPointerClick(eventData);
            ResetClickCoolDown();

        }
    }

    bool ClickCoolDownIsOver()
    {
        Debug.Log(clickTime <= 0);
        return clickTime <= 0;
    }

    void ResetClickCoolDown()
    {
        clickTime = clickCooldown;
    }
}
