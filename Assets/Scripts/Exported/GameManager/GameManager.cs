using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public class GameManager : GenericSingletonClass<GameManager>
{
    public SceneLoader sceneLoader;

    public override void Awake()
    {
        base.Awake();
    }
}
