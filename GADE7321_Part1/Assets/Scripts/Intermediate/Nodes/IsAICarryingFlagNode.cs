using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class IsAICarryingFlagNode : Node
{
    private Transform enemy;

    public IsAICarryingFlagNode(Transform enemy)
    {
        this.enemy = enemy;
        
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Is Carrying Flag");
        if (HelperMethods.IsCarryFlag(enemy))
        {
            Debug.Log("Is Carrying Flag Success");
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}