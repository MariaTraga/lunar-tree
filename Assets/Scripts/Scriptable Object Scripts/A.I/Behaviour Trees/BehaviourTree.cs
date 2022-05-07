using System;
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

    //Tree data cache
    public BlackBoard blackboard = new BlackBoard();


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

#if UNITY_EDITOR
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
#endif

    public void Traverse(Node node, System.Action<Node> visiter)
    {
        if (node)
        {
            visiter.Invoke(node);
            var children = GetChildren(node);
            children.ForEach((n) => Traverse(n, visiter));
        }
    }

    //Create a clone of the behaviour tree, in order to prevent overlapping trees and permanent SUCCESS states
    public BehaviourTree Clone()
    {
        BehaviourTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        tree.nodes = new List<Node>();
        Traverse(tree.rootNode, (n) =>
        {
            tree.nodes.Add(n);
        });

        return tree;
    }

    public void Bind(AnimalAI owner)
    {
        Traverse(rootNode, node =>
        {
            node.owner = owner;
            node.blackboard = blackboard;
        });
    }
}