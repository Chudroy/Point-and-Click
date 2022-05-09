using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using System.Linq;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Tool[] solutionTools;
    // [SerializeField] string toolObstaclePairTitle;
    // string obstacleName;
    // const string Path = "ToolObstacleList";
    // public string _obstacleName => obstacleName;
    private void Start()
    {
        // toolObstacleList = Resources.Load<ToolObstacleList>(Path);
        // obstacleName = toolObstacleList.GetPair(toolObstaclePairTitle)._obstacleName;
    }
    public virtual void Resolve(GameDevTV.Inventories.Tool tool)
    {
        Debug.Log("resolving obstacle");
    }

    public virtual void FailTry()
    {
        Debug.Log("failed try");
    }

    public bool CanBeSolvedBy(Tool tool) => solutionTools.Any(solutionTool => solutionTool == tool);
}
