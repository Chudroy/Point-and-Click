using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationBackground : MonoBehaviour
{
    Image backgroundImage;
    public Image _backgroundImage => backgroundImage;
    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }
}
