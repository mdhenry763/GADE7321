using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Task.Nodes
{
    public class PickUpFlagNode : Node
    {
        private Transform enemy;
        private NavMeshAgent enemyAgent;
        private Transform flag;
        
        public PickUpFlagNode(Transform enemy, NavMeshAgent enemyAgent, Transform flag, IBTObserver ibtObserver)
        {
            this.enemy = enemy;
            this.enemyAgent = enemyAgent;
            this.flag = flag;
            AddObserver(ibtObserver);
            NotifyObservers("Pick-Up Node Running", AIState.Running);
        }
        
        
        public override NodeState Evaluate()
        {
            NotifyObservers("Pick-Up Node Running", AIState.Running);
            float distance = Vector3.Distance(enemy.position, flag.position);
            if (distance > 2.5f)
            {
                enemyAgent.SetDestination(flag.position);
                return NodeState.Running;
            }
            else
            {
                return NodeState.Success;
            }
            
            
            
        }
    }
}