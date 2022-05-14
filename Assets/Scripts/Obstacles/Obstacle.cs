using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using System.Linq;

public class Obstacle : Interactable
{
    [SerializeField] Tool[] solutionTools;

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
