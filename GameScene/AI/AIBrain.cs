using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyNavMeshAgent))]
public class AIBrain : MonoBehaviour
{
    private List<Transform> waypoints = new List<Transform>();

    private MyNavMeshAgent _myNavMeshAgent;
    private Health health;
    private int waypointIndex = 0;
    private void Awake()
    {
        health = GetComponent<Health>();
        _myNavMeshAgent = GetComponent<MyNavMeshAgent>();
        GetLevelWaypoints();
    }

    private void OnEnable() => health.DieEvent += StopWalking;
    

    private void OnDisable() => health.DieEvent -= StopWalking;
    

    private void Update()
    {
        if (_myNavMeshAgent.IsAtDestionation())
            GoNextWayPoint();
    }

    void GoNextWayPoint()
    {
        _myNavMeshAgent.GoNextPoint(waypoints[waypointIndex].position);
        waypointIndex++;
        if (waypointIndex == waypoints.Count) waypointIndex -= 1;
    }

    void GetLevelWaypoints() => waypoints = GameObject.FindObjectOfType<Level>().waypoints;

    void StopWalking() => _myNavMeshAgent._navMeshAgent.isStopped = true;
    
}
