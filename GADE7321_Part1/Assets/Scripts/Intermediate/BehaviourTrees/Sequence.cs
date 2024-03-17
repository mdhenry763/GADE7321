using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    protected List<Node> nodes = new();

    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate() //Iterate through all nodes
    {
        bool isChildRunning = false;
        
        foreach (var nodes in nodes)
        {
            switch (nodes.Evaluate())
            {
                case NodeState.Running:
                    isChildRunning = true;
                    continue;
                case NodeState.Success:
                    //Evaluate next child
                    continue;
                case NodeState.Failure:
                    _nodeState = NodeState.Failure;
                    return _nodeState;
                default:
                    _nodeState = NodeState.Success;
                    return _nodeState;
            }
        }

        _nodeState = isChildRunning ? NodeState.Running : NodeState.Success;
        return _nodeState;
    }
}
