using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;
using UnityEngine.EventSystems;

public class Viewer3D : ViewerAbstract
{
    [HideInInspector] public Transform model;
    [Header("make sure obscamera rotation is 0,0,0")]
    [SerializeField] Transform rig;
    [SerializeField] Transform panelTransform;
    [SerializeField] float sensitivity = 3f;
    [SerializeField] float scrollSpeed = 1f;
    Camera rigCamera;
    Vector3 rigDefaultPosition;
    Quaternion modelRot;
    Quaternion rigRot;
    GameObject item;
    LocationStore locationStore;

    private void Awake()
    {
        rigCamera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        InventoryItem.ObserveModel += Activate;
    }
    private void OnDisable()
    {
        InventoryItem.ObserveModel -= Activate;
    }

    private void Start()
    {
        rigDefaultPosition = rig.transform.position;
    }

    public static Viewer3D GetViewer3D()
    {
        var core = GameObject.FindGameObjectWithTag("Core");
        return core.GetComponentInChildren<Viewer3D>(true);
    }

    public void Update()
    {
        if (model == null) return;

        if (Input.GetMouseButton(0) & (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            modelRot = model.rotation;
            rigRot = rig.rotation;

            ObjectRotation();
        }

        Zoom();
    }

    public void ObjectRotation()
    {
        float yRot = Input.GetAxis("Mouse X") * sensitivity;
        float xRot = Input.GetAxis("Mouse Y") * sensitivity;

        modelRot *= Quaternion.Euler(0f, -yRot, 0f);
        rigRot *= Quaternion.Euler(xRot, 0f, 0f);

        rigRot = ClampRotationAroundXAxis(rigRot);

        model.rotation = modelRot;
        rig.rotation = rigRot;
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, -80f, 80f);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

    GameObject SetParentForItem(GameObject item)
    {
        GameObject parentGameObject = new GameObject();
        item.transform.SetParent(parentGameObject.transform);
        return parentGameObject;
    }

    public void Activate(GameObject go)
    {
        //static public bool from base class
        if (active) return;

        locationStore = LocationStore.GetLocationStore();

        item = Instantiate(go);

        if (go.transform.parent == null)
        {
            item = SetParentForItem(item);
        }

        item.transform.SetParent(this.rig);
        item.transform.localPosition = Vector3.zero;
        item.transform.GetChild(0).localPosition = Vector3.zero;

        model = item.transform;

        locationStore._currentNode.SetReachableNodesColliders(false);

        if (locationStore._currentNode.col != null)
            locationStore._currentNode.col.enabled = false;

        SetViewerActive(true);
    }



    public void Deactivate()
    {
        locationStore = LocationStore.GetLocationStore();

        locationStore._currentNode.SetReachableNodesColliders(true);

        if (locationStore._currentNode.col != null)
        {
            locationStore._currentNode.col.enabled = true;
        }

        Destroy(item);
        rig.rotation = Quaternion.identity;
        rig.transform.position = rigDefaultPosition;
        SetViewerActive(false);
    }

    void SetViewerActive(bool t)
    {
        rig.gameObject.SetActive(t);
        panelTransform.gameObject.SetActive(t);
        rigCamera.enabled = t;
        active = t;
    }

    void Zoom()
    {
        //scroll model
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float zScroll = scroll * scrollSpeed;

        rig.transform.Translate(0, 0, zScroll, Space.World);

        //clamp position of model
        var pos = rig.transform.localPosition;
        pos.z = Mathf.Clamp(pos.z, 1, 5);
        rig.transform.localPosition = pos;

    }
}

