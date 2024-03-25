using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private float distanceToAttack;
    private float maxChaseDistance = 10f;

    public ChasePlayerNode(Transform target, NavMeshAgent agent, float distanceToAttack, IBTObserver observer)
    {
        this.target = target;
        this.agent = agent;
        this.distanceToAttack = distanceToAttack;
        AddObserver(observer);
    }

    public override NodeState Evaluate()
    {
        NotifyObservers("Chasing player", AIState.Running);
        
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if (distance > maxChaseDistance)
        {
            return NodeState.Failure;
        }
        
        if(distance > distanceToAttack)
        {
            agent.SetDestination(target.position);
            return NodeState.Running;
        }
        else
        {
            return NodeState.Success;
        }

        if (agent.remainingDistance <= distanceToAttack)
        {
            return NodeState.Success;
        }
       
    }
}
