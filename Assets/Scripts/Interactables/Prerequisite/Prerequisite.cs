// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Prerequisite : MonoBehaviour
// {
//     //if require item is true, we'll check this collector
//     public Collector checkCollector;
//     //watch this switcher
//     public Switcher watchedSwitcher;
//     // if true, check for item instead
//     public bool requireItem;
//     // if true, block access altogether
//     public bool nodeAccess;
//     //check if prerequisite is met
//     public bool Complete
//     {
//         get
//         {
//             if (!requireItem)
//             {
//                 return watchedSwitcher.state;
//             }
//             else
//             {
//                 return GameManager.Instance.itemHeld.itemName
//                 == checkCollector.item.itemName;
//             }
//         }
//     }
// }
