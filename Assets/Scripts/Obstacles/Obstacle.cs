using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using System.Linq;
public class Obstacle : Interactable
{
    [SerializeField] Tool[] solutionTools;
    public static Action<string> LogFailTry;

    public virtual void Resolve(Tool tool)
    {
        Debug.Log("resolving obstacle");
    }

    public virtual void FailTry()
    {
        LogFailTry?.Invoke("it didn't work");
    }

    public bool CanBeSolvedBy(Tool tool) => solutionTools.Any(solutionTool => solutionTool == tool);
}
