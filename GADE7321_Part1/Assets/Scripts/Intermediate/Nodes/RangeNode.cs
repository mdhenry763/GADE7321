using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float rangeDistance;
    private Transform target;
    private Transform origin;

    public RangeNode(float rangeDistance, Transform target, Transform origin)
    {
        this.rangeDistance = rangeDistance;
        this.target = target;
        this.origin = origin;
    }
    
    
    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, origin.position);
        return distance <= rangeDistance ? NodeState.Success : NodeState.Failure;
    }
}
