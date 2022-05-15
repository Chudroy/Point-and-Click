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

        public Tool currentTool;
        RaycastHit[] hits;
        LocationStore locationStore;
        Viewer3D viewer3D;
        Viewer2D viewer2D;
        ExamineTextPoster examineTextPoster;
        public static PlayerController GetPlayerController()
        {
            var player = GameObject.FindWithTag("Player");
            return player.GetComponent<PlayerController>();
        }

        private void Awake()
        {
            examineTextPoster = ExamineTextPoster.GetExamineTextPoster();
            locationStore = GetComponent<LocationStore>();
            viewer2D = Viewer2D.GetViewer2D();
            viewer3D = Viewer3D.GetViewer3D();
        }

        private void Update()
        {
            if (InteractWithUI()) return;
            if (HandleRightClick()) return;
            if (HandleLeftClick()) return;
        }

        bool HandleRightClick()
        {
            if (ExitingViewer()) return true;
            if (ReturningToPreviousLocation()) return true;
            return false;
        }

        private bool ExitingViewer()
        {
            if (!Input.GetMouseButtonDown(1)) return false;

            if (viewer2D.gameObject.activeInHierarchy == true)
            {
                viewer2D.Deactivate();
                return true;
            }

            if (viewer3D.gameObject.activeInHierarchy == true)
            {
                viewer3D.Deactivate();
                return true;
            }

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
            //inventory ui components aren't rendered infront of cam anymore, so raycasting for their collider doesn't work
            //find a new way to raycast for inventory items.
            if (InteractWithComponent()) return true;
            if (InteractWithMovement()) return true;
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
                Tool tool = currentTool;
                currentTool = null;

                // RaycastHit[] hits = RaycastAllSorted();
                foreach (RaycastHit hit in hits)
                {
                    Obstacle obstacle = hit.transform.GetComponent<Obstacle>();

                    if (obstacle != null)
                    {
                        if (obstacle.CanBeSolvedBy(tool) == true)
                        {
                            obstacle.Resolve(tool);
                            tool.OnResolve();
                            return true;
                        }
                        else
                        {
                            obstacle.FailTry();
                            return true;
                        }
                    }
                }
                examineTextPoster.SetExamineText("nothing happened");
            }
            return true;
        }

        private bool InteractWithMovement()
        {
            if (!Input.GetMouseButtonDown(0)) return false;
            foreach (RaycastHit hit in hits)
            {
                Node node = hit.transform.GetComponent<Node>();
                if (node != null && node.col.enabled == true)
                {
                    node.Arrive();
                    return true;
                }
            }
            return false;
        }

        private bool InteractWithComponent()
        {
            // RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        // SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
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