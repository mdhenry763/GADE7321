using UnityEngine;
using UnityEngine.AI;

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
            NotifyObservers("Entering pick-Up flag");
        }
        
        
        public override NodeState Evaluate()
        {
            
            float distance = Vector3.Distance(enemy.position, flag.position);
            if (distance > 0.2f)
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