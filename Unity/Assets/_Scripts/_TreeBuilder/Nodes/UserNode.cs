using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserNode : ScriptableObject
{
    [HideInInspector] public Blackboard board;

    [HideInInspector] public int childIndex = 0;
    [HideInInspector] public List<UserNode> children;

    public string nodeName;

    public enum State { Running, Failure, Success };

    [HideInInspector] public State state = State.Running;
    [HideInInspector] public bool started = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector3 position;

    public virtual State Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if (state == State.Failure || state == State.Success)
        {
            OnStop();
            started = false;
        }

        return state;
    }

    public virtual UserNode Clone()
    {
        UserNode node = Instantiate(this);
        node.children = children.ConvertAll(c => c.Clone());
        return node;
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();
}
