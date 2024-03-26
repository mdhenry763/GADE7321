using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    protected List<Node> nodes = new();
    
    /// <summary>
    /// Selector
    /// If a child behaviour succeeds then the Selector succeeds
    /// </summary>
    /// <param name="nodes"></param>

    public Selector(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate() //Iterate through all nodes
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    _nodeState = NodeState.Running;
                    return _nodeState;
                    break;
                case NodeState.Success:
                    _nodeState = NodeState.Success;
                    return _nodeState;
                    //Evaluate next child
                case NodeState.Failure:
                    break;
                default:
                    break;
            }
        }

        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}
