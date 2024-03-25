using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Task.Nodes
{
    public class ReturnToBaseNode : Node
    {
        private Transform baseTransform;
        private NavMeshAgent agent;

        public ReturnToBaseNode(Transform baseTransform, NavMeshAgent agent, IBTObserver observer)
        {
            this.baseTransform = baseTransform;
            this.agent = agent;
            AddObserver(observer);
        }

        public override NodeState Evaluate()
        {
            Debug.Log("Return to base Node");
            agent.SetDestination(baseTransform.position);
            NotifyObservers("Return to base", AIState.Running);
            
            if (HelperMethods.IsDistanceLessThan(agent.transform, baseTransform, agent.stoppingDistance))
            {
                
                return NodeState.Success;
            }
            else return NodeState.Running;
            
            
        }
    }
}