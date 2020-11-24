using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance = 5f;
    GameObject player;

    private void Start() {
      player = GameObject.FindWithTag("Player");
    }

    private void Update() {
      
      if (DistanceToPlayer() < chaseDistance) {
        print($"{gameObject.name} is gon chase ya");
        GetComponent<NavMeshAgent>().SetDestination(player.transform.position);
      }
    }

    private float DistanceToPlayer() {
      return Vector3.Distance(transform.position, player.transform.position);
    }
  }
}

