using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour Tree", menuName = "A.I./Behaviour Tree/New Behaviour Tree")]
public class BehaviourTree : ScriptableObject
{
    public GameObject owner;

    //Entry Node
    public Node rootNode;
    public Node.NodeState treeState = Node.NodeState.RUNNING;
    public List<Node> nodes = new List<Node>();


    //Mirrors the update function of the root node
    public Node.NodeState Update()
    {
        //When the root node returns something other than RUNNING then it will stop being updated
        if (rootNode.state == Node.NodeState.RUNNING)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }

    public Node CreateNode(System.Type type)
    {
        Node node = ScriptableObject.CreateInstance(type) as Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();
        nodes.Add(node);

        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;
    }

    public void DeleteNode(Node node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            decorator.childNode = child;
        }

        RootNode root = parent as RootNode;
        if (root)
        {
            root.childNode = child;
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            composite.childrenNodes.Add(child);
        }
    }

    public void RemoveChild(Node parent, Node child)
    {
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator)
        {
            decorator.childNode = null;
        }

        RootNode root = parent as RootNode;
        if (root)
        {
            root.childNode = null;
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            composite.childrenNodes.Remove(child);
        }
    }

    public List<Node> GetChildren(Node parent)
    {
        List<Node> children = new List<Node>();
        DecoratorNode decorator = parent as DecoratorNode;
        if (decorator && decorator.childNode != null)
        {
            children.Add(decorator.childNode);
        }

        RootNode root = parent as RootNode;
        if (root && root.childNode != null)
        {
            children.Add(root.childNode);
        }

        CompositeNode composite = parent as CompositeNode;
        if (composite)
        {
            return composite.childrenNodes;
        }

        return children;
    }

    //Create a clone of the behaviour tree, in order to prevent overlapping trees and permanent SUCCESS states
    public BehaviourTree Clone(GameObject owner)
    {
        BehaviourTree behaviourTree = Instantiate(this);
        behaviourTree.rootNode = behaviourTree.rootNode.Clone(owner);
        behaviourTree.owner = owner;
        return behaviourTree;
    }
}