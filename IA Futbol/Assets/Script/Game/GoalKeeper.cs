using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GoalKeeper : MonoBehaviour
{
    [SerializeField] Collider goal;
    NavMeshAgent navmesh;

    private void Start()
    {
        goal = GetComponent<Collider>();
        navmesh = GetComponent<NavMeshAgent>();
    }
    public void Catch()
    {
        int rand = Random.Range(0, 2);
        Vector3 randomPoint;
        if (rand == 0)
            randomPoint = new Vector3(transform.position.x, transform.position.y, goal.bounds.center.z + goal.bounds.extents.z);
        else
            randomPoint = new Vector3(transform.position.x, transform.position.y, goal.bounds.center.z - goal.bounds.extents.z);
        navmesh.SetDestination(randomPoint);
    }
}
