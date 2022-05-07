using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationTargetNode : ActionNode
{
    [SerializeField] string targetTag = "";

    GameObject target;
    NavMeshAgent navMeshAgent;

    //Animator variables
    Animator animator;
    float posX, posY;

    protected override void OnStart()
    {
        target = GameObject.FindGameObjectWithTag(targetTag);
        navMeshAgent = owner.GetComponent<NavMeshAgent>();
        animator = owner.GetComponentInChildren<Animator>();
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        float distance = Vector2.Distance(owner.transform.position, target.transform.position);
        if (distance > navMeshAgent.stoppingDistance)
        {
            Move();
            HandleAnimation();
            return NodeState.RUNNING;
        }
        else
        {
            StopMove();
            HandleAnimation();
            return NodeState.SUCCESS;
        }
    }

    private void Move()
    {
        animator.SetBool("isWalking", true);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.transform.position);
        posX = navMeshAgent.velocity.x;
        posY = navMeshAgent.velocity.y;
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
