using UnityEngine;

public class IsPlayerCarryingFlagNode : Node
{
    private Transform player;

    public IsPlayerCarryingFlagNode(Transform Enemy)
    {
        this.player = player;
    }

    public override NodeState Evaluate()
    {
        if (CheckFlag.IsCarryFlag(player))
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}