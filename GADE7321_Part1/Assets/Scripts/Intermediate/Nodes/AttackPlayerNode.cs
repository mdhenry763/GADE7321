using UnityEngine;
using Utils;

namespace Intermediate.Nodes
{
    public class AttackPlayerNode : Node
    {
        //Success will be when the player dropped their flag
        //Separate out if the player has flag into class to check
        private Animator enemyAnimator;
        private Transform playerTransform;
        private Transform enemyTransform;
        private float maxDistance;
        private EnemyAI enemyController;

        public AttackPlayerNode(Animator enemyAnimator, Transform playerTransform, Transform enemyTransform,
        float maxDistance, EnemyAI enemyController)
        {
            this.enemyAnimator = enemyAnimator;
            this.playerTransform = playerTransform;
            this.enemyTransform = enemyTransform;
            this.maxDistance = maxDistance;
            this.enemyController = enemyController;
        }

        public override NodeState Evaluate()
        {
            if (HelperMethods.IsDistanceLessThan(playerTransform, enemyTransform, maxDistance))
                return NodeState.Failure;
            
            //If the player has dropped the flag then success
            if (HelperMethods.IsCarryFlag(playerTransform))
            {
                return NodeState.Running;
                //enemyController.Attack;
            }
            else return NodeState.Success;
        }
    }
}