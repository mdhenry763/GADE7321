using UnityEngine;

namespace Intermediate.Nodes
{
    public class AttackPlayerNode : Node
    {
        //Success will be when the player dropped their flag
        //Separate out if the player has flag into class to check
        private Animator enemyAnimator;
        private Transform playerTransform;

        public AttackPlayerNode(Animator enemyAnimator, Transform playerTransform)
        {
            this.enemyAnimator = enemyAnimator;
            this.playerTransform = playerTransform;
        }

        public override NodeState Evaluate()
        {
            if (CheckFlag.IsCarryFlag(playerTransform)) return NodeState.Running;
            else return NodeState.Success;
        }
    }
}