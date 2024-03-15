using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    protected List<Node> nodes = new();

    public Selector(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate() //Iterate through all nodes
    {
        
        foreach (var nodes in nodes)
        {
            switch (nodes.Evaluate())
            {
                case NodeState.Running:
                    _nodeState = NodeState.Running;
                    return _nodeState;
                    break;
                case NodeState.Success:
                    _nodeState = NodeState.Success;
                    return _nodeState;
                    //Evaluate next child
                    break;
                case NodeState.Failure:
                    continue;
                default:
                    continue;
            }
        }

        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}
