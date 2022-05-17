using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRig))]
public class MousePOV : MonoBehaviour
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;


    private Quaternion yAxis;
    private Quaternion xAxis;
    private bool m_cursorIsLocked = true;
    private CameraRig cameraRig;
    Viewer3D viewer3D;
    Viewer2D viewer2D;


    private void Awake()
    {
        viewer3D = Viewer3D.GetViewer3D();
        viewer2D = Viewer2D.GetViewer2D();
    }

    private void Start()
    {
        cameraRig = GetComponent<CameraRig>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(2) & (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {

            yAxis = cameraRig.yAxis.localRotation;
            xAxis = cameraRig.xAxis.localRotation;

            LookRotation();
        }
    }

    public void Init(Transform character, Transform camera)
    {
        yAxis = character.localRotation;
        xAxis = camera.localRotation;
    }


    public void LookRotation()
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        yAxis *= Quaternion.Euler(0f, yRot, 0f);
        xAxis *= Quaternion.Euler(-xRot, 0f, 0f);

        if (clampVerticalRotation)
            xAxis = ClampRotationAroundXAxis(xAxis);

        if (smooth)
        {
            cameraRig.yAxis.localRotation = Quaternion.Slerp(cameraRig.yAxis.localRotation, yAxis,
                smoothTime * Time.deltaTime);
            cameraRig.xAxis.localRotation = Quaternion.Slerp(cameraRig.xAxis.localRotation, xAxis,
                smoothTime * Time.deltaTime);
        }
        else
        {
            cameraRig.yAxis.localRotation = yAxis;
            cameraRig.xAxis.localRotation = xAxis;
        }

        UpdateCursorLock();
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if (!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}
