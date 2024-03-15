using UnityEngine;
using Utils;

public class IsPlayerCarryingFlagNode : Node
{
    private Transform player;

    public IsPlayerCarryingFlagNode(Transform player)
    {
        this.player = player;
    }

    public override NodeState Evaluate()
    {
        if (HelperMethods.IsCarryFlag(player))
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}