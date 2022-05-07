using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNode : NavigationNode
{
    const float minimumUpdateDistance = 2f;

    [Tooltip("The target GameObject's Tag.")]
    [SerializeField] string targetTag = "";

    [SerializeField] bool stationaryTarget = false;

    [SerializeField] float randomOffset = 1f;
    [SerializeField] bool x = false;
    [SerializeField] bool y = false;
    [SerializeField] bool z = false;

    GameObject target;
    Vector3 previousTargetPosition;

    protected override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        base.OnStart();
        SetDestination();
    }

    protected override NodeState OnUpdate()
    {
        // If target is null
        if (!target)
        {
            return NodeState.FAILURE;
        }

        // If target is not stationary and has moved significantly
        if (!stationaryTarget && Vector3.Distance(previousTargetPosition, target.transform.position) > minimumUpdateDistance)
        {
            SetDestination();
            Debug.Log(owner.name + " is following");
        }
        
        return base.OnUpdate();
    }

    /// <summary>
    /// Calculates and sets new destination to the navMeshAgent
    /// </summary>
    void SetDestination()
    {
        Vector3 destination = CalculateDestination();
        previousTargetPosition = destination;
        SetDestination(destination);
    }

    /// <summary>
    /// Get target GameObject's current position with any applied offset.
    /// </summary>
    protected Vector3 CalculateDestination()
    {
        Vector3 destination = target.transform.position;
        return OffsetDestination(destination);
    }

    /// <summary>
    /// Offsets given position according to the set offset parameters.
    /// </summary>
    Vector3 OffsetDestination(Vector3 destination)
    {
        if (x)
        {
            destination += new Vector3(Random.Range(-randomOffset, randomOffset), 0, 0);
        }
        if (y)
        {
            destination += new Vector3(0, Random.Range(-randomOffset, randomOffset), 0);
        }
        if (z)
        {
            destination += new Vector3(0, 0, Random.Range(-randomOffset, randomOffset));
        }

        return destination;
    }
}