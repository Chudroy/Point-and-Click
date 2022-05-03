using UnityEngine;

namespace GameDevTV.Inventories
{
    /// <summary>
    /// An inventory item that can be equipped to the player. Weapons could be a
    /// subclass of this.
    /// </summary>
    [CreateAssetMenu(fileName = "UseableItem", menuName = "ScriptableObjects/UseableItem")]
    public class Tool : InventoryItem
    {
        ToolObstacleList toolObstacleList;
        [SerializeField] string toolObstaclePairTitle;
        string toolName;
        const string Path = "ToolObstacleList";

        void Awake()
        {
            toolObstacleList = Resources.Load<ToolObstacleList>(Path);
            toolName = toolObstacleList.GetPair(toolObstaclePairTitle)._toolName;
        }
    }
}