using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class IsAICarryingFlagNode : Node
{
    private Transform enemy;

    public IsAICarryingFlagNode(Transform Enemy)
    {
        this.enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        if (HelperMethods.IsCarryFlag(enemy))
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}