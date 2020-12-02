using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics

{
  
  public class CinematicControlremover : MonoBehaviour {
    

    private void Start() {
      GetComponent<PlayableDirector>().played += DisableControl;
      GetComponent<PlayableDirector>().stopped += EnableControl;
    }


    void DisableControl(PlayableDirector pd) {
      print("DisableControl");
    }

    void EnableControl(PlayableDirector pd) {
      print("EnableControl");
    }
  }
}
