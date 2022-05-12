using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableObject", menuName = "ScriptableObjects/InteractableObject")]
public class InteractableObject : ScriptableObject
{
    public string objectName;
    public string objectExamineText;
}
