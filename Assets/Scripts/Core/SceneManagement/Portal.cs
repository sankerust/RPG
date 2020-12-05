﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
public class Portal : MonoBehaviour
  {
    [SerializeField] int portalSceneIndex;
    private void OnTriggerEnter(Collider other) {
      if (other.gameObject.tag == "Player") {
        SceneManager.LoadScene(portalSceneIndex);
      }
    }
  }
}

