using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
  public class Mover : MonoBehaviour
  {
    
    [SerializeField] Transform target;
    Animator animator;
    NavMeshAgent navMeshAgent;


    void Start()
    {
      animator = GetComponent<Animator>();
      navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
      UpdateAnimator();
    }

    public void MoveTo(Vector3 destination)
    {
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;
    }

    public void Stop() {
      navMeshAgent.isStopped = true;
    }

    private void UpdateAnimator()
    {
      Vector3 velocity = navMeshAgent.velocity;
      Vector3 localVelocity = transform.InverseTransformDirection(velocity);
      float speed = localVelocity.z;
      animator.SetFloat("forwardSpeed", speed);
    }
  }
}
