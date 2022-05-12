using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewerAbstract : MonoBehaviour
{

    public virtual void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            HandleDeactivation();
        }
    }

    public bool IsActive()
    {
        return this.gameObject.activeInHierarchy;
    }

    public void HandleDeactivation()
    {
        if (IsActive())
        {
            Deactivate();
        }
    }

    public virtual void Deactivate()
    {

    }
}
