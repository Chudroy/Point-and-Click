using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    CameraRig cameraRig;
    MousePOV mousePOV;
    private void Awake()
    {
        cameraRig = GetComponentInParent<CameraRig>();
        mousePOV = GetComponentInParent<MousePOV>();
    }

    private void Update()
    {
        Quaternion rotation = new Quaternion(cameraRig.xAxis.localRotation.x,
        transform.localRotation.y,
        transform.localRotation.z,
        transform.localRotation.w);

        // Quaternion rotation = new Quaternion(mousePOV._xAxis.x, mousePOV._yAxis.y, transform.localRotation.z,
        // transform.localRotation.w);

        transform.localRotation = rotation;
    }
}
