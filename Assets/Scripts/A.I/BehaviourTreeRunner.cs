using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeRunner : MonoBehaviour
{
    [SerializeField] BehaviourTree tree;
    [SerializeField] Node rootNode;

    // Start is called before the first frame update
    void Start()
    {
        tree = tree.Clone();
    }

    // Update is called once per frame
    void Update()
    {
        tree.Update();
    }
}
