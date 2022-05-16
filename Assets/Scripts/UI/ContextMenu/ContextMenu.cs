using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using InventoryExample.Control;
using UnityEngine;
using System.Runtime;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ContextMenu : MonoBehaviour
{
    GameObject buttonPrefab;
    PlayerController playerController;
    const string Path = "ContextMenuButton";
    public static bool contextMenuIsOpen;
    // Start is called before the first frame update
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseIsOverContextMenu())
        {
            return;
        }
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) Destroy(gameObject);
    }

    public void SetupContextMenu(InventoryItem item)
    {
        contextMenuIsOpen = true;

        EventSystem.current.SetSelectedGameObject(this.gameObject);

        IUsable usableItem = item as IUsable;

        if (usableItem != null)
        {
            SetupButton(() => usableItem.Use(playerController), "Use");
        }

        IExaminable examinableItem = item as IExaminable;

        if (examinableItem != null)
        {
            SetupButton(() => examinableItem.Examine(), "Examine");
        }

        IObservable observableItem = item as IObservable;

        if (observableItem != null)
        {
            SetupButton(() => observableItem.Observe(), "Observe");
        }
    }

    void SetupButton(UnityAction method, string buttonName)
    {
        GameObject buttonPrefab = Resources.Load<GameObject>(Path);
        GameObject buttonGameObject = Instantiate(buttonPrefab, transform.GetChild(0).transform);
        Button button = buttonGameObject.GetComponent<Button>();
        button.GetComponent<Button>().onClick.AddListener(method);
        button.GetComponent<Button>().onClick.AddListener(() => ClearContextMenu());
        button.GetComponent<Button>().GetComponentInChildren<Text>().text = buttonName;
    }

    void ClearContextMenu()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        contextMenuIsOpen = false;
    }

    bool MouseIsOverContextMenu()
    {
        List<RaycastResult> hits = new List<RaycastResult>();
        PointerEventData m_pointerEventData = new PointerEventData(EventSystem.current);
        m_pointerEventData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(m_pointerEventData, hits);

        foreach (RaycastResult hit in hits)
        {
            ContextMenu cm = hit.gameObject.GetComponent<ContextMenu>();
            if (cm != null) return true;
        }
        return false;
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
