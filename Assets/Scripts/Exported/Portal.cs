using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.SceneManagement;
using InventoryExample.Control;

namespace RPG.Core
{
    public class Portal : MonoBehaviour
    {
        [Header("Current Portal Information")]
        public int portalIndex;
        [Header("Spawn To Location Information")]
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] int spawnToIndex;
        [SerializeField] Transform playerSpawnLocation;
        private void Start()
        {
            // playerSpawnLocation.GetChild(0).gameObject.SetActive(false);
        }

        public void TransitionToNextScene(int _sceneToLoad, int _spawnToIndex)
        {
            sceneToLoad = _sceneToLoad;
            spawnToIndex = _spawnToIndex;
            StartCoroutine(Transition());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Player") return;

            // StartCoroutine(Transition());
        }

        IEnumerator Transition()
        {
            TransitionFader fader = FindObjectOfType<TransitionFader>();
            DontDestroyOnLoad(this.gameObject);

            FindObjectOfType<PlayerController>().enabled = false;
            yield return fader.FadeOut(2f);

            var savingWrapper = FindObjectOfType<SavingWrapper>();

            savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            savingWrapper.Load();

            FindObjectOfType<PlayerController>().enabled = false;

            // UpdatePlayerTransform();

            //this yield return give a chance for start functions to be called on monobehaviours before saving again...
            // yield return new WaitForSeconds(1f);

            savingWrapper.Save();

            yield return new WaitForSeconds(1f);
            fader.FadeIn(0.5f);

            FindObjectOfType<PlayerController>().enabled = true;

            //has to be last
            Destroy(gameObject);
        }

        void UpdatePlayerTransform()
        {
            var player = GameObject.FindWithTag("Player");
            Transform connectedPortalSpawnLocation = GetConnectedPortalSpawnLocation();
            player.GetComponent<NavMeshAgent>().Warp(connectedPortalSpawnLocation.position);
            player.transform.rotation = connectedPortalSpawnLocation.rotation;
        }

        Transform GetConnectedPortalSpawnLocation()
        {
            var portals = FindObjectsOfType<Portal>();
            foreach (Portal portal in portals)
            {
                if (portal == this) continue;
                if (portal.portalIndex == spawnToIndex)
                {
                    return portal.playerSpawnLocation;
                }
            }
            Debug.LogError("couldn't find connected portal");
            return null;
        }
    }
}
