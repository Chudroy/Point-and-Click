using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panner : MonoBehaviour
{

    RectTransform rectTransform;
    Rect canvasRect;
    [SerializeField] float panSpeed = 1f;
    [SerializeField] float panThreshold = 0.5f;
    float clampVal;
    bool canPan = true;
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
        if (canPan)
        {
            Move();
            ClampToWindow();
        }
    }

    void Move()
    {
        if (Input.mousePosition.y < 300) return;

        rectTransform.localPosition = new Vector3(
            rectTransform.localPosition.x + Time.deltaTime * panSpeed * -GetSpeedFraction(),
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

    float GetSpeedFraction()
    {
        // calculates mousepos between 0-screen width
        var clampedX = Mathf.Clamp(Input.mousePosition.x, 0, Screen.width);
        // calculates mousepos between 0-1
        var normalizedX = clampedX / Screen.width;
        // calculates mousepos between -1-1
        var lerpFraction = Mathf.Lerp(-1, 1, normalizedX);

        if (lerpFraction > panThreshold || lerpFraction < -panThreshold)
        {
            var smooth = lerpFraction * lerpFraction * lerpFraction;
            return smooth;
        }

        return 0;
    }

    public void SetCanPan(bool b)
    {
        canPan = b;
    }
}
