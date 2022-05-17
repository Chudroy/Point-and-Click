using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Location : Node
{
    public bool isCentralLocation;

    private void Start()
    {
        foreach (Node node in reachableNodes)
        {
            Prop prop = node as Prop;
            if (prop == null) continue;
            prop.previousLocation = this;
        }
    }
}
