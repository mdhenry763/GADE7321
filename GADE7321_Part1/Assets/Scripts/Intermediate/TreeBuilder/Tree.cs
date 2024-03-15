using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Task.Nodes;
using UnityEngine.AI;

public class Tree : MonoBehaviour
{
    private readonly List<Node> tree = new();
    private Node root = null;

    public Transform enemyAI;
    public NavMeshAgent enemyAgent;
    public Transform enemyFlag;
    public float nearDistance;

    public Transform playerFlag;
    public Transform player;
    public float distanceOutBase;
    public Transform playerBase;
    

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
        Node captureFlag = BuildSequenceBranch(new List<Node>
        {
            new IsAICarryingFlagNode(enemyAI),
            new PickUpFlagNode(enemyAI, enemyAgent, enemyFlag),
        });

        Node resetPlayerFlag = BuildSequenceBranch(new List<Node>
        {
            new PlayerDroppedFlagNode(distanceOutBase, playerBase, playerFlag, player),
            new PickUpFlagNode(enemyAI, enemyAgent, playerFlag)
            
        });

        Node chasePlayer = BuildSequenceBranch(new List<Node>
        {
            new IsPlayerCarryingFlagNode(player),
            new CheckNearNode(nearDistance, playerBase, enemyAI),
            
        });
        Node attackPlayer = BuildSequenceBranch(new List<Node>
        {
            
        });
        Node returnHome = BuildSequenceBranch(new List<Node>
        {
            
        });
        Node evadePlayer = BuildSequenceBranch(new List<Node>
        {
            
        });
        
        

        //Node top = BuildSequenceBranch()

        /*Node rootNode = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new IsAICarryingFlagNode(enemyAI),
                new PickUpFlagNode(enemyAI, enemyAgent, enemyFlag),

            }),
            new Selector(new List<Node>
            {

            })
        })*/
    }

    
    
}
