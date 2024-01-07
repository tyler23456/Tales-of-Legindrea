using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Runner : MonoBehaviour
{
    [SerializeField] public BehaviorTree tree;

    protected void Awake()
    {
        tree = tree.Clone();
        tree.blackboard = new Blackboard(gameObject);
        tree.BindBlackboard();
    }

    protected void Start()
    {        
        tree.rootNode.state = UserNode.State.Success;
    }

    protected void Update()
    {
        tree.rootNode.state = UserNode.State.Running;
        tree.Update();
    }
}
