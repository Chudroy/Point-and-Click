// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;

// public class InventoryInputHandler : MonoBehaviour
// {
//     ContextMenuManager contextMenuManager;
//     InventoryPanel currentInventoryPanel;
//     public static bool inventoryPanelContextMenuIsOpen;
//     // Start is called before the first frame update
//     void Start()
//     {
//         contextMenuManager = gameObject.AddComponent<ContextMenuManager>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // HandleLeftClick();
//         HandleRightClick();
//     }

//     void HandleRightClick()
//     {
//         if (Input.GetMouseButtonDown(1) && IsPointerOverInventoryPanel())
//         {
//             HandleOpenContextMenu();
//         }

//         //add functionality for closing contextmenu
//         else if (Input.GetMouseButtonDown(1) && (!ClickContextGetter.IsPointerOverInventoryDisplay() || ClickContextGetter.TryGetInventoryPanel()))
//         {
//             HandleCloseContextMenu();
//         }
//     }

//     void HandleLeftClick()
//     {

//     }

//     void HandleCloseContextMenu()
//     {
//         contextMenuManager.CloseContextMenu();
//     }

//     void HandleOpenContextMenu()
//     {
//         contextMenuManager.LoadUIContextMenu();
//     }

//     bool IsPointerOverInventoryPanel()
//     {
//         if (ClickContextGetter.IsPointerOverInventoryDisplay() && ClickContextGetter.TryGetInventoryPanel() != null)
//         {
//             return true;
//         }
//         else
//         { return false; }
//     }
// }
