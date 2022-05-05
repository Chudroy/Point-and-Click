using System;
using InventoryExample.Control;
using UnityEngine;

namespace GameDevTV.Inventories
{
    /// <summary>
    /// An inventory item that can be equipped to the player. Weapons could be a
    /// subclass of this.
    /// </summary>
    [CreateAssetMenu(fileName = "Tool", menuName = "ScriptableObjects/Tool")]
    public class Tool : InventoryItem, IUsable
    {
        ToolObstacleList toolObstacleList;
        [SerializeField] string toolObstaclePairTitle;
        string toolName;
        string targetObstacle;
        const string Path = "ToolObstacleList";

        private void OnEnable()
        {
            toolObstacleList = Resources.Load<ToolObstacleList>(Path);
            toolName = toolObstacleList.GetPair(toolObstaclePairTitle)._toolName;
            targetObstacle = toolObstacleList.GetPair(toolObstaclePairTitle)._obstacleName;
        }


        public bool TryOn(Obstacle obstacle)
        {
            if (targetObstacle == obstacle._obstacleName)
            {
                return true;
            }
            return false;

        }

        public void Use(PlayerController playerController)
        {
            playerController.currentTool = this;
        }
    }
}