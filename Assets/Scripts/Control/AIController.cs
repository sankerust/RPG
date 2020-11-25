﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;

namespace RPG.Control
{
  public class AIController : MonoBehaviour
  {
    [SerializeField] float chaseDistance = 5f;
    [SerializeField] float suspicionTime = 3f;
    [SerializeField] float waypointTolerance = 1f;
    [SerializeField] PatrolPath patrolPath;

    GameObject player;
    Fighter fighter;
    Mover mover;
    Health health;
    ActionScheduler actionScheduler;

    Vector3 guardPosition;
    float timeSinceLastSawPlayer = Mathf.Infinity;

    int currentWaypointIndex = 0;


    private void Start() {
      player = GameObject.FindWithTag("Player");
      fighter = GetComponent<Fighter>();
      health = GetComponent<Health>();
      mover = GetComponent<Mover>();
      actionScheduler = GetComponent<ActionScheduler>();

      guardPosition = transform.position;
    }

    private void Update() {
      if (health.IsDead()) { return; }
      
      if (InAttackRange(player) && fighter.CanAttack(player))
      {
        timeSinceLastSawPlayer = 0;
        AttackBehaviour();
      }

      // suspicion state
      else if (timeSinceLastSawPlayer < suspicionTime)
      {
        SuspicionBehaviour();
      }

      else
      {
        PatrolBehaviour();
      }

      timeSinceLastSawPlayer += Time.deltaTime;
    }

    private void PatrolBehaviour()
    {
      Vector3 nextPosition = guardPosition;

      if (patrolPath != null) {
        if (AtWaypoint()) {
          CycleWaypoint();
        }
        nextPosition = GetCurrentWaypoint();
      }
      mover.StartMoveAction(nextPosition);
    }

    private bool AtWaypoint()
    {
      float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
      return distanceToWaypoint < waypointTolerance;
    }

    private Vector3 GetCurrentWaypoint()
    {
      return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    private void CycleWaypoint()
    {
      currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    private void SuspicionBehaviour()
    {
      actionScheduler.CancelCurrentAction();
    }

    private void AttackBehaviour()
    {
      fighter.Attack(player);
    }

    private bool InAttackRange(GameObject player) {
      float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
      return distanceToPlayer < chaseDistance;
    }

    // Called by Unity
    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
  }
}
