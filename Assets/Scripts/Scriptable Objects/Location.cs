using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Location", menuName = "ScriptableObjects/Location")]
public class Location : ScriptableObject
{
    [SerializeField] Sprite backgroundSprite;
    [SerializeField] LocationObject[] locationObjects;

    public Sprite _backgroundSprite => backgroundSprite;
    public LocationObject[] _locationObjects => locationObjects;

}
