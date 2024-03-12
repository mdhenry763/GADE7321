using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class PlayerNearNode : Node
{
    private float nearDistance;
    private Transform player;
    private Transform enemy;

    public PlayerNearNode(float nearDistance, Transform player, Transform enemy)
    {
        this.nearDistance = nearDistance;
        this.player = player;
        this.enemy = enemy;
    }
    
    
    public override NodeState Evaluate()
    {
        return HelperMethods.IsDistanceLessThan(player, enemy, nearDistance)
            ? NodeState.Success
            : NodeState.Failure;
    }
}
