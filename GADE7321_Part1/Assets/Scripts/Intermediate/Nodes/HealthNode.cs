using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthNode : Node
{
    private EnemyAI ai;
    private float healthThreshold;

    public HealthNode(EnemyAI ai, float threshold)
    {
        this.ai = ai;
        this.healthThreshold = threshold;
    }
    
    
    public override NodeState Evaluate()
    {
        return NodeState.Failure;
        //return ai.GetCurrentHealth() <= healthThreshold ? NodeState.Success : NodeState.Failure;
    }
}
