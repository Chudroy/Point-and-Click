using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationSpawner : MonoBehaviour
{
    [SerializeField] Location currentLocation;
    Image locationBackground;
    private void Awake()
    {
        locationBackground = GetComponent<Image>();
    }
    private void Start()
    {
        LoadBackground();
        LoadObjectContainer();
    }

    private void SetPannable()
    {
        GetComponent<Panner>().SetCanPan(currentLocation._pannableArea);
    }

    private void LoadObjectContainer()
    {

        var objContainer = Instantiate(currentLocation._locationContainer, Vector3.zero, Quaternion.identity);
        objContainer.transform.SetParent(this.transform, false);
        var locationContainerRect = objContainer.GetComponent<RectTransform>();
        locationContainerRect.anchoredPosition3D = currentLocation._rectTransform.anchoredPosition3D;
        locationContainerRect.sizeDelta = currentLocation._rectTransform.sizeDelta;

    }

    private void LoadBackground()
    {
        locationBackground.color = Color.white;
        locationBackground.sprite = currentLocation._backgroundSprite;
    }

    internal void ChangeLocation(Location moveToLocation)
    {
        DestroyObjects();
        currentLocation = moveToLocation;
        LoadBackground();
        LoadObjectContainer();
        SetPannable();
    }

    private void DestroyObjects()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
