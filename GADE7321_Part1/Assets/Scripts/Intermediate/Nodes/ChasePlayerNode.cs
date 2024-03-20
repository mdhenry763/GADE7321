using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private float distanceToAttack;

    public ChasePlayerNode(Transform target, NavMeshAgent agent, float distanceToAttack, IBTObserver observer)
    {
        this.target = target;
        this.agent = agent;
        this.distanceToAttack = distanceToAttack;
        
        
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Chase Player");
        
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if(agent.remainingDistance < 30)
        {
            agent.SetDestination(target.position);
            return NodeState.Running;
        }
        else
        {
            return NodeState.Failure;
        }

        if (agent.remainingDistance <= distanceToAttack)
        {
            return NodeState.Success;
        }
       
    }
}
