using System.Collections;
using System.Collections.Generic;
using BasicBehaviourTree;
using UnityEngine;

public class BasicSelector : BasicNode
{
    public BasicSelector() : base() { }
    public BasicSelector(List<BasicNode> children) : base(children) { }

    public override BasicNodeStates Evaluate()
    {
        foreach (BasicNode node in children)
        {
            switch (node.Evaluate())
            {
                case BasicNodeStates.Failure:
                    continue;
                case BasicNodeStates.Success:
                    state = BasicNodeStates.Success;
                    return state;
                case BasicNodeStates.Running:
                    state = BasicNodeStates.Running;
                    return state;
                default:
                    continue;
            }
        }

        state = BasicNodeStates.Failure;
        return state;
    }
}
