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
        }
        
        
        public override NodeState Evaluate()
        {
            
            float distance = Vector3.Distance(enemy.position, flag.position);
            if (!HelperMethods.IsCarryFlag(enemy))
            {
                enemyAgent.SetDestination(flag.position);
                NotifyObservers("Pick-Up Node Running");
                return NodeState.Running;
            }
            else
            {
                NotifyObservers("Pick-Up Node Successful");
                return NodeState.Success;
            }
        }
    }
}