using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panner : MonoBehaviour
{

    RectTransform rectTransform;
    Rect canvasRect;
    [SerializeField] float panSpeed = 1f;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        canvasRect = GetComponentInParent<Canvas>().pixelRect;
    }

    // Update is called once per frame
    void Update()
    {
        if (ClampToWindow()) return;
        Move();

    }

    void Move()
    {
        rectTransform.position = new Vector3(
            rectTransform.position.x + Time.deltaTime * panSpeed,
            rectTransform.position.y,
            rectTransform.position.z
        );
    }

    bool ClampToWindow()
    {

        //clamp rect horizontally within UI
        // if (rectTransform.localPosition.x >= (canvasRect.width / 2) - rectTransform.rect.width / 2) return true;
        // if (rectTransform.localPosition.x <= -(canvasRect.width / 2) + rectTransform.rect.width / 2) return true;

        //clamp scrolling sides within UI
        if (rectTransform.localPosition.x - rectTransform.rect.width / 2 >= -(canvasRect.width / 2)) return true;
        if (rectTransform.localPosition.x + rectTransform.rect.width / 2 <= (canvasRect.width / 2)) return true;

        return false;
    }
}
