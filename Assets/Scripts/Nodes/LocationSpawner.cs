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
        LoadObjects();
    }

    private void LoadObjects()
    {
        foreach (LocationObject locationObject in currentLocation._locationObjects)
        {
            var obj = Instantiate(locationObject._prefab, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(this.transform, false);
            var rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.anchoredPosition3D = locationObject._rectTransform.anchoredPosition3D;
            rectTransform.sizeDelta = locationObject._rectTransform.sizeDelta;
        }
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
        LoadObjects();
    }

    private void DestroyObjects()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
