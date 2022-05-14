using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCollider : MonoBehaviour
{
    Collider debugCollider;
    private void Awake()
    {
        debugCollider = GetComponent<Collider>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("collider enabled: " + debugCollider.enabled);
    }
}
