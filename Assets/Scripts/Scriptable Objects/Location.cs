using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Location", menuName = "ScriptableObjects/Location")]
public class Location : ScriptableObject
{
    [SerializeField] Sprite backgroundSprite;
    [SerializeField] GameObject locationContainer;
    [SerializeField] bool pannableArea;
    RectTransform rectTransform;
    public Sprite _backgroundSprite => backgroundSprite;
    public GameObject _locationContainer => locationContainer;
    public RectTransform _rectTransform => rectTransform;
    public bool _pannableArea => pannableArea;

    private void OnEnable()
    {
        rectTransform = locationContainer.GetComponent<RectTransform>();
    }





}
