using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolObstacleList", menuName = "ScriptableObjects/ToolObstacleList")]
public class ToolObstacleList : ScriptableObject
{
    [SerializeField] ToolObstaclePairs[] toolObstaclePairs;
    Dictionary<string, ToolObstaclePairs> lookupTable = null;

    public ToolObstaclePairs GetPair(string title)
    {
        BuildLookup();

        return lookupTable[title];

    }

    private void BuildLookup()
    {
        if (lookupTable != null) return;

        lookupTable = new Dictionary<string, ToolObstaclePairs>();

        foreach (ToolObstaclePairs pair in toolObstaclePairs)
        {
            lookupTable[pair._title] = pair;
        }
    }

    [System.Serializable]
    public struct ToolObstaclePairs
    {
        [SerializeField] string title;
        [SerializeField] string obstacleName;
        [SerializeField] string toolName;
        public string _title => title;
        public string _obstacleName => obstacleName;
        public string _toolName => toolName;
    }
}
