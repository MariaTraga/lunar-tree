using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationNode : ActionNode
{
    const float distanceEpsilon = 0.1f;

    [SerializeField] float stoppingDistance = 1f;
    Vector3 destination;
    NavMeshAgent navMeshAgent;
    float remainingDistance;
    bool destinationChangedThisFrame = true;

    //Animator variables
    Animator animator;
    float posX, posY;

    protected override void OnStart()
    {
        Init();
    }

    protected override void OnStop()
    {
        StopMove();
    }

    protected override NodeState OnUpdate()
    {
        float remainingDistance = Vector3.Distance(owner.transform.position, destination);

        // If agent reached destination or agent stopped moving
        if (remainingDistance < stoppingDistance || (!destinationChangedThisFrame && navMeshAgent.isPathStale && Mathf.Abs(remainingDistance - this.remainingDistance) < distanceEpsilon))
        {
            // Stop
            StopMove();
            HandleAnimation();
            this.remainingDistance = float.MaxValue;
            return NodeState.SUCCESS;
        }
        else
        {
            destinationChangedThisFrame = false;

            // Continue
            HandleAnimation();
            this.remainingDistance = remainingDistance;
            return NodeState.RUNNING;
        }
    }

    protected void Init()
    {
        this.remainingDistance = float.MaxValue;
        navMeshAgent = owner.GetComponent<NavMeshAgent>();
        animator = owner.GetComponentInChildren<Animator>();
    }

    protected void SetDestination(Vector3 destination)
    {
        destinationChangedThisFrame = true;
        this.destination = destination;
        Move();
        HandleAnimation();
    }

    private void Move()
    {
        animator.SetBool("isWalking", true);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(destination);
        Vector3 normal = (destination - owner.transform.position).normalized;
        posX = normal.x;
        posY = normal.y;
    }

    private void StopMove()
    {
        animator.SetBool("isWalking", false);
        navMeshAgent.isStopped = true;
    }

    public void HandleAnimation()
    {
        animator.SetFloat("walkPositionX", posX);
        animator.SetFloat("walkPositionY", posY);
        animator.SetFloat("idlePositionX", posX);
        animator.SetFloat("idlePositionY", posY);
    }
}
