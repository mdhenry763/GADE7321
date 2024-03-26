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
        
        
        public override NodeState Evaluate() //Run towards flag while distance is greater than 1.5f, hits collider of flag
        {
            NotifyObservers("Pick-Up Node Running", AIState.Running);
            float distance = Vector3.Distance(enemy.position, flag.position);
            if (distance > 1.5f)
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