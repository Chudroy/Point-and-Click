using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] LocationScriptableObject moveToLocation;
    private void OnMouseDown()
    {
        Move();
    }
    private void OnMouseOver()
    {
        Debug.Log("currently over");
    }

    private void Move()
    {
        FindObjectOfType<Location>().ChangeLocation(moveToLocation);
    }
}
