using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
public class Portal : MonoBehaviour
  {
    enum DestinationIdentifier {
      A, B, C, D, E
    }

    [SerializeField] int portalSceneIndex;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destination;
    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "Player") {
        StartCoroutine(Transition());
      }
    }

    private IEnumerator Transition() {

      if (portalSceneIndex <0 ) {
        Debug.LogError("Scene to load is not set");
        yield break;
      }
      
      DontDestroyOnLoad(gameObject);
      yield return SceneManager.LoadSceneAsync(portalSceneIndex);

      Portal otherPortal = GetOtherPortal();
      UpdatePlayer(otherPortal);

      Destroy(gameObject);
    }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");

            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);

            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>()) {
              if (portal == this) { continue; };
              if (this.destination == portal.destination) {
                return portal;
              }
              
            };
            return null;
        }
    }
}

