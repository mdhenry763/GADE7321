using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task.Nodes;
using UnityEngine.AI;

public class Tree : MonoBehaviour, IBTObserver
{
    private readonly List<Node> tree = new();
    private Node root = null;

    [Header("Enemy References: ")]
    [Space]
    public Transform enemyAI;
    public NavMeshAgent enemyAgent;
    public Transform enemyFlag;
    public Transform enemyBase;
    
    [Header("Enemy Settings:")]
    [Space]
    public float nearDistance;
    public float attackDistance;
    public float strafeMultiplier;
    public float chaseDistance;
    public Vector2 randomMoveEvade = new Vector2(-20, 20);

    [Header("Player References:")]
    [Space]
    public Transform playerFlag;
    public Transform player;
    public Transform playerBase;
    
    [Header("Player Settings")]
    [Space]
    public float distanceOutBase;
    public float nearPlayerDistance;
    
    private void Start()
    {
        BuildTree();
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

    //Capture Flag
    private Node BuildCaptureFlagBranch()
    {
        Node captureFlagDecision = new Selector(new List<Node>
        {
            BuildAttackPlayerBranch(),
            new PickUpFlagNode(enemyAI, enemyAgent, enemyFlag, this),
            
        });

        return new Sequence(new List<Node>
        {
            new Inverter(new IsAICarryingFlagNode(enemyAI)),
            captureFlagDecision
        });
        
        
    }
    
    private Node BuildAttackPlayerBranch()
    {
        Debug.Log("Attack Player");
        Node chasePlayer = BuildSequenceBranch(new List<Node>
        {
            new IsPlayerCarryingFlagNode(player),
            new CheckNearNode(nearDistance, enemyBase, enemyAI),
            new ChasePlayerNode(player, enemyAgent, attackDistance, this)
            
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

        return attackPlayer;
    }

    private Node BuildEvadePlayerBranch()
    {
        Node EvadePlayer = new Sequence(new List<Node>
        {
            new IsAICarryingFlagNode(enemyAI),
            new CheckNearNode(50f, player, enemyAI),
            new EvadePlayerNode(5, player, enemyAgent, 3, randomMoveEvade)
        });

        return EvadePlayer;
    }
    
    
    private Node BuildReturnHomeBranch()
    {
        
        Node returnHome = BuildSequenceBranch(new List<Node>
        {
            new IsAICarryingFlagNode(enemyAI),
            new ReturnToBaseNode(enemyBase, enemyAgent)
        });
        
        Node evadeWhenClose = new Sequence(new List<Node>
        {
            new CheckNearNode(nearDistance, player, enemyAI),
            new EvadePlayerNode(strafeMultiplier, player, enemyAgent, chaseDistance, randomMoveEvade)
        });
        
        Node returnOrEvade = new Selector(new List<Node>
        {
            evadeWhenClose, 
            returnHome 
        });

        return returnOrEvade;
    }
    
    private Node ResetOpponentFlag()
    {
        Node resetPlayerFlag = BuildSequenceBranch(new List<Node>
        {
            new PlayerDroppedFlagNode(distanceOutBase, playerBase, playerFlag, player),
            new PickUpFlagNode(enemyAI, enemyAgent, playerFlag, this)
            
        });

        return resetPlayerFlag;
    }

    private void BuildTree()
    {
        var captureFlag = BuildCaptureFlagBranch();
        var attackPlayer = BuildAttackPlayerBranch();
        var returnToBase = BuildReturnHomeBranch();
        var resetPlayerFlag = BuildReturnHomeBranch();
        var evadePlayer = BuildEvadePlayerBranch();
        //Node top = BuildSequenceBranch()

        root = new Selector(new List<Node>
        {
            returnToBase,
            evadePlayer,
            captureFlag,
            attackPlayer,
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

