using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu()]
public class BehaviorTree : ScriptableObject
{
    [HideInInspector] public UserNode rootNode;
    [HideInInspector] UserNode.State treeState = UserNode.State.Running;

    [HideInInspector] public List<UserNode> nodes = new List<UserNode>();
    [SerializeField] public Blackboard blackboard;

    public UserNode.State Update()
    {
        if (rootNode.state == UserNode.State.Running)
        {
            treeState = rootNode.Update();
        }
        return treeState;
    }

#if (UNITY_EDITOR)
    public UserNode createNode(System.Type type)
    {
        UserNode node = ScriptableObject.CreateInstance(type) as UserNode;
        node.nodeName = type.Name.Replace("Node", "");
        node.guid = GUID.Generate().ToString();

        //Undo.RecordObject(this, "Behavior Tree (CreateNode)");
        nodes.Add(node);

        if (!Application.isPlaying)
            AssetDatabase.AddObjectToAsset(node, this);


        //Undo.RegisterCreatedObjectUndo(node, "Behavior Tree (CreateNode)");
        AssetDatabase.SaveAssets();
        return node;

    }

    public void DeleteNode(UserNode node)
    {
        //Undo.RecordObject(this, "Behavior Tree (DeleteNode)");
        nodes.Remove(node);
        //Undo.RegisterCreatedObjectUndo(node, "Behavior Tree (DeleteNode)");

        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }

    public void AddChild(UserNode parent, UserNode child)
    {
        if (parent.children == null)
            parent.children = new List<UserNode>();

        //Undo.RecordObject(parent, "Behavior Tree (AddChild)");
        parent.children.Add(child);
        EditorUtility.SetDirty(parent);
    }

    public void RemoveChild(UserNode parent, UserNode child)
    {
        if (parent.children == null)
            parent.children = new List<UserNode>();

        //Undo.RecordObject(parent, "Behavior Tree (RemoveChild)");
        parent.children.Remove(child);
        EditorUtility.SetDirty(parent);
    }
#endif

    public List<UserNode> GetChildren(UserNode parent)
    {
        if (parent.children == null)
            parent.children = new List<UserNode>();

        List<UserNode> children = new List<UserNode>();
        children.AddRange(parent.children);
        return children;
    }

    public void Traverse(UserNode node, System.Action<UserNode> visitor)
    {
        if (node)
        {
            visitor.Invoke(node);
            var children = GetChildren(node);
            children.ForEach((n) => Traverse(n, visitor));
        }
    }

    public BehaviorTree Clone()
    {
        BehaviorTree tree = Instantiate(this);
        tree.rootNode = tree.rootNode.Clone();
        tree.nodes = new List<UserNode>();

        Traverse(tree.rootNode, (n) =>
        {
            tree.nodes.Add(n);
        });

        return tree;
    }

    public void BindBlackboard()
    {
        Traverse(rootNode, (n) => n.board = blackboard);
    }
}
