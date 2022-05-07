using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalAI : MonoBehaviour
{
    [Header("Animal Behaviour Tree")]
    [SerializeField] BehaviourTree behaviourTree;
    [SerializeField] Node rootNode;

    [Header("NavMesh Variables")]
    [SerializeField] NavMeshAgent navMeshAgent;

    private void Awake()
    {
        behaviourTree = behaviourTree.Clone(this.gameObject);
    }

    void Start()
    {
        //behaviourTree = behaviourTree.Clone(this.gameObject);
        FixRotationFor2D();
    }

    void Update()
    {
        behaviourTree.Update();
    }

    void FixRotationFor2D()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }
}
