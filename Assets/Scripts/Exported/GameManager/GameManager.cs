using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    public CameraRig cameraRig;
    public Viewer3D viewer3D;
    public Viewer2D viewer2D;
    public Inventory inventory;
    public SceneLoader sceneLoader;

    public override void Awake()
    {
        base.Awake();
    }
}
