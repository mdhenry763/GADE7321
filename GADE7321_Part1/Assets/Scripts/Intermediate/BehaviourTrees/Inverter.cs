using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    protected Node node;

    public Inverter(Node node)
    {
        this.node = node;
    }

    public override NodeState Evaluate() //Iterate through all nodes
    {

        switch (node.Evaluate())
        {
            case NodeState.Running:
                _nodeState = NodeState.Running;
                break;
            case NodeState.Success:
                _nodeState = NodeState.Failure;
                //Evaluate next child
                break;
            case NodeState.Failure:
                _nodeState = NodeState.Success;
                return _nodeState;
                break;
        }
        
        return _nodeState;
    }
}
