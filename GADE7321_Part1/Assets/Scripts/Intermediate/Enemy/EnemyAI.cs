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
