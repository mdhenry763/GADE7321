using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Intermediate.Nodes
{
    public class ReturnToBaseNode : Node
    {
        private Transform baseTransform;
        private NavMeshAgent agent;

        public ReturnToBaseNode(Transform baseTransform, NavMeshAgent agent)
        {
            this.baseTransform = baseTransform;
            this.agent = agent;
        }

        public override NodeState Evaluate()
        {
            agent.SetDestination(baseTransform.position);

            if (HelperMethods.IsDistanceLessThan(agent.transform, baseTransform, agent.stoppingDistance))
            {
                return NodeState.Success;
            }
            else return NodeState.Running;
        }
    }
}