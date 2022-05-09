using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MannequinPart : MonoBehaviour
{
    [SerializeField] MannequinItem mannequinItem;
    public MannequinItem _mannequinItem => mannequinItem;
}
