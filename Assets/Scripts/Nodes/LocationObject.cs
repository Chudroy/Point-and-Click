using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LocationObject
{
    [SerializeField] GameObject prefab;
    [SerializeField] RectTransform rectTransform;
    public GameObject _prefab => prefab;
    public RectTransform _rectTransform => rectTransform;
}
