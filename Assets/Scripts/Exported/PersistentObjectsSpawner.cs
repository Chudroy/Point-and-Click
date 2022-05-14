using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Core
{
    public class PersistentObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab;
        static bool hasSpawned = false;
        void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistentObjects();
        }

        private void SpawnPersistentObjects()
        {
            var persistentObjects = Instantiate(persistentObjectPrefab, Vector3.zero, Quaternion.identity);
            DontDestroyOnLoad(persistentObjects);
            hasSpawned = true;
        }
    }
}
