using System.Collections;
using System.Collections.Generic;
using GameDevTV.Core.UI.Tooltips;
using GameDevTV.UI.Inventories;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(IItemHolder))]
public class ContextMenuSpawner : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("The prefab of the context menu to spawn.")]
    [SerializeField] GameObject contextMenuPrefab = null;

    // PRIVATE STATE
    GameObject contextMenu = null;

    /// <summary>
    /// Return true when the tooltip spawner should be allowed to create a tooltip.
    /// </summary>

    public bool CanCreateContextMenu()
    {
        var item = GetComponent<IItemHolder>().GetItem();
        if (!item) return false;
        return true;
    }

    public void UpdateContextMenu(GameObject contextMenu)
    {
        var itemContextMenu = contextMenu.GetComponent<ContextMenu>();
        if (!itemContextMenu) return;

        var item = GetComponent<IItemHolder>().GetItem();

        itemContextMenu.SetupContextMenu(item);
    }

    private void OnDestroy()
    {
        ClearContextMenu();
    }

    private void OnDisable()
    {
        ClearContextMenu();
    }

    void IPointerClickHandler.OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        var parentCanvas = GetComponentInParent<Canvas>();

        if (contextMenu && !CanCreateContextMenu())
        {
            ClearContextMenu();
        }

        if (!contextMenu && CanCreateContextMenu())
        {
            contextMenu = Instantiate(contextMenuPrefab, parentCanvas.transform);
        }

        if (contextMenu)
        {
            UpdateContextMenu(contextMenu);
            PositionContextMenu();
        }

    }

    private void PositionContextMenu()
    {
        // Required to ensure corners are updated by positioning elements.
        Canvas.ForceUpdateCanvases();

        var contextMenuCorners = new Vector3[4];
        contextMenu.GetComponent<RectTransform>().GetWorldCorners(contextMenuCorners);
        var slotCorners = new Vector3[4];
        GetComponent<RectTransform>().GetWorldCorners(slotCorners);

        bool below = transform.position.y > Screen.height / 2;
        bool right = transform.position.x < Screen.width / 2;

        int slotCorner = GetCornerIndex(below, right);
        int contextMenuCorner = GetCornerIndex(!below, !right);

        contextMenu.transform.position = slotCorners[slotCorner] - contextMenuCorners[contextMenuCorner] + contextMenu.transform.position;

        //TODO Position contextmenu corner on current mouse position
        //alternative: pause all input until contextmenu has been resolved.
    }

    private int GetCornerIndex(bool below, bool right)
    {
        if (below && !right) return 0;
        else if (!below && !right) return 1;
        else if (!below && right) return 2;
        else return 3;

    }

    // void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    // {
    //     ClearContextMenu();
    // }

    private void ClearContextMenu()
    {
        if (contextMenu)
        {
            Destroy(contextMenu.gameObject);
        }
    }
}

