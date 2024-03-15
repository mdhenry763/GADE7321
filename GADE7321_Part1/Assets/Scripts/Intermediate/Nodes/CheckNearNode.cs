using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class CheckNearNode : Node
{
    private float nearDistance;
    private Transform target;
    private Transform enemy;

    public CheckNearNode(float nearDistance, Transform target, Transform enemy)
    {
        this.nearDistance = nearDistance;
        this.target = target;
        this.enemy = enemy;
    }
    
    
    public override NodeState Evaluate()
    {
        return HelperMethods.IsDistanceLessThan(target, enemy, nearDistance)
            ? NodeState.Success
            : NodeState.Failure;
    }
}
