using System.Collections;
using System.Collections.Generic;
using BasicBehaviourTree;
using UnityEngine;

public class BehaviourTree : BasicTree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    public static float fovRange = 6f;
    public static float attackRange = 1f;

    protected override BasicNode SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            //new Sequence(new List<Node>
            //{
            //    new CheckEnemyInAttackRange(transform),
            //    new TaskAttack(transform),
            //}),
            // new Sequence(new List<Node>
            // {
            //     new CheckEnemyInFOVRange(transform),
            //     new TaskGoToTarget(transform),
            // }),
            // new TaskPatrol(transform, waypoints),
        });

        return null;
    }
}
