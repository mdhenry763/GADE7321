using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task.Nodes;
using UnityEngine.AI;

public class Tree : MonoBehaviour, IObserver
{
    private readonly List<Node> tree = new();
    private Node root = null;

    public Transform enemyAI;
    public NavMeshAgent enemyAgent;
    public Transform enemyFlag;
    public float nearDistance;
    public float attackDistance;
    public float strafeMultiplier;
    public float chaseDistance;
    public Vector2 randomMoveEvade = new Vector2(-20, 20);
    public Transform enemyBase;

    public Transform playerFlag;
    public Transform player;
    public float distanceOutBase;
    public Transform playerBase;
    public float nearPlayerDistance;

    private Dictionary<Nodes, Node> leafNodes = new Dictionary<Nodes, Node>();

    private void Start()
    {
        BuildTree();
    }

    void SetupLeafNodes()
    {
        leafNodes.Add(Nodes.PickUpFlag, new PickUpFlagNode(enemyAI, enemyAgent, enemyFlag, this));
        leafNodes.Add(Nodes.IsAICarryingFlag, new IsAICarryingFlagNode(enemyAI));
        leafNodes.Add(Nodes.IsPlayerCarryingFlag, new IsPlayerCarryingFlagNode(player));
        leafNodes.Add(Nodes.BaseDistanceCheck, new IsPlayerCarryingFlagNode(player));
        
    }
    


    public Node BuildSequenceBranch(List<Node> children)
    {
        Node sequence = new Sequence(children);
        tree.Add(sequence);
        return sequence;
    }

    public Node BuildSelectorBranch(List<Node> children)
    {
        Node selector = new Selector(children);
        tree.Add(selector);
        return selector;
    }

    public void BuildTree()
    {
        Node captureFlag = BuildSelectorBranch(new List<Node>
        {
            new IsAICarryingFlagNode(enemyAI),
            new PickUpFlagNode(enemyAI, enemyAgent, enemyFlag, this),
        });

        Node resetPlayerFlag = BuildSequenceBranch(new List<Node>
        {
            new PlayerDroppedFlagNode(distanceOutBase, playerBase, playerFlag, player),
            new PickUpFlagNode(enemyAI, enemyAgent, playerFlag, this)
            
        });

        Node chasePlayer = BuildSequenceBranch(new List<Node>
        {
            new IsPlayerCarryingFlagNode(player),
            new CheckNearNode(nearDistance, enemyBase, enemyAI),
            
        });

        Node attack = BuildSequenceBranch(new List<Node>
        {
            new CheckNearNode(nearPlayerDistance, player, enemyAI),
            new AttackPlayerNode(player, enemyAI, attackDistance)
        });
        Node attackPlayer = BuildSequenceBranch(new List<Node>
        {
            chasePlayer,
            attack
        });
        Node returnHome = BuildSequenceBranch(new List<Node>
        {
            new IsAICarryingFlagNode(enemyAI),
            new ReturnToBaseNode(enemyBase, enemyAgent)
        });
        
        Node evadePlayer = BuildSequenceBranch(new List<Node>
        {
            new CheckNearNode(nearDistance, player, enemyAI),
            new IsPlayerCarryingFlagNode(player),
            new EvadePlayerNode(strafeMultiplier, player, enemyAgent, chaseDistance, randomMoveEvade)
        });
        
        Node returnToBase = BuildSequenceBranch(new List<Node>
        {
            evadePlayer,
            returnHome
        });
        

        //Node top = BuildSequenceBranch()

        root = new Selector(new List<Node>
        {
            captureFlag,
            attackPlayer,
            returnToBase,
            resetPlayerFlag
        });
    }

    private void Update()
    {
        root.Evaluate();
    }

    public void OnNotify(string name)
    {
        Debug.Log($"Entering Node: {name}");
    }
}

public enum Nodes
{
    IsPlayerCarryingFlag,
    PickUpFlag,
    IsAICarryingFlag,
    BaseDistanceCheck,
    ChasePlayer,
    AttackPlayer,
    CheckNear,
    EvadePlayer,
    ReturnHome,
    PlayerDroppedFlag,
}
