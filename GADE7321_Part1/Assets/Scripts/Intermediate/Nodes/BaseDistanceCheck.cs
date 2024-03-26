using UnityEngine;
using Utils;

namespace Task.Nodes
{
    public class BaseDistanceCheck : Node
    {
        private float distance;
        private Transform enemyBase;
        private Transform playerFlag;
        private Transform player;

        public BaseDistanceCheck(float distance, Transform enemyBase, Transform playerFlag, Transform player)
        {
            this.distance = distance;
            this.enemyBase = enemyBase;
            this.playerFlag = playerFlag;
            this.player = player;
            
            
        }

        public override NodeState Evaluate() //Check if base is within range
        {
            Debug.Log("Is Dropped Flag");
            //Distance check to see if flag is still in base
            if (HelperMethods.IsDistanceLessThan(enemyBase, playerFlag, distance)) 
                return NodeState.Failure;
            else
            {
                return NodeState.Success;
            }
            
            

        }
    }
}