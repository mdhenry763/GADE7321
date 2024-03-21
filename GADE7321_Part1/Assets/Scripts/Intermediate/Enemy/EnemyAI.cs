using System;
using System.Collections;
using System.Collections.Generic;
using Intermediate.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IBTObserver
{
    [SerializeField] private Transform punchRaycastPoint;
    
    [SerializeField] private Subject _playerSubject;
    [SerializeField] private AIAnimController AIAnim;
    
    //Construction of BTree

    private void Awake()
    {
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnNotify(string msg)
    {
        Debug.Log("message: " + msg);
    }
    
    
}
