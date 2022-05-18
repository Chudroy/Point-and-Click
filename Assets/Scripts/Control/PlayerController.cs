using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using GameDevTV.Inventories;

namespace InventoryExample.Control
{
    public class PlayerController : MonoBehaviour
    {
        [System.Serializable]
        public struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float raycastRadius = 1f;

        //OTHER

        public static Tool currentTool;
        RaycastHit[] hits;
        LocationStore locationStore;
        public static Action<string> LogNothingHappened;

        public static PlayerController GetPlayerController()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<PlayerController>();
        }

        private void Awake()
        {
            locationStore = GetComponent<LocationStore>();
        }

        private void Update()
        {
            if (HandleLeftClick()) return;
            if (InteractWithUI()) return;
            if (HandleRightClick()) return;
        }

        bool HandleRightClick()
        {
            if (ReturningToPreviousLocation()) return true;
            return false;
        }

        private bool ReturningToPreviousLocation()
        {
            if (!Input.GetMouseButtonDown(1)) return false;

            Location currentLocation = locationStore._currentNode as Location;
            if (currentLocation != null && currentLocation.isCentralLocation) return false;

            Node previousLocation = locationStore._currentNode.previousLocation;
            if (previousLocation == null) return false;

            previousLocation.Arrive();

            return true;
        }

        bool HandleLeftClick()
        {
            RayCastForInteraction();
            if (InteractWithTool()) return true;
            // if (InteractWithMovement()) return true;
            // SetCursor(CursorType.None);
            return false;
        }

        private bool InteractWithUI()
        {
            if (ContextMenu.contextMenuIsOpen) return true;
            if (EventSystem.current.IsPointerOverGameObject()) return true;
            return false;
        }

        private void RayCastForInteraction()
        {
            hits = RaycastAllSorted();
        }

        private bool InteractWithTool()
        {
            if (currentTool == null) return false;
            if (Input.GetMouseButtonDown(0))
            {
                // RaycastHit[] hits = RaycastAllSorted();
                foreach (RaycastHit hit in hits)
                {
                    Obstacle obstacle = hit.transform.GetComponent<Obstacle>();

                    if (obstacle != null)
                    {
                        if (obstacle.CanBeSolvedBy(currentTool) == true)
                        {
                            Debug.Log("resolving");
                            obstacle.Resolve(currentTool);
                            currentTool = null;
                            return true;
                        }
                        else
                        {
                            Debug.Log("fail try");
                            obstacle.FailTry();
                            currentTool = null;
                            return true;
                        }
                    }
                }
                LogNothingHappened?.Invoke("nothing happened");
            }
            return true;
        }

        RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}