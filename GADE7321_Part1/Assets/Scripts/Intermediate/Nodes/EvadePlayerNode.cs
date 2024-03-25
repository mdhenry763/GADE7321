using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Task.Nodes
{
    public class EvadePlayerNode: Node
    {
        //Make the enemy strafe
        private float strafeMultiplier;
        private Transform player;
        private NavMeshAgent agent;
        private float maxDistance;
        private Vector2 evadeMinMax;

        private float _timer;
        private Vector3 evadePos;
        
        public EvadePlayerNode(float strafeMultiplier, Transform player, NavMeshAgent agent, float maxDistance, 
            Vector2 evadeMinMax, IBTObserver observer)
        {
            this.strafeMultiplier = strafeMultiplier;
            this.player = player;
            this.agent = agent;
            this.maxDistance = maxDistance;
            this.evadeMinMax = evadeMinMax;

            _timer = 0;
            evadePos = GetEvadePoint();
            AddObserver(observer);
        }

        public override NodeState Evaluate()
        {
            NotifyObservers("Evade Player Node", AIState.Evading);
            if (!HelperMethods.IsDistanceLessThan(player, agent.transform, maxDistance))
            {
                return NodeState.Success;
            }
            
            evadePos = GetEvadePoint();

            agent.SetDestination(evadePos);
            
            return NodeState.Running;
            

        }

        private Vector3 GetEvadePoint()
        {
            //Could add raycast to the left right and front of the direction
            //Get the direction the player is chasing from
            Vector3 direction = player.position - agent.transform.position;
            direction.Normalize();
            //Move the enemy away from player in this direction
            evadePos = -direction * strafeMultiplier;
            if (!agent.CalculatePath(evadePos, agent.path))
            {
                evadePos += new Vector3(Random.Range(evadeMinMax.x, evadeMinMax.y), 0, Random.Range(evadeMinMax.x, evadeMinMax.y));
            }

            
            return evadePos;
        }
    }
}