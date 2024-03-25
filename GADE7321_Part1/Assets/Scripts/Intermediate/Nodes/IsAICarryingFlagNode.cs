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
        
        if (HelperMethods.IsCarryFlag(enemy))
        {
            Debug.Log("Is Carrying Flag Success");
            return NodeState.Success;
        }
        else
        {
            return NodeState.Failure;
        }
        
    }
}