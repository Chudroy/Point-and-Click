using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Viewer2D : ViewerAbstract, IPointerClickHandler
{
    public Image image;
    LocationStore locationStore;

    public static Viewer2D GetViewer2D()
    {
        var core = GameObject.FindWithTag("Core");
        return core.GetComponentInChildren<Viewer2D>(true);
    }

    public void Activate(Sprite sprite)
    {
        //static public bool from base class
        if (active) return;

        locationStore = LocationStore.GetLocationStore();

        locationStore._currentNode.SetReachableNodesColliders(false);

        if (locationStore._currentNode.col != null)
            locationStore._currentNode.col.enabled = false;

        SetViewerActive(true);
        image.sprite = sprite;
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
    }

    public void Deactivate()
    {
        locationStore = LocationStore.GetLocationStore();

        locationStore._currentNode.SetReachableNodesColliders(true);

        if (locationStore._currentNode.col != null)
            locationStore._currentNode.col.enabled = true;

        SetViewerActive(false);
        image.sprite = null;
    }

    void SetViewerActive(bool t)
    {
        active = t;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(t);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;
        Deactivate();
    }
}
