using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task.Nodes;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Tree : MonoBehaviour, IBTObserver
{
    private readonly List<Node> tree = new();
    private Node root = null;

    [FormerlySerializedAs("enemyAI")]
    [Header("Enemy References: ")]
    [Space]
    public EnemyAI enemyAI;
    public Transform enemyTransform;
    public NavMeshAgent enemyAgent;
    public Transform enemyFlag;
    public Transform enemyBase;
    
    [Header("Enemy Settings:")]
    [Space]
    public float evadeDistance;
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
            new PickUpFlagNode(enemyTransform, enemyAgent, enemyFlag, this),
            
        });

        return new Sequence(new List<Node>
        {
            new Inverter(new IsAICarryingFlagNode(enemyTransform)),
            captureFlagDecision
        });
        
        
    }
    
    private Node BuildAttackPlayerBranch()
    {
        Debug.Log("Attack Player");
        Node chasePlayer = BuildSequenceBranch(new List<Node>
        {
            new IsPlayerCarryingFlagNode(player),
            new CheckNearNode(nearDistance, enemyBase, enemyTransform),
            new ChasePlayerNode(player, enemyAgent, attackDistance, enemyAI)
            
        });

        Node attack = BuildSequenceBranch(new List<Node>
        {
            new CheckNearNode(nearPlayerDistance, player, enemyTransform),
            new AttackPlayerNode(player, enemyTransform, attackDistance, enemyAI)
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
            new IsAICarryingFlagNode(enemyTransform),
            new CheckNearNode(50f, player, enemyTransform),
            new EvadePlayerNode(strafeMultiplier, player, enemyAgent, evadeDistance, randomMoveEvade, enemyAI)
        });

        return EvadePlayer;
    }
    
    
    private Node BuildReturnHomeBranch()
    {
        
        Node returnHome = BuildSequenceBranch(new List<Node>
        {
            new IsAICarryingFlagNode(enemyTransform),
            new ReturnToBaseNode(enemyBase, enemyAgent)
        });
        
        Node evadeWhenClose = new Sequence(new List<Node>
        {
            new CheckNearNode(nearDistance, player, enemyTransform),
            new EvadePlayerNode(strafeMultiplier, player, enemyAgent, chaseDistance, randomMoveEvade, enemyAI)
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
            new PickUpFlagNode(enemyTransform, enemyAgent, playerFlag, enemyAI)
            
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

    public void OnAIStateChange(AIState aiState)
    {
        
    }
}

