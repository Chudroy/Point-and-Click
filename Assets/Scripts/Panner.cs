using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panner : MonoBehaviour
{

    RectTransform rectTransform;
    Rect canvasRect;
    [SerializeField] float panSpeed = 1f;
    float clampVal;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().pixelRect;
    }
    void Start()
    {
        clampVal = Mathf.Abs(rectTransform.rect.width - canvasRect.width) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ClampToWindow();
    }

    void Move()
    {
        rectTransform.localPosition = new Vector3(
            rectTransform.localPosition.x + Time.deltaTime * panSpeed,
            rectTransform.localPosition.y,
            rectTransform.localPosition.z
        );
    }

    void ClampToWindow()
    {
        if (rectTransform.localPosition.x - rectTransform.rect.width / 2 >= -(canvasRect.width / 2))
        {
            rectTransform.localPosition = new Vector3(
                 clampVal,
                 rectTransform.localPosition.y,
                 rectTransform.localPosition.z
             );
        }

        if (rectTransform.localPosition.x + rectTransform.rect.width / 2 <= (canvasRect.width / 2))
        {
            rectTransform.localPosition = new Vector3(
                            -clampVal,
                            rectTransform.localPosition.y,
                            rectTransform.localPosition.z
                        );
        }



    }
}
