using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Health Settings: ")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;

    [Header("Range Settings: ")] 
    [SerializeField] private float chaseRange;
    [SerializeField] private float shootingRange;
    [SerializeField] private Transform playerTransform;
    
    //Construction of BTree
    private Node topNode;


    private NavMeshAgent agent;
    private float currentHealth;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        
        currentHealth = maxHealth;
        ConstructBehaviourTree();
    }

    private void ConstructBehaviourTree()
    {
        ChaseNode chaseNode = new(playerTransform, agent, this);
        HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
        RangeNode chasingRangeNode = new RangeNode(chaseRange, playerTransform, transform);
        RangeNode shootingRangeNode = new RangeNode(shootingRange, playerTransform, transform);
        ShootNode shootNode = new ShootNode(agent, this);

        Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
        Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });
        
        //Evasion Sequence
        //Evade Sequence
        Selector findCoverSelector = new Selector(new List<Node> { chaseSequence, chaseSequence });
        Sequence mainCoverSequence = new Sequence(new List<Node> { });
        topNode = new Selector(new List<Node> { chaseSequence, shootSequence });
    }

    void Update()
    {
        if(currentHealth < 100)
         currentHealth += Time.deltaTime * healthRestoreRate;
        topNode.Evaluate();
        if (topNode.nodeState == NodeState.Failure)
        {
            Debug.Log("System failure");
        }
    }

    private void OnMouseDown()
    {
        currentHealth -= 10f;
    }

    public float GetCurrentHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        return currentHealth;
    }

    public void ShootPlayer()
    {
        Debug.Log("Shoot Player");
    }

    public void ChasePlayer()
    {
        Debug.Log("Chase Player");
    }
    
    
}
