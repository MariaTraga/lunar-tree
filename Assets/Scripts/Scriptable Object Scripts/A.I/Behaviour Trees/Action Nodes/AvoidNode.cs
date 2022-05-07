using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidNode : NavigationNode
{
    [Tooltip("The target GameObject's Tag.")]
    [SerializeField] string targetTag = "";
    [SerializeField] float movefactor = 2f;

    GameObject target;

    protected override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        base.OnStart();
        Vector3 destination = CalculateDestination();
        SetDestination(destination);
    }

    protected override NodeState OnUpdate()
    {
        if (!target)
        {
            return NodeState.FAILURE;
        }
        Debug.Log(owner.name + " is avoiding");
        return base.OnUpdate();
    }

    /// <summary>
    /// Calculates destination in the opposite direction of target GameObject.
    /// </summary>
    protected Vector3 CalculateDestination()
    {
        Vector3 destination;
        Vector3 ownerPosition = owner.transform.position;
        Vector3 targetPosition = target.transform.position;

        Vector3 normal = (targetPosition - ownerPosition).normalized;

        destination = ownerPosition + normal * movefactor * -1;

        return destination;
    }
}
