using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task.Nodes;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class TreeBuilder : MonoBehaviour, IBTObserver //Tree building class
{
    private readonly List<Node> tree = new();
    private Node root = null;
    
    //Setting up Enemy 
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

    //Setting up Player
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

    //Helper method for building a sequence
    public Node BuildSequenceBranch(List<Node> children)
    {
        Node sequence = new Sequence(children);
        tree.Add(sequence);
        return sequence;
    }

    //Helper method for building a Selector
    public Node BuildSelectorBranch(List<Node> children)
    {
        Node selector = new Selector(children);
        tree.Add(selector);
        return selector;
    }

    //Helper methods for branch building
    #region Branch Builder
    //Capture Flag
    private Node BuildCaptureFlagBranch()
    {
        Node captureFlagDecision = new Selector(new List<Node>
        {
            //On way to capture flag Check if should attack or reset opponents flag
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
            //Attack player if they have a flag, and they are not out of range
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
            //If AI is carrying flag Evade player if they are near
            new IsAICarryingFlagNode(enemyTransform),
            new CheckNearNode(nearDistance, player, enemyTransform),
            new EvadePlayerNode(strafeMultiplier, player, enemyAgent, evadeDistance, randomMoveEvade, enemyAI)
        });

        return EvadePlayer;
    }
    
    
    private Node BuildReturnHomeBranch() //Most important branch as it leads to a point for AI
    {
        Node returnHome = BuildSequenceBranch(new List<Node>
        {
            //If carrying flag the return home
            new IsAICarryingFlagNode(enemyTransform),
            new ReturnToBaseNode(enemyBase, enemyAgent, enemyAI)
        });
        
        Node evadeWhenClose = new Sequence(new List<Node>
        {
            //Evade player when returning home, to not lose the flag
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
    
    private Node BuildResetOpponentFlagBranch() //Reset Opponents Flag when they do not have it and it is outside their base
    {
        Node resetPlayerFlag = BuildSequenceBranch(new List<Node>
        {
            new Inverter(new IsPlayerCarryingFlagNode(player)),
            new BaseDistanceCheck(distanceOutBase, enemyBase, playerFlag, player),
            new PickUpFlagNode(enemyTransform, enemyAgent, playerFlag, enemyAI)
            
        });

        return resetPlayerFlag;
    }
    
    #endregion

    private void BuildTree()
    {
        var captureFlag = BuildCaptureFlagBranch();
        var attackPlayer = BuildAttackPlayerBranch();
        var returnToBase = BuildReturnHomeBranch();
        var resetPlayerFlag = BuildReturnHomeBranch();
        var evadePlayer = BuildEvadePlayerBranch();

        root = new Selector(new List<Node> //Root node as a selector to dynamically choose a behaviour every Frame
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
        root.Evaluate(); //Evaluate the root node every frame
    }

    public void OnNotify(string name)
    {
        Debug.Log($"Entering Node: {name}");
    }

    public void OnAIStateChange(AIState aiState)
    {
        
    }
}

