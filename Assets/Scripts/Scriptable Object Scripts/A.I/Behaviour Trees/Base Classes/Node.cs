using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{

    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    public string Name;

    [HideInInspector] public NodeState state = NodeState.RUNNING;
    //Signifies if the node ever executed
    [HideInInspector] public bool started = false;
    [HideInInspector] protected GameObject owner;

    public NodeState Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        //Store the state on the node itself
        state = OnUpdate();

        //If the node finished
        if (state == NodeState.FAILURE || state == NodeState.SUCCESS)
        {
            OnStop();
            started = false;
        }

        return state;
    }

    public virtual Node Clone(GameObject owner)
    {
        Node node = Instantiate(this);
        node.owner = owner;
        return node;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract NodeState OnUpdate();
}