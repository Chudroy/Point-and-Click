using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer3D : ViewerAbstract
{
    [HideInInspector] public Transform model;
    [Header("make sure obscamera rotation is 0,0,0")]
    public Transform rig;
    public float sensitivity = 3f;
    Quaternion modelRot;
    Quaternion rigRot;
    GameObject item;
    LocationStore locationStore;

    public override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(0) & (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            if (model == null)
                return;


            modelRot = model.rotation;
            rigRot = rig.rotation;

            ObjectRotation();
        }
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

    public void Activate(GameObject go)
    {
        locationStore = LocationStore.GetLocationStore();

        item = Instantiate(go);
        item.transform.SetParent(GameManager.Instance.viewer3D.rig);
        item.transform.localPosition = Vector3.zero;
        item.transform.GetChild(0).localPosition = Vector3.zero;

        model = item.transform;

        locationStore._currentNode.SetReachableNodesColliders(false);

        if (locationStore._currentNode.col != null)
            locationStore._currentNode.col.enabled = false;

        gameObject.SetActive(true);

    }

    public override void Deactivate()
    {
        locationStore = LocationStore.GetLocationStore();

        locationStore._currentNode.SetReachableNodesColliders(true);

        if (locationStore._currentNode.col != null)
            locationStore._currentNode.enabled = true;

        Destroy(item);
        rig.rotation = Quaternion.identity;

        gameObject.SetActive(false);
    }
}
