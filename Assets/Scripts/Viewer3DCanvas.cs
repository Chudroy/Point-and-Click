using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Viewer3DCanvas : MonoBehaviour, IPointerClickHandler
{
    Viewer3D viewer3D;

    private void Awake()
    {
        viewer3D = GetComponentInParent<Viewer3D>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;
        viewer3D.Deactivate();
    }
}
