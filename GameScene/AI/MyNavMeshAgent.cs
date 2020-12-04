using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class MyNavMeshAgent : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent _navMeshAgent;

    private const float speedWhatever = 2f;
    private const float distanceOkay = 3f;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _navMeshAgent.stoppingDistance = distanceOkay;
    }

    public void GoNextPoint(Vector3 destination) => _navMeshAgent.destination = destination;

    public bool IsAtDestionation()
    {
        if (!_navMeshAgent.pathPending)
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.magnitude <= speedWhatever)
                    return true;

        return false;
    }
}
