using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NearTargetNode : ActionNode
{
    [SerializeField] string targetTag = "";
    [SerializeField] float stoppingDistance = 1f;

    GameObject target;
    NavMeshAgent navMeshAgent;

    protected override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        navMeshAgent = owner.GetComponent<NavMeshAgent>();
    }

    protected override void OnStop()
    {
        
    }

    protected override NodeState OnUpdate()
    {
        if (!target)
        {
            return NodeState.FAILURE;
        }
        if (Vector2.Distance(owner.transform.position, target.transform.position) < stoppingDistance)
        {
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}
