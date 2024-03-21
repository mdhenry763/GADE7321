using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Task.Nodes
{
    public class ReturnToBaseNode : Node
    {
        private Transform baseTransform;
        private NavMeshAgent agent;

        public ReturnToBaseNode(Transform baseTransform, NavMeshAgent agent)
        {
            this.baseTransform = baseTransform;
            this.agent = agent;
            NotifyObservers("Return to base", AIState.Running);
        }

        public override NodeState Evaluate()
        {
            Debug.Log("Return to base Node");
            agent.SetDestination(baseTransform.position);

            if (HelperMethods.IsDistanceLessThan(agent.transform, baseTransform, agent.stoppingDistance))
            {
                return NodeState.Success;
            }
            else return NodeState.Running;
        }
    }
}