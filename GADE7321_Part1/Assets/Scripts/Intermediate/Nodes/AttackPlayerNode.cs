using System;
using UnityEngine;
using Utils;

namespace Task.Nodes
{
    [Serializable]
    public class AttackPlayerNode : Node
    {
        //Success will be when the player dropped their flag
        //Separate out if the player has flag into class to check
        private Transform playerTransform;
        private Transform enemyTransform;
        private float maxDistance;

        public AttackPlayerNode( Transform playerTransform, Transform enemyTransform,
        float maxDistance, IBTObserver observer)
        {
            this.playerTransform = playerTransform;
            this.enemyTransform = enemyTransform;
            this.maxDistance = maxDistance;
            AddObserver(observer);
        }

        public override NodeState Evaluate()
        {
            NotifyObservers("Is Attacking", AIState.Attacking);;
            Debug.Log("Attack Player");
            
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