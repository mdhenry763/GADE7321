using UnityEngine;
using Utils;

namespace Task.Nodes
{
    public class PlayerDroppedFlagNode : Node
    {
        private float distance;
        private Transform playerBase;
        private Transform playerFlag;
        private Transform player;

        public PlayerDroppedFlagNode(float distance, Transform playerBase, Transform playerFlag, Transform player)
        {
            this.distance = distance;
            this.playerBase = playerBase;
            this.playerFlag = playerFlag;
            this.player = player;
        }

        public override NodeState Evaluate()
        {
            if (HelperMethods.IsDistanceLessThan(playerBase, playerFlag, distance)) 
                return NodeState.Failure;
            else
            {
                if (HelperMethods.IsCarryFlag(player))
                {
                    return NodeState.Success;
                }

                return NodeState.Failure;
            }

        }
    }
}