using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private float distance;

    public ChasePlayerNode(Transform target, NavMeshAgent agent, float distanceToAttack)
    {
        this.target = target;
        this.agent = agent;
        this.distance = distanceToAttack;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if(agent.remainingDistance < distance)
        {
            agent.SetDestination(target.position);
            return NodeState.Running;
        }
        else
        {
            return NodeState.Success;
        }
       
    }
}
