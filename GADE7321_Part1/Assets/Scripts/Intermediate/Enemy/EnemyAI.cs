using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IObserver
{
    [Header("Health Settings: ")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float lowHealthThreshold;
    [SerializeField] private float healthRestoreRate;

    [Header("Range Settings: ")] 
    [SerializeField] private float chaseRange;
    [SerializeField] private float shootingRange;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Subject _playerSubject;
    
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
    }


    void Update()
    {
        
    }
    

    public void OnNotify(string msg)
    {
        Debug.Log("message: " + msg);
    }
}
