using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationStore : MonoBehaviour
{
    public Node _currentNode;
    public Node _startingNode;

    private void Awake()
    {
        InitializeCentralLocation();
    }

    private void Start()
    {
        _startingNode.Arrive();
    }

    public static LocationStore GetLocationStore()
    {
        var player = GameObject.FindWithTag("Player");
        return player.GetComponent<LocationStore>();
    }

    private void InitializeCentralLocation()
    {
        List<Location> centralLocations = new List<Location>();

        Location[] locations = FindObjectsOfType<Location>();


        foreach (Location location in locations)
        {
            if (location.isCentralLocation == true)
            {
                centralLocations.Add(location);
            }
        }

        if (centralLocations.Count > 1)
        {
            Debug.LogError("more than one central location assigned");
        }

        if (centralLocations.Count == 0)
        {
            Debug.LogError("no central location assigned");
        }

        _startingNode = centralLocations[0];
    }
}
