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
        public static Action<string> LogNothingHappened;
        RaycastHit[] hits;
        LocationStore locationStore;
        bool controlEnabled = false;


        private void Awake()
        {
            locationStore = GetComponent<LocationStore>();
        }

        private void OnEnable()
        {
            PlayerControlToggler.SetControl += SetControl;
        }

        private void OnDisable()
        {
            PlayerControlToggler.SetControl -= SetControl;
        }

        void SetControl(bool t, object sender)
        {
            Debug.Log(sender);
            controlEnabled = t;
        }

        private void Update()
        {
            if (!EnabledControl()) return;
            if (HandleLeftClick()) return;
            if (InteractWithUI()) return;
            if (HandleRightClick()) return;
        }

        bool EnabledControl()
        {
            return controlEnabled;
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
                var tool = currentTool;
                currentTool = null;

                foreach (RaycastHit hit in hits)
                {
                    Obstacle obstacle = hit.transform.GetComponent<Obstacle>();

                    if (obstacle != null)
                    {
                        if (obstacle.CanBeSolvedBy(tool) == true)
                        {
                            Debug.Log("resolving");
                            obstacle.Resolve(tool);
                            Collector.ResetCountDown();
                            return true;
                        }
                        else
                        {
                            Debug.Log("fail try");
                            obstacle.FailTry();
                            Collector.ResetCountDown();
                            return true;
                        }
                    }
                }
                Collector.ResetCountDown();
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