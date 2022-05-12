using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//add event for left clicking on nodes while context menu is open
//maybe use delegate for right click?

public class WorldSpaceMouseInputHandler : MonoBehaviour
{
    Viewer3D observerCamera;
    Viewer2D imageViewerCanvas;

    void Start()
    {
        imageViewerCanvas = GameManager.Instance.viewer2D;
        observerCamera = GameManager.Instance.viewer3D;
    }

    void Update()
    {
        HandleRightClick();
    }


    void HandleRightClick()
    {
        // if (Input.GetMouseButtonDown(1))
        // {
        //     if (!ClickContextGetter.ClickIsOnWorldspace())
        //     {
        //         return;
        //     }
        //     else if (ClickContextGetter.ClickIsOnWorldspace())
        //     {
        //         var currentLocation = GameManager.Instance.currentNode.GetComponent<Location>();
        //         var currentProp = GameManager.Instance.currentNode.GetComponent<Prop>();

        //         //check if not right clicking while obscam or imageviewer is active
        //         if (currentProp != null)
        //         {
        //             HandleRightClickWhileOnProp(currentProp);
        //         }
        //         else if (currentLocation != null)
        //         {
        //             HandleRightClickWhileOnLocation(currentLocation);
        //         }
        //     }
        // }
    }

    // void HandleRightClickWhileOnProp(Prop currentProp)
    // {
    //     currentProp.previousLocation.Arrive();
    // }

    // void HandleRightClickWhileOnLocation(Location currentLocation)
    // {
    //     if (!currentLocation.isCentralLocation)
    //     {
    //         currentLocation.previousLocation.ReturnToPreviousLocation(currentLocation.transform);
    //     }
    // }

}
