using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsCarryingFlag : Node
{
    private Transform target;
    private Transform origin;

    public IsCarryingFlag(Transform target, Transform origin)
    {
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        //Perform a Physics.RayCastSphere to check if player has flag
        return NodeState.Failure;
    }
}
