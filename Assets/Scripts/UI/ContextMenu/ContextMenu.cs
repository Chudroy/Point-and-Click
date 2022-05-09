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
    const string Path = "ContextMenuButton";
    PlayerController playerController;
    public static bool contextMenuIsOpen;
    // Start is called before the first frame update
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseIsOverContextMenu()) return;
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
            SetupButton(() => examinableItem.Examine(playerController), "Examine");
        }
    }

    void SetupButton(UnityAction method, string buttonName)
    {
        GameObject buttonPrefab = Resources.Load<GameObject>(Path);
        GameObject button = Instantiate(buttonPrefab, GetComponentInChildren<Image>().transform);
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
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        foreach (RaycastHit hit in hits)
        {
            ContextMenu cm = hit.transform.GetComponent<ContextMenu>();
            if (cm != null) return true;
        }
        return false;
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
