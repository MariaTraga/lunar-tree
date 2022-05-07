using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderNode : NavigationNode
{
    [SerializeField] float maxDistance = 1f;

    protected override void OnStart()
    {
        base.OnStart();
        Vector2 destination = CalculateRandomDestination();
        SetDestination(destination);
    }

    private Vector2 CalculateRandomDestination()
    {
        Debug.Log(owner.name + " is wandering");
        Vector2 wanderWaypoint = new Vector2(UnityEngine.Random.Range(-maxDistance, maxDistance), UnityEngine.Random.Range(-maxDistance, maxDistance));
        return wanderWaypoint;
    }
}