using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameDevTV.Saving;


namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        TransitionFader fader;
        SavingSystem savingSystem;
        [SerializeField] float fadeInTime = 1f;
        const string defaultSaveFile = "save";
        void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
            fader = FindObjectOfType<TransitionFader>();
        }
        IEnumerator Start()
        {
            fader.FadeOutImmediate();
            yield return savingSystem.LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }
        void Update()
        {
            HandleSaveFromInput();
            HandleLoadFromInput();
            HandleDeleteFromInput();
        }

        void HandleDeleteFromInput()
        {

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Debug.Log("deleting save file");
                savingSystem.Delete(defaultSaveFile);
            }

        }

        void HandleSaveFromInput()
        {
            if (Input.GetKeyDown(KeyCode.S)) Save();
        }

        void HandleLoadFromInput()
        {
            if (Input.GetKeyDown(KeyCode.L)) Load();
        }
        public void Save()
        {
            savingSystem.Save(defaultSaveFile);
        }

        public void Load()
        {
            savingSystem.Load(defaultSaveFile);
        }
    }
}

