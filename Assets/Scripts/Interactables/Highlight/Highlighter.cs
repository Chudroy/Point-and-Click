// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using cakeslice;

// public class Highlighter : MonoBehaviour
// {
//     [SerializeField] GameObject modelToHighlight;
//     Outline outline;

//     void Start()
//     {
//         var inter = GetComponent<Interactable>();
//         if (inter == null)
//         {
//             Debug.LogError(name + " doesn't have an interactable component"); ;
//         }
//     }
//     void OnMouseEnter()
//     {
//         if (outline == null)
//         {
//             outline = modelToHighlight.AddComponent<Outline>();
//             return;
//         }
//         if (outline.enabled == false)
//             outline.enabled = true;

//     }
//     void OnMouseExit()
//     {
//         if (outline.enabled == true)
//             outline.enabled = false;
//     }
// }
