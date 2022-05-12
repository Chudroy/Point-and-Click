// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ColorReactor : StateReactor
// {
//     public Color activeColor;
//     public Color inactiveColor;
//     MeshRenderer meshRenderer;

//     protected override void Awake()
//     {
//         meshRenderer = GetComponent<MeshRenderer>();

//         base.Awake();

//         React();

//     }

//     public override void React()
//     {
//         // base.React();
//         if (switcher.state)
//         {
//             meshRenderer.material.color = activeColor;
//         }
//         else
//         {
//             meshRenderer.material.color = inactiveColor;
//         }
//     }
// }
