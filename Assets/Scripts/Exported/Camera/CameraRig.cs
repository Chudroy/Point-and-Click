using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraRig : MonoBehaviour
{
    public Transform yAxis;
    public Transform xAxis;
    [SerializeField] float tweenDuration = 0.75f;
    public float getTweenDuration { get { return tweenDuration; } }

    public void AlignToTarget(Transform target)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(yAxis.DOMove(target.position, tweenDuration));
        seq.Join(yAxis.DORotate(new Vector3(0, target.rotation.eulerAngles.y, 0), tweenDuration));
        seq.Join(xAxis.DOLocalRotate(new Vector3(target.rotation.eulerAngles.x, 0, 0), tweenDuration));
    }

    public void MoveToPreviousLocation(Transform previousCameraTransform, Transform fromLocation)
    {
        var dir = fromLocation.position - previousCameraTransform.position;
        dir.Normalize();

        var lookRotation = Quaternion.LookRotation(dir);

        Sequence seq = DOTween.Sequence();
        seq.Append(yAxis.DOMove(previousCameraTransform.position, tweenDuration));
        seq.Join(yAxis.DORotate(new Vector3(0, lookRotation.eulerAngles.y, 0), tweenDuration));
        seq.Join(xAxis.DOLocalRotate(new Vector3(lookRotation.eulerAngles.x, 0, 0), tweenDuration));
    }

    public void yAxisRotate(int angle)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(yAxis.DORotate(new Vector3(0, yAxis.rotation.eulerAngles.y + angle, 0), tweenDuration));
    }
}
