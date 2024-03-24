using System;
using System.Collections;
using System.Collections.Generic;
using Intermediate.Enemy;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour, IBTObserver
{
    [SerializeField] private Transform punchRaycastPoint1;
    [SerializeField] private Transform punchRaycastPoint2;
    
    [SerializeField] private Subject _playerSubject;
    [SerializeField] private AIAnimController AIAnim;

    private AIState currentPlayerState;
    
    //Construction of BTree

    private void Awake()
    {
        currentPlayerState = AIState.Idle;
    }
    

    public void OnNotify(string msg)
    {
        Debug.Log("message: " + msg);
    }

    private void Update()
    {
        if (currentPlayerState == AIState.Attacking)
        {
            //Use a sphere cast
        }
    }

    public void OnAIStateChange(AIState state)
    {
        if(state == currentPlayerState) return;
        
        AIAnim.PlayAttackingAnim(false);
        AIAnim.PlayEvadeAnim(false);
        AIAnim.PlayRunningAnim(0);
        
        
        currentPlayerState = state;

        switch (state)
        {
            case AIState.Attacking:
                AIAnim.PlayAttackingAnim(true);
                break;
            case AIState.Evading:
                AIAnim.PlayEvadeAnim(true);
                break;
            case AIState.Running:
                AIAnim.PlayRunningAnim(1);
                break;
            default:
                AIAnim.PlayRunningAnim(0);
              break;  
        }
        
        Debug.Log($"!!!State Changed - to - {state}!!!");
    }
}

public enum AIState
{
    Running,
    Attacking,
    Idle,
    Evading
}
