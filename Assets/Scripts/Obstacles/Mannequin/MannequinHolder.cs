using System.Collections;
using System.Collections.Generic;
using GameDevTV.Inventories;
using UnityEngine;

public class MannequinHolder : Obstacle
{

    public override void Resolve(Tool tool)
    {
        MannequinItem mannequinItem = tool as MannequinItem;

        foreach (MannequinPart mannequinPart in GetComponentsInChildren<MannequinPart>(true))
        {
            if (mannequinPart._mannequinItem == mannequinItem)
            {
                Debug.Log("activating");
                if (!mannequinPart.gameObject.activeInHierarchy) { mannequinPart.gameObject.SetActive(true); }
            }
        }


    }

    public override void FailTry()
    {
        base.FailTry();
    }
}
