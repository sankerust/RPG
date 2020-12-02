using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace RPG.Cinematics
{
  public class CinematicTrigger : MonoBehaviour
  {
    bool wasPlayed = false;
    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "Player" && !wasPlayed) {
        GetComponent<PlayableDirector>().Play();
        wasPlayed = true;
      }
      
    }
  }
}

