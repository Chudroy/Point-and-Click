using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Viewer2D : ViewerAbstract
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
        locationStore = LocationStore.GetLocationStore();

        locationStore._currentNode.SetReachableNodesColliders(false);

        if (locationStore._currentNode.col != null)
            locationStore._currentNode.col.enabled = false;

        gameObject.SetActive(true);
        image.sprite = sprite;
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.texture.width, sprite.texture.height);
    }

    public override void Deactivate()
    {
        locationStore = LocationStore.GetLocationStore();

        locationStore._currentNode.SetReachableNodesColliders(true);

        if (locationStore._currentNode.col != null)
            locationStore._currentNode.col.enabled = true;

        gameObject.SetActive(false);
        image.sprite = null;
    }

}
