using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collider))]
public abstract class Node : MonoBehaviour
{
    public Transform cameraPosition;
    public List<Node> reachableNodes = new List<Node>();
    [HideInInspector] public Collider col;
    public Node previousLocation;
    LocationStore locationStore;
    CameraRig cameraRig;

    public virtual void Awake()
    {
        locationStore = LocationStore.GetLocationStore();
        col = GetComponent<Collider>();
        col.enabled = false;
        cameraRig = GameObject.FindGameObjectWithTag("Core").GetComponentInChildren<CameraRig>();
    }

    public virtual void Leave()
    {
        SetReachableNodesColliders(false);
    }

    public virtual void Arrive()
    {
        if (locationStore._currentNode != null)
            locationStore._currentNode.Leave();

        cameraRig.AlignToTarget(cameraPosition);

        UpdateNodes();
    }

    protected void UpdateNodes()
    {
        SetCurrentNode();
        DisableCurrentNodeCollider();
        SetReachableNodesColliders(true);
    }

    private void SetCurrentNode()
    {
        locationStore._currentNode = this;
    }

    void DisableCurrentNodeCollider()
    {
        col.enabled = false;
    }
    public void SetReachableNodesColliders(bool t)
    {
        foreach (Node node in reachableNodes)
        {
            if (node != null)
                node.col.enabled = t;
        }
    }


}
