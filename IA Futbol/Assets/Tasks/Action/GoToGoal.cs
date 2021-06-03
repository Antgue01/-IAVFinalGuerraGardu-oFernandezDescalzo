﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

[TaskCategory("IAFutbol")]
[TaskDescription("Vamos hacia la portería")]
public class GoToGoal : Action
{
    NavMeshAgent navMeshAgent;
    Transform goalZone;
    Vector3 target;
    public override void OnAwake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        goalZone = GetComponent<FootBallPlayer>().getGoalZone().transform;
        target = transform.position + (goalZone.position - transform.position).normalized;

    }

    /// <summary>
    /// Allow pathfinding to resume.
    /// </summary>
    public override void OnStart()
    {
        navMeshAgent.isStopped = false;
        SetDestination(target);
    }

    // Seek the destination. Return success once the agent has reached the destination.
    // Return running if the agent hasn't reached the destination yet
    public override TaskStatus OnUpdate()
    {
        if (HasArrived())
        {
            return TaskStatus.Success;
        }

        SetDestination(target);

        return TaskStatus.Running;
    }



    /// <summary>
    /// Set a new pathfinding destination.
    /// </summary>
    /// <param name="destination">The destination to set.</param>
    /// <returns>True if the destination is valid.</returns>
    private bool SetDestination(Vector3 destination)
    {
        navMeshAgent.isStopped = false;
        return navMeshAgent.SetDestination(destination);
    }

    /// <summary>
    /// Has the agent arrived at the destination?
    /// </summary>
    /// <returns>True if the agent has arrived at the destination.</returns>
    private bool HasArrived()
    {
        // The path hasn't been computed yet if the path is pending.
        float remainingDistance;
        if (navMeshAgent.pathPending)
        {
            remainingDistance = float.PositiveInfinity;
        }
        else
        {
            remainingDistance = navMeshAgent.remainingDistance;
        }

        return remainingDistance <= 0;
    }

    /// <summary>
    /// Stop pathfinding.
    /// </summary>
    private void Stop()
    {
        if (navMeshAgent.hasPath)
        {
            navMeshAgent.isStopped = true;
        }
    }

    /// <summary>
    /// The task has ended. Stop moving.
    /// </summary>
    public override void OnEnd()
    {
        Stop();
    }

    /// <summary>
    /// The behavior tree has ended. Stop moving.
    /// </summary>
    public override void OnBehaviorComplete()
    {
        Stop();
    }


}
