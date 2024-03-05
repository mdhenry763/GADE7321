using System.Collections;
using System.Collections.Generic;
using BasicBehaviourTree;
using UnityEngine;

public class BasicSequence : BasicNode
{
    public BasicSequence() : base() { }
    public BasicSequence(List<BasicNode> children) : base(children) { }

    public override BasicNodeStates Evaluate()
    {
        bool anyChildIsRunning = false;

        foreach (BasicNode node in children)
        {
            switch (node.Evaluate())
            {
                case BasicNodeStates.Failure:
                    state = BasicNodeStates.Failure;
                    return state;
                case BasicNodeStates.Success:
                    continue;
                case BasicNodeStates.Running:
                    anyChildIsRunning = true;
                    continue;
                default:
                    state = BasicNodeStates.Success;
                    return state;
            }
        }

        state = anyChildIsRunning ? BasicNodeStates.Running : BasicNodeStates.Success;
        return state;
    }
}
