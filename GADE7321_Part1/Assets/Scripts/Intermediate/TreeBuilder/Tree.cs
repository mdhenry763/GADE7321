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
            BuildResetOpponentFlagBranch(),
            new PickUpFlagNode(enemyTransform, enemyAgent, enemyFlag, enemyAI),
            
            
        });

        return new Sequence(new List<Node>
        {
            new Inverter(new IsAICarryingFlagNode(enemyTransform)),
            captureFlagDecision
        });
        
        
    }
    
    private Node BuildAttackPlayerBranch()
    {

        Node attack = BuildSequenceBranch(new List<Node>
        {
            //new CheckNearNode(nearPlayerDistance, player, enemyTransform),
            new IsPlayerCarryingFlagNode(player),
            new ChasePlayerNode(player, enemyAgent, attackDistance, enemyAI),
            new AttackPlayerNode(player, enemyTransform, attackDistance, enemyAI)
        });

        return attack;
    }

    private Node BuildEvadePlayerBranch()
    {
        Node EvadePlayer = new Sequence(new List<Node>
        {
            new IsAICarryingFlagNode(enemyTransform),
            new CheckNearNode(nearDistance, player, enemyTransform),
            new EvadePlayerNode(strafeMultiplier, player, enemyAgent, evadeDistance, randomMoveEvade, enemyAI)
        });

        return EvadePlayer;
    }
    
    
    private Node BuildReturnHomeBranch()
    {
        
        Node returnHome = BuildSequenceBranch(new List<Node>
        {
            new IsAICarryingFlagNode(enemyTransform),
            new ReturnToBaseNode(enemyBase, enemyAgent, enemyAI)
        });
        
        Node evadeWhenClose = new Sequence(new List<Node>
        {
            new IsAICarryingFlagNode(enemyTransform),
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
    
    private Node BuildResetOpponentFlagBranch()
    {
        Node resetPlayerFlag = BuildSequenceBranch(new List<Node>
        {
            new Inverter(new IsPlayerCarryingFlagNode(player)),
            new BaseDistanceCheck(distanceOutBase, enemyBase, playerFlag, player),
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
            resetPlayerFlag,
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

