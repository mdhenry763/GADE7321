using UnityEngine;

namespace Intermediate.Nodes
{
    public class PlayerDroppedFlagNode : Node
    {
        private float distance;
        private Transform playerBase;
        
        public override NodeState Evaluate()
        {
            return NodeState.Running;
        }
    }
}