using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class AreaChangerCaller : MonoBehaviour
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] int spawnToIndex;
    private void OnMouseDown()
    {
        FindObjectOfType<Portal>().TransitionToNextScene(sceneToLoad, spawnToIndex);
    }
}
