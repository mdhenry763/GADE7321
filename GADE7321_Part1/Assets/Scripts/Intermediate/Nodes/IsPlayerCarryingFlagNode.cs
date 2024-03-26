using UnityEngine;
using Utils;

public class IsPlayerCarryingFlagNode : Node
{
    private Transform player;

    public IsPlayerCarryingFlagNode(Transform player)
    {
        this.player = player;
        
    }

    public override NodeState Evaluate() //Check if player is carrying flag
    {
        Debug.Log("Is Player Carrying Flag");
        
        if (HelperMethods.IsCarryFlag(player))
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}