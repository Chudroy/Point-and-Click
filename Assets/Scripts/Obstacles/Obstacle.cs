using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using System.Linq;
public class Obstacle : Interactable
{
    [SerializeField] Tool[] solutionTools;
    ExamineTextPoster examineTextPoster;

    private void Awake()
    {
        examineTextPoster = ExamineTextPoster.GetExamineTextPoster();
    }

    public virtual void Resolve(Tool tool)
    {
        Debug.Log("resolving obstacle");
    }

    public virtual void FailTry()
    {
        examineTextPoster.SetExamineText("It didn't work");
    }

    public bool CanBeSolvedBy(Tool tool) => solutionTools.Any(solutionTool => solutionTool == tool);
}
