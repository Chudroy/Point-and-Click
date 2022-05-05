using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    ToolObstacleList toolObstacleList;
    [SerializeField] string toolObstaclePairTitle;
    string obstacleName;
    const string Path = "ToolObstacleList";
    public string _obstacleName => obstacleName;
    private void Start()
    {
        toolObstacleList = Resources.Load<ToolObstacleList>(Path);
        obstacleName = toolObstacleList.GetPair(toolObstaclePairTitle)._obstacleName;
    }
    public void Resolve(GameDevTV.Inventories.Tool tool)
    {
        Debug.Log("resolving obstacle");
    }

    internal void FailTry()
    {
        Debug.Log("failed try");
    }
}
