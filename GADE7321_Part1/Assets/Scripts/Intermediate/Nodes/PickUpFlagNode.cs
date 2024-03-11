using UnityEngine;
using UnityEngine.AI;

namespace Intermediate.Nodes
{
    public class PickUpFlagNode : Node
    {
        private Transform enemy;
        private NavMeshAgent enemyAgent;
        private Transform target;
        
        public PickUpFlagNode(Transform enemy, NavMeshAgent enemyAgent, Transform target)
        {
            this.enemy = enemy;
            this.enemyAgent = this.enemyAgent;
            this.target = target;
        }
        
        
        public override NodeState Evaluate()
        {
            float distance = Vector3.Distance(enemy.position, target.position);
            if (distance > 0.2f)
            {
                enemyAgent.SetDestination(target.position);
                return NodeState.Running;
            }
            else
            {
                return NodeState.Success;
            }
        }
    }
}